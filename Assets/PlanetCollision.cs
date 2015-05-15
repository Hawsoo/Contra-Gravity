using UnityEngine;
using System.Collections;

public class PlanetCollision : MonoBehaviour
{
    public Vector3 rayLeft;
    public Vector3 rayRight;

    [SerializeField]
    private bool leftHit = false;
    [SerializeField]
    private float leftDist = 0;
    [SerializeField]
    private bool rightHit = false;
    [SerializeField]
    private float rightDist = 0;

	// Init
	void Start()
	{
		
	}

	// Update
	void Update()
	{
        //Quaternion.Euler(60, 0, 0) * Vector3.forward;

        EntityProperties p = GetComponent<EntityProperties>();

        // On ground
        if (p.onGround)
        {
            CalculateRayCastLeftRight();

            // Tilt according
            if (leftHit && rightHit)
            {
                // Find angle of slope
                float w = Mathf.Abs(rayRight.x - rayLeft.x);
                float h = leftDist - rightDist;

                float angle = (Mathf.Atan2(h, w) * Mathf.Rad2Deg) + transform.eulerAngles.z;

                Debug.Log(angle);

                // Set grav direction to correct angle
                EntityProperties.gravDir = angle - 90;

            }
        }
        // In midair
        else
        {

        }
	}
    
    private void CalculateRayCastLeftRight()
    {
        // Setup ray test
        float propX = Mathf.Cos((transform.eulerAngles.z - 90) * Mathf.Deg2Rad);
        float propY = Mathf.Sin((transform.eulerAngles.z - 90) * Mathf.Deg2Rad);

        Debug.DrawRay(transform.position + (transform.rotation * rayLeft), new Vector3(propX, propY), Color.cyan);
        Debug.DrawRay(transform.position + (transform.rotation * rayRight), new Vector3(propX, propY), Color.cyan);

        // Do ray test
        leftHit = false;
        leftDist = 0;
        rightHit = false;
        rightDist = 0;

        // Cast left ray
        RaycastHit rayHit;

        Ray ray = new Ray(transform.position + (transform.rotation * rayLeft), new Vector3(propX, propY));
        if (Physics.Raycast(ray, out rayHit, 25)
            && rayHit.collider.gameObject.tag == "Ground")
        {
            leftHit = true;
            leftDist = rayHit.distance;
        }

        // Cast right ray
        ray = new Ray(transform.position + (transform.rotation * rayRight), new Vector3(propX, propY));
        if (Physics.Raycast(ray, out rayHit, 25)
            && rayHit.collider.gameObject.tag == "Ground")
        {
            rightHit = true;
            rightDist = rayHit.distance;
        }
    }
}