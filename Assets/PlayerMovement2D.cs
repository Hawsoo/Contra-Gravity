using UnityEngine;
using System.Collections;

public class PlayerMovement2D : MonoBehaviour
{
    public float multiplier = 20;
    public float gravity = 0.5f;
    public float jumpheight = 15;

    public float maxDX = 10;
    public float spinSpeed;

    public GameObject obj;

    private float hspeed;
    private float vspeed;

    private float dx;
    private float mHsp;
    private float mVsp;

	// Init
	void Start()
	{
		
	}
	
	// Update
    void FixedUpdate()
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

        if (Input.GetKey(KeyCode.UpArrow) && p.onGround)
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

        mHsp = hspeed + (dx * propX);
        mVsp = vspeed + (dx * propY);

        // Move
        GetComponent<Rigidbody2D>().velocity = new Vector2(mHsp, mVsp);
	}

    // Flag on ground
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
            GetComponent<EntityProperties>().onGround = true;
    }

    // Flag leave ground
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
            GetComponent<EntityProperties>().onGround = false;
    }
}