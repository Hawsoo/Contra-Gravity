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

	// Update
	void Update()
	{
		
	}
}