using UnityEngine;
using System.Collections;

public class ToggleCameraEffects : MonoBehaviour
{
    // Update
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.M))
        {
            // Toggle Motion Blur
            UnityStandardAssets.ImageEffects.CameraMotionBlur blur =
                GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>();
            blur.enabled = !blur.enabled;
        }
	}
}
