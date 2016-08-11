Unity Steam Screenshots Demo
============================
This is a fairly straightforward example of how to intercept Steam Overlay
screenshot requests and do some processing before sending a screenshot back
to the overlay. This method uses a `Texture2D` to capture the screen and
sends back the pixel data to the overlay directly.

How to use
----------
Simply place the [SteamScreenshotCapturer.cs](Assets/Scripts/SteamScreenshotCapturer.cs)
script somewhere in your `Assets` folder and attach the script to a persistent
`GameObject` in your scene. If you are using a `Canvas` for your GUI, set the
target canvas on the script. If you are using something more complicated than
a `Canvas`, change the type of `TargetCanvas` in the script to `GameObject` or
whatever you need.

When the player presses the screen capture key configured in Steam Overlay (by
default F12), the overlay will send a screenshot request to your game, and this
script will receive that, disable `TargetCanvas`, generate the screenshot,
send it to the overlay, then reenable `TargetCanvas`. The end result will look
as if the overlay itself took the screenshot, but sans whatever objects you
don't want to appear.

If you'd like to do hi-res screenshots, have a look [here](http://answers.unity3d.com/answers/22959/view.html)
for what you can change and adapt.

This project was made in response to a question [Luke](https://steamcommunity.com/id/lucasgame)
posted on the Steamworks Development discussions.
