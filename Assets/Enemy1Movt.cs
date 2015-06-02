using UnityEngine;
using System.Collections;

enum Action
{
    MOVE, STAY, ATTACK
}

public class Enemy1Movt : MonoBehaviour
{
    public CircleCollider2D attackBubble;

    public float moveSpeed = 20;
    public float gravity = 70;

    private float hspeed;
    private float vspeed;

    // AI components
    private Action action;
    
    private bool moving = true;
    private bool right = true;
    public float chanceMoving = .7f;
    public float chanceTurn = .05f;

    private bool attack = false;
    private bool attackRight = false;

    public float actionDuration;
    private float actionTimer = 0;

	// Update
	void FixedUpdate()
	{
        float targetAngle = GetComponent<EntityProperties>().gravDir;

        // Move to gravity direction
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

        // Got hit
        if (p.IsHit())
        {
            // BETA: destroy self
            Destroy(gameObject);
        }

        // AI
        float dx = 0;
        {
            if (p.onGround)
            {
                if (actionTimer <= 0)
                {
                    // Find random
                    moving = chanceMoving >= Random.value;
                    if (chanceTurn >= Random.value)
                    {
                        right = !right;
                    }

                    // Perform another action
                    actionTimer = actionDuration;

                    // Moving
                    if (moving)
                    {
                        action = Action.MOVE;
                        if (attack) { action = Action.ATTACK; }
                    }
                    else
                    {
                        action = Action.STAY;
                    }

                    // Attacking
                }
                else
                {
                    actionTimer -= Time.deltaTime;

                    // Do action
                    switch (action)
                    {
                        case Action.MOVE:
                            if (right)
                            {
                                dx = moveSpeed;
                            }
                            else
                            {
                                dx = -moveSpeed;
                            }
                            break;

                        case Action.STAY:
                            dx = 0;
                            break;

                        case Action.ATTACK:
                            if (attackRight)
                            {
                                dx = moveSpeed * 2;
                            }
                            else
                            {
                                dx = -moveSpeed * 2;
                            }
                            break;
                    }
                }
            }
        }

        // Proportion horizontal input movement
        propX = Mathf.Cos((targetAngle - 90) * Mathf.Deg2Rad);
        propY = Mathf.Sin((targetAngle - 90) * Mathf.Deg2Rad);

        // Move
        GetComponent<Rigidbody2D>().velocity = new Vector2(hspeed + (dx * propX), vspeed + (dx * propY));

        // Reset variables
        p.onGround = false;
        attack = false;
	}

    // Flag on ground
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            GetComponent<EntityProperties>().onGround = true;
        }
    }

    // Attack
    void Attack(bool right)
    {
        attack = true;
        attackRight = right;
    }

    // Repel player
    void OnCollisionEnter2D(Collision2D coll)
    {
        // Check if player
        if (coll.gameObject.tag == "Entity"
            && coll.gameObject.name == "Player")
        {
            coll.gameObject.GetComponent<EntityProperties>().SendHit();
        }
    }
}