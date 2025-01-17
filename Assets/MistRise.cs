﻿using UnityEngine;
using System.Collections;

public class MistRise : MonoBehaviour
{
    public float gravity;
    public float timeFlipped;

    // Update
	void Update ()
    {
        // Set up directional proportions
        float propX = Mathf.Cos(GetComponent<EntityProperties>().gravDir * Mathf.Deg2Rad);
        float propY = Mathf.Sin(GetComponent<EntityProperties>().gravDir * Mathf.Deg2Rad);

        // Rise
        GetComponent<Rigidbody2D>().velocity = new Vector2(gravity * propX * Time.deltaTime, gravity * propY * Time.deltaTime);
	}

    // Flag on ground
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Entity")
        {
            other.GetComponent<EntityProperties>().Flip(timeFlipped);
            Destroy(gameObject);
        }
    }
}
