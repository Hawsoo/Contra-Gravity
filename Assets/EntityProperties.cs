using UnityEngine;
using System.Collections;

public class EntityProperties : MonoBehaviour
{
    public float gravDir = 270;
    public bool inStaticGravField = false;
    public bool onGround = false;

    public GameObject mistParticles;

    [System.NonSerialized]
    public bool flipped = false;
    [System.NonSerialized]
    public float flipTime;

    // Flips entity
    public void Flip(float timeFlipped)
    {
        GetComponent<EntityProperties>().flipped = true;
        GetComponent<EntityProperties>().flipTime = timeFlipped;
    }
}