using UnityEngine;
using UnityEngine.UI;

public class SoceryManager : MonoBehaviour
{
    public static SoceryManager instance;


    public GameObject[] soceries;
    public Button GachaSocery;

    public float soceryTimer = 0;
    public GameObject player;

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
    }

    private void Update()
    {
        soceryTimer -= Time.deltaTime;
        if (soceryTimer < 0)
        {
           UseSocery();
            soceryTimer = 0.2f;

        }
    }

    private void UseSocery()
    {
        if(soceryTimer <= 0)
        Instantiate(soceries[0], player.transform.position, player.transform.rotation);
    }

}
