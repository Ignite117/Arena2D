using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnEvery = 2.0f;
    public int maxAlive = 12;
    public float radius = 8f;

    float t;

    void Update()
    {
        if (!enemyPrefab || !player) return;

        t += Time.deltaTime;
        if (t < spawnEvery) return;
        t = 0f;

        if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length >= maxAlive) return;

        Vector2 pos = (Vector2)player.position + Random.insideUnitCircle.normalized * radius;
        var e = Instantiate(enemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>();
        e.Init(player);
    }
}
