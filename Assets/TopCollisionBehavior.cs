using UnityEngine;
using System.Collections;

public class TopCollisionBehavior : MonoBehaviour
{
    // Checks if hit the collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Top");
            SendMessageUpwards("HitTop");
        }
    }
}
