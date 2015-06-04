using UnityEngine;
using System.Collections;

public class HazardBehavior : MonoBehaviour
{
    // Destroy other
    void OnTriggerEnter2D(Collider2D other)
    {
        // If it is an entity
        if (other.gameObject.tag == "Entity")
        {

        }
    }
}
