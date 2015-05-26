using UnityEngine;
using System.Collections;

public class PlayerMovement2D : MonoBehaviour
{
    public float multiplier = 20;
    public float gravity;
    public float jumpheight = 15;

    public float maxDX = 10;
    public float spinSpeed;

    public bool startRight = true;

    public GameObject obj;
    private float hspeed;
    private float vspeed;

    private float dx;
    private float mHsp;
    private float mVsp;

    private bool flipped = false;
    private float flipTime;

    [System.NonSerialized]
    public int direction;      // -1 = left, 1 = right

	// Init
	void Start()
	{
        if (startRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
	}

    // Flips character
    public void Flip()
    {
        flipped = true;
        flipTime = 0.5f;
    }
	
	// Update
    void FixedUpdate()
    {
        // Set direction of player according to gravity
        float targetAngle = GetComponent<EntityProperties>().gravDir;
        if (flipped)
        {
            targetAngle += 180;

            flipTime -= Time.deltaTime;
            if (flipTime <= 0) flipped = false;
        }

        float angle = Mathf.MoveTowardsAngle(
            transform.eulerAngles.z - 90,
            targetAngle,
            spinSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle + 90);


        // Modify controller
        EntityProperties p = GetComponent<EntityProperties>();

        // Set up directional proportions
        float propX = Mathf.Cos(targetAngle * Mathf.Deg2Rad);
        float propY = Mathf.Sin(targetAngle * Mathf.Deg2Rad);

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
        propX = Mathf.Cos((targetAngle + 90) * Mathf.Deg2Rad);
        propY = Mathf.Sin((targetAngle + 90) * Mathf.Deg2Rad);

        mHsp = hspeed + (dx * propX);
        mVsp = vspeed + (dx * propY);

        // Move
        GetComponent<Rigidbody2D>().velocity = new Vector2(mHsp, mVsp);

        // Update Animation
        GetComponentInChildren<PlayerAnimationHandler>().AniUpdate(direction, dx);
        GetComponentInChildren<ContraAnimationHandler>().AniUpdate();
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