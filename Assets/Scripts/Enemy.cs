using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 2;
    public float speed = 2.2f;
    public int contactDamage = 1;
    public float attackCooldown = 0.8f;

    Rigidbody2D rb;
    Transform target;
    float atkCd;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    public void Init(Transform player) => target = player;

    void FixedUpdate()
    {
        if (!target) return;
        Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        rb.linearVelocity = dir * speed;
        atkCd -= Time.fixedDeltaTime;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (atkCd > 0f) return;

        var pc = col.collider.GetComponent<PlayerHealth>();
        if (pc)
        {
            pc.TakeDamage(contactDamage);
            atkCd = attackCooldown;
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            GameStats.Kills++;
            Destroy(gameObject);
        }
    }
}