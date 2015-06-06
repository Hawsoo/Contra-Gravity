using UnityEngine;
using System.Collections;

public class PlayerMovement2D : MonoBehaviour
{
    public Animator playerModel;

    public float multiplier = 20;
    public float gravity = 70;
    public float jumpheight = 25;
    public float launchheight = 40;

    public float maxDX = 10;

    public bool startRight = true;

    public GameObject obj;
    private float hspeed;
    private float vspeed;

    private float dx;
    private float mHsp;
    private float mVsp;

    [System.NonSerialized]
    public int direction;      // -1 = left, 1 = right
    [System.NonSerialized]
    public bool canMove = true;
    private float slowDownFriction = 25;

    private bool ranWebsite = false;

    // Movement disablements
    private bool disableLeft = false;
    private bool disableRight = false;

    // BETA (debug)
    private Vector3 start;

	// Init
	void Start()
	{
        // BETA (debug)
        start = transform.position;

        // Start in correct direction
        if (startRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
	}

    // Check if player has died
    void Update()
    {
        if (GetComponent<EntityProperties>().hitHazard)
        {
            // Die
            GetComponent<EntityProperties>().hitHazard = false;
            transform.position = start;
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
            EntityProperties.spinSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle + 90);

        // Modify controller
        EntityProperties p = GetComponent<EntityProperties>();

        // Set up directional proportions
        float propX = Mathf.Cos(targetAngle * Mathf.Deg2Rad);
        float propY = Mathf.Sin(targetAngle * Mathf.Deg2Rad);

        {
            // Get input
            if (canMove)
                dx = Input.GetAxis("Horizontal") * multiplier;
            else
            {
                // Add friction to DX
                if (dx > 0)
                {
                    dx -= slowDownFriction * Time.deltaTime;
                    if (dx < 0) dx = 0;
                }
                else if (dx < 0)
                {
                    dx += slowDownFriction * Time.deltaTime;
                    if (dx > 0) dx = 0;
                }
            }

            // Disable
            if (!p.onGround)
            {
                if (disableLeft) { dx = Mathf.Max(0, dx); }
                if (disableRight) { dx = Mathf.Min(0, dx); }
            }
        }

        // Update animation components
        playerModel.SetBool("Walking", Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1);

        // Jump
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

            playerModel.SetBool("Jump", false);
        }
        else
        {
            hspeed += gravity * propX * Time.deltaTime;
            vspeed += gravity * propY * Time.deltaTime;

            playerModel.SetBool("Jump", true);
        }

        // Got hit
        if (p.IsHit())
        {
            propX = Mathf.Cos((targetAngle + (45 * direction)) * Mathf.Deg2Rad);
            propY = Mathf.Sin((targetAngle + (45 * direction)) * Mathf.Deg2Rad);

            // Launch in certain direction
            hspeed = launchheight * -propX;
            vspeed = launchheight * -propY;
            p.onGround = false;
        }

        // Proportion horizontal input movement
        propX = Mathf.Cos((targetAngle + 90) * Mathf.Deg2Rad);
        propY = Mathf.Sin((targetAngle + 90) * Mathf.Deg2Rad);

        mHsp = hspeed + (dx * propX);
        mVsp = vspeed + (dx * propY);

        // Move
        GetComponent<Rigidbody2D>().velocity = new Vector2(mHsp, mVsp);

        // Update Animation
        GetComponentInChildren<AvatarAnimationHandler>().AniUpdate(direction, dx);
        GetComponentInChildren<ContraAnimationHandler>().AniUpdate();

        // Reset variables
        GetComponent<EntityProperties>().onGround = false;
        disableLeft = false;
        disableRight = false;
	}

    // Messages
    void HitEnemy(GameObject other) { StartCoroutine("Pause", 0.15f); other.GetComponent<EntityProperties>().SendHit(); }
    void OnGround() { GetComponent<EntityProperties>().onGround = true; }
    void HitSide() { hspeed = 0; }
    void DisableSideLeft() { disableLeft = true; }
    void DisableSideRight() { disableRight = true; }
    void HitTop() { vspeed = 0; }

    // Coroutines
    private IEnumerator Pause(float p)
    {
        Time.timeScale = .0001f;
        yield return new WaitForSeconds(p * Time.timeScale);
        Time.timeScale = 1.0f;
    }
}