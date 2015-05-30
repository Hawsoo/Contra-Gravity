using UnityEngine;
using System.Collections;

public class PlayerModelAnimEvents : MonoBehaviour
{
	void TurnOffHornChange()
    {
        Debug.Log("Happened");
        GetComponent<Animator>().SetBool("HornChanged", false);
    }

    void EndHornChange()
    {
        GetComponentInParent<PlayerMovement2D>().canMove = true;
    }
}