using UnityEngine;
using System.Collections;

public class AvatarAnimationHandler : MonoBehaviour
{
    private Animator anim;
    private int nextFlip;

    // Init
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update
    public void AniUpdate(int direction, float hspeed)
    {
        if (anim.GetBool("TurnRequested")) return;

        // Make sure is facing correct direction
        if (direction < 0)
        {
            // Facing left
            if (hspeed > 0)
            {
                // Turn right
                nextFlip = GetComponentInParent<PlayerMovement2D>().direction = 1;
                anim.SetBool("TurnRequested", true);
            }
        }
        else if (direction > 0)
        {
            // Facing right
            if (hspeed < 0)
            {
                // Turn left
                nextFlip = GetComponentInParent<PlayerMovement2D>().direction = -1;
                anim.SetBool("TurnRequested", true);
            }
        }
    }

    // Messages
    void Flip()
    {
        transform.localScale = new Vector3(nextFlip * Mathf.Abs(transform.localScale.x), transform.localScale.y, nextFlip * Mathf.Abs(transform.localScale.z));
        GetComponent<Animator>().SetBool("TurnRequested", false);
    }
}