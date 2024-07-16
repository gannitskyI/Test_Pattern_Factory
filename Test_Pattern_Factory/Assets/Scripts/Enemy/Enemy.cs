using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, IHealth
{
    public static event System.Action<Enemy> OnEnemyDestroyed;

    private float speed;
    private int health;
    private EnemySpawner enemySpawner;
    private PlayerHealth playerHealth;

    public void Initialize(float speed, int health, PlayerHealth player)
    {
        this.speed = speed;
        this.health = health;
        this.playerHealth = player;
    }

    void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void CheckBounds()
    {
        if (transform.position.y <= -5.6f)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            if (enemySpawner != null)
            {
                enemySpawner.ReturnEnemyToPool(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (OnEnemyDestroyed != null)
            {
                OnEnemyDestroyed(this);  // Передаем текущий объект врага
            }
        }
    }
}
