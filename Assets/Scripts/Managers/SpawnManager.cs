using System.Threading;
using TMPro;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.Texture2DShaderProperty;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject[] enemyPrefabs;
    public GameObject spawnTransfrom;
    public float spawnCoolTime = 0;
    public float resetSpawnCoolTime = 0.3f;
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

    private void Update()
    {
        spawnCoolTime -= Time.deltaTime;
        if (spawnCoolTime <= 0)
        {
            SpawnEnemy();
            spawnCoolTime = resetSpawnCoolTime;
        }
    }

    private void Init()
    {
        spawnTransfrom = GameObject.Find("EnemySpawner");
    }

    public void SpawnEnemy()
    {
        if (spawnCoolTime <= 0) 
        Instantiate(enemyPrefabs[0],spawnTransfrom.transform.position, spawnTransfrom.transform.rotation);
    }

}
