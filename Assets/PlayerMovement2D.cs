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

    [System.NonSerialized]
    public int direction;      // -1 = left, 1 = right

    private bool ranWebsite = false;

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
	
	// Update
    void FixedUpdate()
    {
        // Does stupid easter egg stuff
        // ctrl + S
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.S) && !ranWebsite)
        {
            // Goto mameshiba website with weird american kids
            ranWebsite = true;
            Application.OpenURL("http://dogatch.jp/mameshibaworld/bc/video10.html");
        }
        else if (!Input.GetKey(KeyCode.LeftControl) || !Input.GetKey(KeyCode.S))
        {
            // Reset flag
            ranWebsite = false;
        }

        // Set direction of player according to gravity
        float targetAngle = GetComponent<EntityProperties>().gravDir;
        if (GetComponent<EntityProperties>().flipped)
        {
            GetComponent<EntityProperties>().mistParticles.SetActive(true);
            targetAngle += 180;

            GetComponent<EntityProperties>().flipTime -= Time.deltaTime;
            if (GetComponent<EntityProperties>().flipTime <= 0) GetComponent<EntityProperties>().flipped = false;
        }
        else
        {
            if (!GetComponent<EntityProperties>().inStaticGravField)
                GetComponent<EntityProperties>().mistParticles.SetActive(false);
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