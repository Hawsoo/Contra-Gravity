using UnityEngine;
using System.Collections;

public class PlayerMovementAlt : MonoBehaviour
{
    //public static float gravDir = 270;

    public float multiplier = 20;
    public float gravity = 0.5f;
    public float jumpheight = 15;

    public float maxDX = 10;
    public float spinSpeed;

    public GameObject obj;

    private float hspeed;
    private float vspeed;

    private float dx;

	// Init
	void Start ()
	{
		
	}
	
	// Update
	void Update ()
	{
        // Set direction of player according to gravity
        float angle = Mathf.MoveTowardsAngle(
            transform.eulerAngles.z - 90,
            EntityProperties.gravDir,
            spinSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle + 90);


        
        // Modify controller
        CharacterController c = GetComponent<CharacterController>();
        EntityProperties p = GetComponent<EntityProperties>();

        // Set up directional proportions
        float propX = Mathf.Cos(EntityProperties.gravDir * Mathf.Deg2Rad);
        float propY = Mathf.Sin(EntityProperties.gravDir * Mathf.Deg2Rad);

        // Get input
        dx = Input.GetAxis("Horizontal") * multiplier;

        if (Input.GetKeyDown(KeyCode.UpArrow) && p.onGround)
        {
            hspeed = jumpheight * -propX;
            vspeed = jumpheight * -propY;
            p.onGround = false;
        }

        // Apply gravity
        if (p.onGround)
        {
            hspeed = gravity * propX * Time.deltaTime;
            vspeed = gravity * propY * Time.deltaTime;
        }
        else
        {
            hspeed += gravity * propX * Time.deltaTime;
            vspeed += gravity * propY * Time.deltaTime;
        }

        // Proportion horizontal input movement
        propX = Mathf.Cos((EntityProperties.gravDir + 90) * Mathf.Deg2Rad);
        propY = Mathf.Sin((EntityProperties.gravDir + 90) * Mathf.Deg2Rad);

        float mHsp = hspeed + (dx * propX);
        float mVsp = vspeed + (dx * propY);

        // Move
        c.Move(new Vector3(mHsp, mVsp, 0) * Time.deltaTime);
        //GetComponent<Rigidbody>().velocity = new Vector3(mHsp, mVsp, 0)/* * Time.deltaTime*/;
	}

    // Flag on ground
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
            GetComponent<EntityProperties>().onGround = true;
    }

    // Flag leave ground
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
            GetComponent<EntityProperties>().onGround = false;
    }
}