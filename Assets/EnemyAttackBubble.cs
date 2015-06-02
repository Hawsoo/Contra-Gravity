using UnityEngine;
using System.Collections;

public class EnemyAttackBubble : MonoBehaviour
{
    public MonoBehaviour enemy;

    public Vector2 leftSide;
    public Vector2 rightSide;

    // Follow enemy
    void Update()
    {
        transform.position = enemy.transform.position;
    }

    // Check if in attack bubble
    void OnTriggerStay2D(Collider2D other)
    {
        // Trigger attack
        if (other.gameObject.tag == "Entity"
            && other.gameObject.name == "Player")
        {
            // Check which direction
            bool right = false;

            // Transform left and right side
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, enemy.transform.eulerAngles.z));
            Vector3 leftPt = transform.position + (rot * leftSide),
                rightPt = transform.position + (rot * rightSide);

            // Check shorter distance
            if (Vector3.Distance(leftPt, other.transform.position)
                < Vector3.Distance(rightPt, other.transform.position))
            {
                right = true;
            }

            // Attack
            enemy.SendMessage("Attack", right);
        }
    }
}
