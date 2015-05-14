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

    [SerializeField]
    private bool onGround = false;

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
            PlayerMovement.gravDir,
            spinSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle + 90);


        
        // Modify controller
        CharacterController c = GetComponent<CharacterController>();

        // Set up directional proportions
        float propX = Mathf.Cos(PlayerMovement.gravDir * Mathf.Deg2Rad);
        float propY = Mathf.Sin(PlayerMovement.gravDir * Mathf.Deg2Rad);

        // Get input
        dx = Input.GetAxis("Horizontal") * multiplier;

        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
        {
            hspeed = jumpheight * -propX;
            vspeed = jumpheight * -propY;
            onGround = false;
        }

        // Apply gravity
        if (onGround)
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
        propX = Mathf.Cos((PlayerMovement.gravDir + 90) * Mathf.Deg2Rad);
        propY = Mathf.Sin((PlayerMovement.gravDir + 90) * Mathf.Deg2Rad);

        float mHsp = hspeed + (dx * propX);
        float mVsp = vspeed + (dx * propY);

        // Move
        c.Move(new Vector3(mHsp, mVsp, 0) * Time.deltaTime);
	}

    // Flag on ground
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground") onGround = true;
    }

    // Flag leave ground
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground") onGround = false;
    }
}