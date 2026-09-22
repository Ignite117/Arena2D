using UnityEngine;

public class Bolt : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 2.0f;

    void Start() => Destroy(gameObject, lifeTime);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Если попали в препятствие
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            Destroy(gameObject);
    }
}