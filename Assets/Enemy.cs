using UnityEngine;

public class Enemy : MonoBehaviour , IDamageble
{
    Rigidbody2D rb;
    int atk = 10; // Attack damage of the enemy
    float moveSpeed = 5f; // Speed at which the enemy moves

    public int health { get; set; } = 100; // Initial health of the enemy 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the Enemy GameObject.");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       rb.linearVelocity = Vector2.up * moveSpeed; // Move the enemy upwards at a speed of 5 units per second
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var target = collision.GetComponent<IDamageble>();
            if (target != null)
            {
                target.TakeDamage(atk); // Deal damage to the player
                moveSpeed = 0f; // Stop the enemy's movement upon collision
                Debug.Log("Enemy attacked the player for " + atk + " damage.");
            }
            else
            {
                Debug.LogWarning("Player does not implement IDamageble interface.");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);
        if (health <= 0)
        {

        }
    }
}
