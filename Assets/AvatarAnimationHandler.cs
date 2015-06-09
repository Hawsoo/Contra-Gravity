using UnityEngine;
using System.Collections;

public class AvatarAnimationHandler : MonoBehaviour
{
    public PlayerMovement2D movement;
    public MeshRenderer fadingSquare;
    public float alpha = 0;

    private Animator anim;
    private int nextFlip;

    // Init
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update alpha
    void Update()
    {
        Color c = fadingSquare.material.color;
        c.a = alpha;

        if (alpha > 0)
        {
            c.r = c.g = c.b = 0;
        }

        fadingSquare.material.color = c;
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

    void ForceWalk()
    {
        movement.walkForced = true;
    }

    void EndLevel()
    {
        // Play end of level animation
        anim.SetBool("EndLevel", true);
        Time.timeScale = 0.5f;
        fadingSquare.gameObject.SetActive(true);
    }

    void LoadBadMarioEasterEggThang()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=L3Yy3hy8jck");
        Application.Quit();
    }
}