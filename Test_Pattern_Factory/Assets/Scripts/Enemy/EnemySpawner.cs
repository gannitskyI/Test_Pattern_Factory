using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 3f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private int enemyHealth = 100;
    [SerializeField] private int initialPoolSize = 10;

    private IEnemyFactory enemyFactory;
    private Queue<GameObject> enemyPool;
    private PlayerShooter playerShooter;

    private void Awake()
    {
        enemyFactory = new EnemyFactory(enemyPrefab);
        enemyPool = new Queue<GameObject>(initialPoolSize);
        playerShooter = FindObjectOfType<PlayerShooter>();
    }

    private void Start()
    {
        InitializeEnemyPool();
        StartCoroutine(SpawnEnemies());
    }

    private void InitializeEnemyPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject enemy = enemyFactory.CreateEnemy(Vector3.zero, 0, 0, playerShooter.GetComponent<PlayerHealth>());
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);

            playerShooter.AddEnemyToList(enemy.GetComponent<Enemy>());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
            float speed = Random.Range(minSpeed, maxSpeed);

            SpawnEnemy(spawnPosition, speed, enemyHealth);
        }
    }

    private void SpawnEnemy(Vector3 spawnPosition, float speed, int health)
    {
        GameObject enemy;
        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue();
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
        }
        else
        {
            enemy = enemyFactory.CreateEnemy(spawnPosition, speed, health, playerShooter.GetComponent<PlayerHealth>());
        }

        IEnemy enemyComponent = enemy.GetComponentInChildren<IEnemy>();
        if (enemyComponent != null)
        {
            enemyComponent.Initialize(speed, health, playerShooter.GetComponent<PlayerHealth>());
        }

        playerShooter.AddEnemyToList(enemy.GetComponent<Enemy>());
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);

        playerShooter.RemoveEnemyFromList(enemy.GetComponent<Enemy>());
    }
}
