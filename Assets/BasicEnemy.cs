using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    private AudioSource a;

    // Get the audio source
    void Awake()
    {
        a = GetComponent<AudioSource>();
    }

    // Messages
    void DeleteWholeObject()
    {
        Destroy(gameObject);
    }

    void PlaySFX(AudioClip clip)
    {
        if (!a.isPlaying)
        {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }
    }
}
