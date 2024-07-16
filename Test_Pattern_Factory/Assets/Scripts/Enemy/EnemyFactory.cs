using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private GameObject enemyPrefab;

    public EnemyFactory(GameObject enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;
    }

    public GameObject CreateEnemy(Vector3 spawnPosition, float speed, int health, PlayerHealth player)
    {
        GameObject enemyObject = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        IEnemy enemy = enemyObject.GetComponentInChildren<IEnemy>();
        if (enemy != null)
        {
            enemy.Initialize(speed, health, player);
        }
        return enemyObject;
    }
}
