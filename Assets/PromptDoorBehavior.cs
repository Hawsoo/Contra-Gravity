using UnityEngine;
using System.Collections;

public class PromptDoorBehavior : MonoBehaviour
{
    public GameObject playerAvatar;

    // Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAvatar.SendMessage("EndLevel");
        }
    }
}
