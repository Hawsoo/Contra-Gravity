using UnityEngine;
using System.Collections;

public class PlayButtonBehavior : MonoBehaviour
{
    // Messages
    void ActionHappened()
    {
        Application.LoadLevel(1);
    }
}