using UnityEngine;
using System.Collections;

public class EntityProperties : MonoBehaviour
{
    public static float spinSpeed = 500;

    public float gravDir = 270;
    public bool inStaticGravField = false;
    public bool onGround = false;

    public GameObject mistParticles;

    [System.NonSerialized]
    public bool flipped = false;
    [System.NonSerialized]
    public float flipTime;

    private bool gotHit = false;

    // Flips entity
    public void Flip(float timeFlipped)
    {
        GetComponent<EntityProperties>().flipped = true;
        GetComponent<EntityProperties>().flipTime = timeFlipped;
    }

    // Hit messages
    public void SendHit()
    {
        gotHit = true;
    }

    public bool IsHit()
    {
        bool val = gotHit;
        gotHit = false;
        return val;
    }
}