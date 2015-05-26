using UnityEngine;
using System.Collections;

public class ModifyGravity2D : MonoBehaviour
{
    public float gravDirOffset;

    // Switch gravity
    void OnTriggerStay2D(Collider2D other)
    {
        // If player entered
        if (other.gameObject.tag == "Player")
        {
            // Switch player's gravity
            other.GetComponent<EntityProperties>().inStaticGravField = true;
            other.GetComponent<EntityProperties>().gravDir = transform.rotation.eulerAngles.z + gravDirOffset;
        }
    }

    // Release gravity
    void OnTriggerExit2D(Collider2D other)
    {
        // If player entered
        if (other.gameObject.tag == "Player")
        {
            // Switch player's gravity
            other.GetComponent<EntityProperties>().inStaticGravField = false;
        }
    }
}