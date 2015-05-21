using UnityEngine;
using System.Collections;

public class ContraAnimationHandler : MonoBehaviour
{
    private bool nextAttack = false;

    // Update
    public void AniUpdate()
    {
        // Attack
        if (Input.GetKey(KeyCode.X))
        {
            if (nextAttack)
            {
                nextAttack = false;
                GetComponent<Animator>().Play("attack2");
            }
            else
            {
                GetComponent<Animator>().Play("attack1");
            }
        }

    }

    // Messages
    void AllowNextAttack() { nextAttack = true; Debug.Log("yes"); }
    void DisallowNextAttack() { nextAttack = false; Debug.Log("non"); }
}