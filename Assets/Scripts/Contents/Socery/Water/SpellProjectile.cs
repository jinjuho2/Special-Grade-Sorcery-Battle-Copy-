using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public SpellDataSO spell;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.TryGetComponent<IDamageble>(out var target))
            {
                target.TakeDamage(spell.damage);
                Destroy(this.gameObject);
            }

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Road"))
        {
            Destroy(this.gameObject);
        }

    }

}
