using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour
{
    public GameObject HUDCam;
    public float angleErrorWindow = 10;

    // Check if in doorway
    void OnTriggerStay2D(Collider2D other)
    {
        // Trigger attack
        if (other.gameObject.tag == "Entity"
            && other.gameObject.name == "Player"
            && !other.GetComponent<PlayerMovement2D>().walkForced)
        {
            // Check if player's rotation is about the same
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, other.transform.eulerAngles.z)) < angleErrorWindow)
            {
                // Prompt user to open door
                HUDCam.SendMessage("ShowActionIcon", 1);
            }
        }
    }
}
