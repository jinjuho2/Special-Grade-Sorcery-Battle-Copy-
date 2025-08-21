using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Threading;
using UnityEditor.SceneManagement;

public class SpellSlot : MonoBehaviour
{
    public SpellDataSO spell;   // 이 슬롯이 가진 주문
    public float castTimer = 0;
    public GameObject firePivot;
    public Image coolImage;


    private void Awake()
    {
        firePivot = GameObject.Find("Player/FirePivot");
        coolImage = GetComponent<Image>();
        
    }

    private void Start()
    {
        spell.origincooldown = spell.cooldown;
    }
    private void Update()
    {
        if (spell != null && spell.count > 0)
        {

            castTimer -= Time.deltaTime;
            castTimer = Mathf.Clamp(castTimer, 0, spell.cooldown);
            coolImage.fillAmount = castTimer / spell.cooldown;
            if (castTimer <= 0)
            {
                GameObject go = Instantiate(spell.spellPrefab, firePivot.transform.position, firePivot.transform.rotation);
                Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.down * spell.speed;
                rb.gravityScale = 0;
                AutoCast();
                castTimer = spell.cooldown;
            }
        }

    }
    public void Upgrade()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        int num = int.Parse(text.text);
        if (num >= 3)
        {
            SpellDataSO newSpe = SoceryManager.instance.GetNextGradeSpell(spell);

            SoceryManager.instance.SetSpell(newSpe);


            num -= 3;
            spell.count -= 3;

            if (num == 0)
            {
                spell.cooldown = spell.origincooldown;
                Image image = this.GetComponent<Image>();
                Color color = image.color;
                color.a = 0.3f;   // 원하는 알파값
                image.color = color;
            }
            else
            {
                spell.cooldown = spell.origincooldown;
                spell.cooldown /= num;

            }

            text.text = num.ToString();

        }

        else
            return;

    }
    private void AutoCast()
    {
        // 여기서 공격 로직 넣기
        Debug.Log($"{spell.spellName} 오토캐스팅!");

    }


}
