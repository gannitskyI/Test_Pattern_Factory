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
        // �������� ��������� IHealth � �������, � ������� ����������� ����
        IHealth targetHealth = other.GetComponent<IHealth>();

        // ���������, ��� �������� ��������� IHealth
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage); // �������� ����� TakeDamage
        }

        ReturnToPool(); // ���������� ���� � ���
    }


    private void ReturnToPool()
    {
        // Instead of destroying, return the bullet to the object pool
        gameObject.SetActive(false);
    }
}
