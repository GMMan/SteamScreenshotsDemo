using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float Speed = 25;
    public bool RotateZOnly = false;

    // Update is called once per frame
    void Update()
    {
        float r = Speed * Time.deltaTime;
        if (RotateZOnly)
        {
            gameObject.transform.Rotate(0, 0, r);
        }
        else
        {
            gameObject.transform.Rotate(r, r, 0);
        }
    }
}
