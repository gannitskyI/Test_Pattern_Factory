public interface IEnemy
{
    void Initialize(float speed, int health, PlayerHealth player);
    void TakeDamage(int amount);
}
