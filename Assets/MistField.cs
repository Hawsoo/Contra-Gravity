using UnityEngine;
using System.Collections;

public class MistField : MonoBehaviour
{
    public float gravDirOffset;

    // Switch gravity
    void OnTriggerStay2D(Collider2D other)
    {
        // If player entered
        if (other.gameObject.tag == "Entity")
        {
            EntityProperties e = other.GetComponent<EntityProperties>();

            // Switch player's gravity
            e.inStaticGravField = true;
            e.gravDir = transform.rotation.eulerAngles.z + gravDirOffset;

            if (e.mistParticles != null)
                e.mistParticles.SetActive(true);
        }
    }

    // Release gravity
    void OnTriggerExit2D(Collider2D other)
    {
        // If player entered
        if (other.gameObject.tag == "Entity")
        {
            EntityProperties e = other.GetComponent<EntityProperties>();

            // Switch player's gravity
            e.inStaticGravField = false;

            if (e.mistParticles != null)
                e.mistParticles.SetActive(false);
        }
    }
}