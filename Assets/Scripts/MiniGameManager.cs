using UnityEngine;
using System.Collections;
using TMPro;


public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] Clouds;
    [SerializeField] private TextMeshProUGUI TimerNum;
    [SerializeField] private Vector3 outOfFramePoint = new Vector3(-10f,0,0);
    [SerializeField] private Vector3 spawnPointRunningEnemy = new Vector3(10f, -2.8f, 0f);
    [SerializeField] private Vector3 spawnPointFlyingEnemy = new Vector3(10f, 0.88f, 0f);
    [SerializeField] private Vector3 spawnPointCloud = new Vector3(10f, 2.5f, 0f);
    private int[] enemyQueue = { 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1 }; //13
    private float enemySpawnTimePeriod = 1.5f;
    private float cloudSpawnTimePeriod = 3.5f;
    private float nextEnemySpawnTime;
    private float nextCloudSpawnTime;
    private int enemyCount;
    private int cloudCount;
    private float timer;
    private int timerMinutes;
    private int timerSeconds;


    private void Start()
    {
        timer = 0;
        timerMinutes = 0;
        timerSeconds = 0;
        enemyCount = 0;
        cloudCount = 0;
        nextEnemySpawnTime = enemySpawnTimePeriod; //start spawning enemies now
        nextCloudSpawnTime = cloudSpawnTimePeriod; //start spawning clouds now
    }

    private void Update()
    {
        SpawnEnemy();
        SpawnCloud();
        UpdateTimer();
    }

    private void SpawnEnemy()
    {
        nextEnemySpawnTime += Time.deltaTime;
        if (nextEnemySpawnTime >= enemySpawnTimePeriod)
        {
            nextEnemySpawnTime = 0;
            Enemy enemyScript;
            if (enemyQueue[enemyCount % enemyQueue.Length] == 0)
            {
                enemyScript = Instantiate(Enemies[enemyQueue[enemyCount % enemyQueue.Length]], spawnPointRunningEnemy, Quaternion.identity).GetComponent<Enemy>();
            }
            else //(enemyQueue[enemyCount % enemiesAmount] == 1)
            {
                enemyScript = Instantiate(Enemies[enemyQueue[enemyCount % enemyQueue.Length]], spawnPointFlyingEnemy, Quaternion.identity).GetComponent<Enemy>();
            }
            enemyScript.Init(outOfFramePoint);
            enemyCount++;
        }
    }

    private void SpawnCloud()
    {
        nextCloudSpawnTime += Time.deltaTime;
        if (nextCloudSpawnTime >= cloudSpawnTimePeriod)
        {
            nextCloudSpawnTime = 0;
            Cloud cloudScript = Instantiate(Clouds[cloudCount % Clouds.Length], spawnPointCloud, Quaternion.identity).GetComponent<Cloud>();
            cloudScript.Init(outOfFramePoint);
            cloudCount++;
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        timerMinutes = (int)(timer / 60f);
        timerSeconds = (int)(timer % 60f);
        TimerNum.text = $"{timerMinutes}:{timerSeconds:00}";
    }

    public void Loss()
    {
        Debug.Log("Loss");
    }
}
