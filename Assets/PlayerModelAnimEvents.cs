using UnityEngine;
using System.Collections;

public class PlayerModelAnimEvents : MonoBehaviour
{
	void TurnOffHornChange()
    {
        GetComponent<Animator>().SetBool("HornChanged", false);
    }

    // Checks if player is on ground
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            SendMessageUpwards("OnGround");
        }
    }
}