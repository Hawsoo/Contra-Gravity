using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour
{
    // Update
    public void AniUpdate(int direction, float hspeed)
    {
        // Make sure is facing correct direction
        if (direction < 0)
        {
            // Facing left
            if (hspeed > 0)
            {
                // Turn right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, Mathf.Abs(transform.localScale.z));
                GetComponentInParent<PlayerMovement2D>().direction = 1;
                GetComponent<Animator>().Play("turn");
            }
        }
        else if (direction > 0)
        {
            // Facing right
            if (hspeed < 0)
            {
                // Turn left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, -Mathf.Abs(transform.localScale.z));
                GetComponentInParent<PlayerMovement2D>().direction = -1;
                GetComponent<Animator>().Play("turn");
            }
        }
    }
}