using UnityEngine;
using System.Collections;

public class PlayerModelAnimEvents : MonoBehaviour
{
	void TurnOffHornChange()
    {
        GetComponent<Animator>().SetBool("HornChanged", false);
    }

    void EndHornChange()
    {
        //GetComponentInParent<PlayerMovement2D>().canMove = true;
    }
}