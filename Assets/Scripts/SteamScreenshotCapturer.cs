using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamScreenshotCapturer : MonoBehaviour {
    // We should only have one instance hooked at a time, otherwise multiple
    // screenshots will get taken and things get a bit messy.
    static bool initialized;

    Callback<ScreenshotRequested_t> screenshotRequestedCallback;

    // This is the canvas you want to toggle on/off. Change the type to
    // GameObject if you have something more complicated going on.
    public Canvas TargetCanvas;

	// Use this for initialization
	void OnEnable () {
	    if (SteamManager.Initialized && !initialized)
        {
            // Create callback and hook the overlay's screenshot requests
            screenshotRequestedCallback = Callback<ScreenshotRequested_t>.Create(captureScreenshot);
            SteamScreenshots.HookScreenshots(true);
            initialized = true;
        }
	}

    void OnDisable()
    {
        if (initialized)
        {
            // Unhook from the overlay's screenshot requests
            screenshotRequestedCallback = null;
            SteamScreenshots.HookScreenshots(false);
            initialized = false;
        }
    }
	
    void captureScreenshot(ScreenshotRequested_t param)
    {
        StartCoroutine(captureScreenshotRoutine());
    }

    // This captures the screenshot and feeds it to Steam
    IEnumerator captureScreenshotRoutine()
    {
        // Just in case the callback's still firing even if we're disabled
        if (!enabled) yield break;

        if (TargetCanvas != null) TargetCanvas.enabled = false; // Hide the canvas
        yield return new WaitForEndOfFrame(); // Wait for the entire frame to finish drawing

        // Create texture and load it with the current screen output
        // Note the texture format is RGB24, as required by Steam
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, screenshot.width, screenshot.height), 0, 0);

        // Get raw texture data; need to flip it vertically as well. After that, send it to Steam
        byte[] raw = screenshot.GetRawTextureData();
        raw = VerticallyFlipRgb(raw, screenshot.width, screenshot.height);
        SteamScreenshots.WriteScreenshot(raw, (uint)raw.Length, screenshot.width, screenshot.height);

        if (TargetCanvas != null) TargetCanvas.enabled = true; // Reenable the canvas
    }

    static byte[] VerticallyFlipRgb(byte[] raw, int width, int height)
    {
        int bytesPerLine = width * 3; // RGB is 1 byte per component
        byte[] flipped = new byte[raw.Length];
        for (int i = 0; i < height; ++i)
        {
            System.Buffer.BlockCopy(raw, (height - i - 1) * bytesPerLine,
                flipped, i * bytesPerLine, bytesPerLine);
        }
        return flipped;
    }
}
