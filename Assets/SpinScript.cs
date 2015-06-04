using UnityEngine;
using System.Collections;

public class SpinScript : MonoBehaviour
{
    public bool spin = true;
    public float speed;

    private Vector3 currentPos;

    // Init
    void Start()
    {
        currentPos = transform.position;
    }

    // Update
    void FixedUpdate()
    {
        // Flag out
        if (!spin) return;

        transform.position = currentPos;

        // Spin
        GetComponent<Rigidbody2D>().angularVelocity = speed;
    }
}
