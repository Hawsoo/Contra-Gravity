using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour
{
    public AnimationClip turnLeft;
    public AnimationClip turnRight;

    // Update
    public void AniUpdate(int direction, float hspeed)
    {
        if (direction < 0)
        {
            // Facing left
            if (hspeed > 0)
            {
                // Turn right
                GetComponentInParent<PlayerMovement2D>().direction = 1;
                GetComponent<Animator>().Play("turn_right");
            }
        }
        else if (direction > 0)
        {
            // Facing right
            if (hspeed < 0)
            {
                // Turn left
                GetComponentInParent<PlayerMovement2D>().direction = -1;
                GetComponent<Animator>().Play("turn_left");
            }
        }
    }
}