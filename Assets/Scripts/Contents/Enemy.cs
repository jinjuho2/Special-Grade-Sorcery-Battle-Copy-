using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageble
{
    Rigidbody2D rb;
    int atk = 30;
    float moveSpeed = 1f;

    public Image healthbar;
    public float atkCooldown = 2f;   // 공격 간격(초)

    public int health { get; set; } = 100;

    // 내부 상태
    private bool inRange = false;
    private float atkTimer = 0f;
    private IDamageble currentTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D missing on Enemy.");

        Init();
    }

    private void Init()
    {
        healthbar = transform.Find("Canvas/Healthbar").GetComponent<Image>();
    }

    

    private void Update()
    {
        healthbar.fillAmount = health / 100f;
        // 사정거리 안이면 공격 타이머 진행
        if (inRange && currentTarget != null)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkCooldown)
            {
                currentTarget.TakeDamage(atk);
                atkTimer = 0f;                 
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.up * moveSpeed;
    }

    // 사정거리 진입
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Player 스크립트가 부모에 있을 수도 있으니 InParent로
            currentTarget = collision.GetComponentInParent<IDamageble>();
            if (currentTarget != null)
            {
                inRange = true;
                atkTimer = 0f;          // 진입 시 타이머 초기화(원하면 유지해도 됨)
                moveSpeed = 0f;
            }
        }
    }

    // 사정거리 유지 (필수는 아님, 깨어있게 하고 싶으면 사용)
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (inRange && rb != null && rb.IsSleeping())
            rb.WakeUp();                // 잠들면 깨워서 트리거 유지 안정화
    }

    // 사정거리 이탈
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            inRange = false;
            currentTarget = null;
            atkTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // 사망 처리
            Destroy(gameObject);
            GameManager.instance.gold += Random.Range(1, 4);
        }
    }
}
