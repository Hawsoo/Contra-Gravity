using UnityEngine;
using System.Collections;

public class ModifyGravity2D : MonoBehaviour
{
    public float gravDirOffset;

    // Switch gravity
    void OnTriggerEnter2D(Collider2D other)
    {
        // If player entered
        if (other.gameObject.tag == "Player")
        {
            // Switch player's gravity
            EntityProperties.gravDir = transform.rotation.eulerAngles.z + gravDirOffset;
        }
    }
}