using UnityEngine;
using System.Collections;

public class ModifyGravity : MonoBehaviour
{
    public float gravDirOffset;

    // Switch gravity
    void OnTriggerEnter(Collider other)
    {
        // If player entered
        if (other.gameObject.tag == "Player")
        {
            // Switch player's gravity
            EntityProperties.gravDir = transform.rotation.eulerAngles.z + gravDirOffset;
        }
    }
}