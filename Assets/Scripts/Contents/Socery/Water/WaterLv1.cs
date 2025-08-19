using UnityEngine;

public class WaterLv1 : MonoBehaviour
{

    Rigidbody2D rb;
    float projectileSpeed = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector3.down * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.TryGetComponent<IDamageble>(out var target))
            {
                target.TakeDamage(10);
                Destroy(this.gameObject);
            }

        }


    }
}
