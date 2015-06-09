using UnityEngine;
using System.Collections;

enum HornPosition
{
    UP, CARRY
}

[System.Serializable]
public class SoundsContra
{
    public AudioClip tubaSong;
    public AudioClip swing1;
    public AudioClip swing2;
    public AudioClip swing3;
}

public class ContraAnimationHandler : MonoBehaviour
{
    public PlayerMovement2D playerMove;
    public Animator playerAnim;

    public SoundsContra sounds;
    private AudioSource a;

    private bool xHeld = false;
    private bool cHeld = false;

    private HornPosition hornpos = HornPosition.CARRY;

    private bool attack2 = false;
    private bool attack3 = false;

    void Awake()
    {
        a = GetComponent<AudioSource>();
    }

    // Update
    public void AniUpdate()
    {
        // Toggle horn position
        if (Input.GetKey(KeyCode.C) && !cHeld)
        {
            //playerMove.canMove = false;
            playerAnim.SetBool("HornChanged", true);

            if (hornpos == HornPosition.CARRY)
            {
                hornpos = HornPosition.UP;
                playerAnim.SetBool("HornUp", true);

                // Horns up
                GetComponent<Animator>().Play("horn_up");
            }
            else if (hornpos == HornPosition.UP)
            {
                hornpos = HornPosition.CARRY;
                playerAnim.SetBool("HornUp", false);

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
                playerMove.canMove = false;
                GetComponent<Animator>().Play("contra_play");
            }
            else if (!Input.GetKey(KeyCode.X) && xHeld)
            {
                playerMove.canMove = true;
                GetComponent<Animator>().Play("contra_up");
            }
        }

        // Update lagging messages
        xHeld = Input.GetKey(KeyCode.X);
        cHeld = Input.GetKey(KeyCode.C);
    }

    // Checks if contra hit something
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Entity")
        {
            SendMessageUpwards("HitEnemy", other.gameObject);
        }
    }

    // Messages
    void PlayTubaSong() { a.PlayOneShot(sounds.tubaSong); }
    void PlaySwing1() { a.PlayOneShot(sounds.swing1); }
    void PlaySwing2() { a.PlayOneShot(sounds.swing2); }
    void PlaySwing3() { a.PlayOneShot(sounds.swing3); }

    void AllowAttack2() { attack2 = true; }
    void DisallowAttack2() { attack2 = false; }
    void AllowAttack3() { attack3 = true; }
    void DisallowAttack3() { attack3 = false; }
    void StartOfAttack() { playerMove.canMove = false; GetComponent<BoxCollider2D>().enabled = true; }
    void EndOfAttack() { playerMove.canMove = true; GetComponent<BoxCollider2D>().enabled = false; }
}