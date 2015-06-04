using UnityEngine;
using System.Collections;

public class PlayButtonBehavior : MonoBehaviour
{
    public Animator fadeAnim;

    // Messages
    void ActionHappened()
    {
        fadeAnim.Play("fadein_fast");
    }
}