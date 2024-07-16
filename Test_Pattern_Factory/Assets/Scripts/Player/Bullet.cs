using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private int damage;
    private float lifetime;
    private float timeSinceSpawned;

    public void Initialize(float speed, int damage, float lifetime)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.timeSinceSpawned = 0f;
 
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);

        timeSinceSpawned += Time.fixedDeltaTime;

        // Destroy bullet if lifetime expires
        if (timeSinceSpawned >= lifetime)
        {
            ReturnToPool();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Получаем компонент IHealth с объекта, с которым столкнулась пуля
        IHealth targetHealth = other.GetComponent<IHealth>();

        // Проверяем, что получили компонент IHealth
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage); // Вызываем метод TakeDamage
        }

        ReturnToPool(); // Возвращаем пулю в пул
    }


    private void ReturnToPool()
    {
        // Instead of destroying, return the bullet to the object pool
        gameObject.SetActive(false);
    }
}
