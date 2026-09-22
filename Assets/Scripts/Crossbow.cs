using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public PlayerController2D player;
    public Transform firePoint;
    public GameObject boltPrefab;

    [Header("Tuning")]
    public float boltSpeed = 14f;
    public float fireCooldown = 0.75f;     // медленно, по-соулсу
    public float reloadTime = 0.55f;       // Увзвешенна€Ф перезар€дка
    public float staminaCost = 12f;

    float cd;
    bool reloading;
    float reloadTimer;

    void Update()
    {
        cd -= Time.deltaTime;

        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f) reloading = false;
            return;
        }

        if (Input.GetMouseButtonDown(0) && cd <= 0f)
        {
            if (player && !player.TrySpendStamina(staminaCost)) return;

            Shoot();
            cd = fireCooldown;
            reloading = true;
            reloadTimer = reloadTime;
        }
    }

    void Shoot()
    {
        if (!boltPrefab || !firePoint) return;

        var bolt = Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
        var rb = bolt.GetComponent<Rigidbody2D>();
        if (rb)
            rb.linearVelocity = (Vector2)firePoint.right * boltSpeed;
    }
}