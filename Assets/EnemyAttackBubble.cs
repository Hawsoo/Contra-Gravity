using UnityEngine;
using System.Collections;

public class EnemyAttackBubble : MonoBehaviour
{
    public MonoBehaviour enemy;

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
            bool right = false;// FINISH this
            transform.RotateAround(transform.position, other.transform.position, transform.eulerAngles.z);

            // Attack
            enemy.SendMessage("Attack", right);
        }
    }
}
