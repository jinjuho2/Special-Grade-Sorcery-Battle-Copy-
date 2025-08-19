using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour , IDamageble
{
    public int health { get; set; } = 100; // Initial health of the player
    public Image healthbar;


    public void Awake()
    {
        Transform healthbarTransform = transform.Find("Canvas/Healthbar");
        healthbar = healthbarTransform.GetComponent<Image>();
    }


    public void TakeDamage(int amout)
    {
        if (health > 0)
        {
            health -= amout;
            healthbar.fillAmount = health / 100f;
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
