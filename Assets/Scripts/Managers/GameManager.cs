using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int wave = 1;
    public float time = 15;
    public float defaultTime = 15;
    public int gold = 0;


    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI waveTxt;
    public TextMeshProUGUI goldTxt;



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
        timeTxt = GameObject.Find("UI/TimeTxt")?.GetComponent<TextMeshProUGUI>();
        waveTxt = GameObject.Find("UI/WaveInt")?.GetComponent<TextMeshProUGUI>();
        goldTxt = GameObject.Find("UI/CenterBackground/Gold").GetComponent<TextMeshProUGUI>();
        time = defaultTime;
    }


    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            wave++;
            defaultTime += 1;
            time = defaultTime;
        }

        int minutes = Mathf.FloorToInt(time / 60f);  // 분
        int seconds = Mathf.FloorToInt(time % 60f);  // 초

        waveTxt.text = wave.ToString("F0");
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        goldTxt.text = gold.ToString();
    }



}
