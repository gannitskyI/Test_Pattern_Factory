using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootRadius = 5f;
    [SerializeField] private float shootInterval = 1f;
    [SerializeField] private int bulletDamage = 10;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletLifetime = 2f;

    private float shootTimer;
    private Enemy nearestEnemy;
    private List<Enemy> enemies;

    private List<GameObject> bulletPool;

    void Start()
    {
        enemies = new List<Enemy>();
        bulletPool = new List<GameObject>();
        Enemy.OnEnemyDestroyed += HandleEnemyDestroyed;  // Подписываемся на событие
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDestroyed -= HandleEnemyDestroyed;  // Отписываемся от события
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Shoot();
            }
            shootTimer = shootInterval;
        }
    }

    private void FindNearestEnemy()
    {
        if (nearestEnemy == null || !nearestEnemy.isActiveAndEnabled)
        {
            nearestEnemy = null;
            float nearestDistance = shootRadius * shootRadius;

            foreach (var enemy in enemies)
            {
                if (enemy == null || !enemy.isActiveAndEnabled)
                    continue;

                float distance = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = GetPooledBullet();
        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab);
        }

        bullet.transform.position = transform.position;
        bullet.SetActive(true);

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.Initialize(bulletSpeed, bulletDamage, bulletLifetime);
        }
    }

    private GameObject GetPooledBullet()
    {
        foreach (var bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }


        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        RemoveEnemyFromList(enemy);
    }
}
