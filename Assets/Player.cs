using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour , IDamageble
{
    public int health { get; set; } = 100; // Initial health of the player




    public void TakeDamage(int amout)
    {
        if (health > 0)
        {
            health -= amout;
            Debug.Log("Player took " + amout + " damage. Remaining health: " + health);
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Time.timeScale = 0f; 
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        Time.timeScale = 0f; // 게임 일시정지
    }
}
