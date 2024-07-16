using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateEnemy(Vector3 spawnPosition, float speed, int health, PlayerHealth player);
}
