using UnityEngine;
using System.Collections;

public class LeftCollisionBehavior : MonoBehaviour
{
    // Checks if hit the collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            SendMessageUpwards("HitSide");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            SendMessageUpwards("DisableSideLeft");
        }
    }
}
