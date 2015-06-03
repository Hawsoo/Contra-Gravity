using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour
{
    public bool playOnStart;

	// Init
	void Start()
	{
		if (playOnStart)
        {
            GetComponent<AudioSource>().Play();
        }
	}

	// Messages
	void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }
}