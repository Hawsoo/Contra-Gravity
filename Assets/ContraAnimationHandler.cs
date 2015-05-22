using UnityEngine;
using System.Collections;

enum HornPosition
{
    UP, CARRY
}

public class ContraAnimationHandler : MonoBehaviour
{
    private bool xHeld = false;
    private bool spaceHeld = false;

    private HornPosition hornpos = HornPosition.CARRY;

    private bool attack2 = false;
    private bool attack3 = false;

    // Update
    public void AniUpdate()
    {
        // Toggle horn position
        if (Input.GetKey(KeyCode.Space) && !spaceHeld)
        {
            if (hornpos == HornPosition.CARRY)
            {
                hornpos = HornPosition.UP;

                // Horns up
                GetComponent<Animator>().Play("horn_up");
            }
            else if (hornpos == HornPosition.UP)
            {
                hornpos = HornPosition.CARRY;

                // Horns down
                GetComponent<Animator>().Play("horn_down");
            }
        }

        if (hornpos == HornPosition.CARRY)
        {
            // Attack
            if (Input.GetKey(KeyCode.X) && !xHeld)
            {
                if (attack3)
                {
                    attack3 = false;
                    GetComponent<Animator>().Play("attack3");
                }
                else if (attack2)
                {
                    attack2 = false;
                    GetComponent<Animator>().Play("attack2");
                }
                else
                {
                    GetComponent<Animator>().Play("attack1");
                }
            }
        }

        // Update lagging messages
        xHeld = Input.GetKey(KeyCode.X);
        spaceHeld = Input.GetKey(KeyCode.Space);
    }

    // Messages
    void AllowAttack2() { attack2 = true; }
    void DisallowAttack2() { attack2 = false; }
    void AllowAttack3() { attack3 = true; }
    void DisallowAttack3() { attack3 = false; }
}