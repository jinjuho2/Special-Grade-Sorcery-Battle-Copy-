using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoceryManager : MonoBehaviour
{
    public static SoceryManager instance;


    public SpellDataSO[] SpellDataSO;
    public Button GachaSocery;
    public Transform slotParent;

    public List<SpellDataSO> spellList = new List<SpellDataSO>();
    public float soceryTimer = 0;
    public GameObject player;

    public TextMeshProUGUI gachaTxt;
    public int gachaGold = 15;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        player = GameObject.Find("Player");
        GachaSocery = GameObject.Find("UI/SpawnButton/SpawnSocery").GetComponent<Button>();
        slotParent = GameObject.Find("UI/SoceryBackground").transform;
        gachaTxt = GameObject.Find("UI/SpawnButton/SpawnSocery/CoinTxt").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        gachaTxt.text = gachaGold.ToString();
    }




    public void CreatSpell()
    {
        if (GameManager.instance.gold < gachaGold) return;

            GameManager.instance.gold -= gachaGold;
            int weight = 0;
            foreach (SpellDataSO spell in SpellDataSO)
            {
                weight += spell.weight;
            }

            int randomValue = Random.Range(0, weight);


            SpellDataSO selectedSpell = null;

            int currentWeight = 0;

            foreach (SpellDataSO spell in SpellDataSO)
            {
                currentWeight += spell.weight;
                if (randomValue < currentWeight)
                {
                    selectedSpell = spell;
                    break;
                }
            }



            foreach (Transform slot in slotParent)
            {
                SpellSlot check = slot.GetComponent<SpellSlot>();
                if (check.spell == selectedSpell)
                {
                    TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
                    int num = int.Parse(text.text);
                    if (num == 0)
                    {

                        Image image = slot.GetComponent<Image>();
                        Color color = image.color;
                        color.a = 1f;   // 원하는 알파값
                        image.color = color;
                    }
                    selectedSpell.count++;
                    
                    num++;
                selectedSpell.cooldown /= num;
                    
                    text.text = num.ToString();

                }
            }

        gachaGold++;
        
    }

    public SpellDataSO GachaSpell()
    {
        int weight = 0;
        foreach (SpellDataSO spell in SpellDataSO)
        {
            weight += spell.weight;
        }

        int randomValue = Random.Range(0, weight);



        int currentWeight = 0;

        foreach (SpellDataSO spell in SpellDataSO)
        {
            currentWeight += spell.weight;
            if (randomValue < currentWeight)
            {
                return spell;
            }
        }
        return SpellDataSO[SpellDataSO.Length - 1];

    }

    public void SetSpell(SpellDataSO spell)
    {
        foreach (Transform slot in slotParent)
        {
            SpellSlot check = slot.GetComponent<SpellSlot>();
            if (check.spell == spell)
            {
                TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
                int num = int.Parse(text.text);
                if (num == 0)
                {
                    Image image = slot.GetComponent<Image>();
                    Color color = image.color;
                    color.a = 1f;   // 원하는 알파값
                    image.color = color;
                }
                spell.count++;
                num++;
                text.text = num.ToString();

            }
        }
    }

    public SpellDataSO GetNextGradeSpell(SpellDataSO currentSpell)
    {
        // 예시: weight 기준으로 다음 등급 스펠 찾기
        foreach(SpellDataSO spellDataSOs in SpellDataSO) 
        {
            if (spellDataSOs.weight < currentSpell.weight)
            {
                return spellDataSOs;
            }
        }
        // 만약 최고 등급이면 null 반환
        return null;
    }
}
