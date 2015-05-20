using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{
    public GameObject obj;
    public float interpolation;
    
    public Vector3 offset;
    public Vector3 rotateOffset;

    // Init
    void Start()
    {
        offset = obj.transform.position - transform.position;
        rotateOffset = obj.transform.eulerAngles - transform.eulerAngles;
    }

    // Sets the offset
    public void setOffset(Vector3 offset)
    {
        this.offset = offset;
    }

    // Sets the rotation offset
    public void setRotationOffset(Vector3 rotateOffset)
    {
        this.rotateOffset = rotateOffset;
    }

	// Update
	void FixedUpdate()
    {
        // Move towards
        transform.position += (obj.transform.position - offset - transform.position) * interpolation * Time.deltaTime;

        // Rotate towards
        float dx = Mathf.DeltaAngle(transform.eulerAngles.x, obj.transform.eulerAngles.x - rotateOffset.x);
        float dy = Mathf.DeltaAngle(transform.eulerAngles.y, obj.transform.eulerAngles.y - rotateOffset.y);
        float dz = Mathf.DeltaAngle(transform.eulerAngles.z, obj.transform.eulerAngles.z - rotateOffset.z);
        transform.eulerAngles += new Vector3(dx, dy, dz) * interpolation * Time.deltaTime;
	}
}
