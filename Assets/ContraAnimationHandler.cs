using UnityEngine;
using System.Collections;

enum HornPosition
{
    UP, CARRY
}

public class ContraAnimationHandler : MonoBehaviour
{
    public PlayerMovement2D player;

    private bool xHeld = false;
    private bool cHeld = false;

    private HornPosition hornpos = HornPosition.CARRY;

    private bool attack2 = false;
    private bool attack3 = false;

    // Update
    public void AniUpdate()
    {
        // Toggle horn position
        if (Input.GetKey(KeyCode.C) && !cHeld)
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

        // Attack
        if (hornpos == HornPosition.CARRY)
        {
            // Attack
            if (Input.GetKey(KeyCode.X) && !xHeld)
            {
                player.canMove = false;
                GetComponent<BoxCollider2D>().enabled = true;

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
        // Blow/Play
        else
        {
            // Blow/Play
            if (Input.GetKey(KeyCode.X) && !xHeld)
            {
                player.canMove = false;
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().Play("contra_play");
            }
            else if (!Input.GetKey(KeyCode.X) && xHeld)
            {
                player.canMove = true;
                GetComponent<AudioSource>().Stop();
                GetComponent<Animator>().Play("contra_up");
            }
        }

        // Update lagging messages
        xHeld = Input.GetKey(KeyCode.X);
        cHeld = Input.GetKey(KeyCode.C);
    }

    // Messages
    void AllowAttack2() { attack2 = true; }
    void DisallowAttack2() { attack2 = false; }
    void AllowAttack3() { attack3 = true; }
    void DisallowAttack3() { attack3 = false; }
    void EndOfAttack() { player.canMove = true; GetComponent<BoxCollider2D>().enabled = false; }
}