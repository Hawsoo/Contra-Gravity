using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{
    public GameObject obj;

	// Update
	void Update ()
    {
        obj.transform.position = new Vector3(transform.position.x, obj.transform.position.y, obj.transform.position.z);
	}
}
