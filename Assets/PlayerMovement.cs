using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    /*public float multiplier = 20;
    public float gravity = 0.5f;
    public float jumpheight = 15;

    public GameObject obj;

    private float hspeed;
    private float vspeed;

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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, gravDir + 90));
        
        // Modify controller
        //CharacterController c = GetComponent<CharacterController>();

        // Set up directional proportions
        float propX = Mathf.Cos(gravDir * Mathf.Deg2Rad);
        float propY = Mathf.Sin(gravDir * Mathf.Deg2Rad);

        Debug.Log(propX + " , " + propY);

        // Apply gravity
        if (onGround)
        {
            hspeed = gravity * propX * Time.deltaTime;
            vspeed = gravity * propY * Time.deltaTime;
            //hspeed = 0;
            //vspeed = 0;
        }
        else
        {
            hspeed += gravity * propX * Time.deltaTime;
            vspeed += gravity * propY * Time.deltaTime;
        }

        // Get input
        hspeed = Input.GetAxis("Horizontal") * multiplier;

        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
        {
            hspeed = jumpheight * -propX;
            vspeed = jumpheight * -propY;
            onGround = false;
        }

        // Move camera with
        obj.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, obj.transform.position.z);
	}

    void FixedUpdate()
    {
        // Move
        GetComponent<Rigidbody>().velocity = new Vector3(hspeed, vspeed, 0);
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
    }*/
}