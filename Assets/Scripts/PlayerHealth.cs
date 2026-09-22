using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 5;
    public PlayerController2D controller;
    public GameOverUI gameOverUI;

    private bool isDead = false;

    public void TakeDamage(int dmg)
    {
        if (isDead) return;
        if (controller && controller.IsInvulnerable()) return;

        hp -= dmg;
        if (hp < 0) hp = 0; // зажимаем, чтобы не было минуса

        if (hp == 0)
            Die();
    }

    void Die()
    {
        isDead = true;

        GameStats.Reset();
        gameOverUI.ShowGameOver();

        // важно: отключаем возможность получать урон дальше
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        // если есть Rigidbody2D — можно остановить
        var rb = GetComponent<Rigidbody2D>();
        if (rb) rb.linearVelocity = Vector2.zero;

        // по желанию выключить управление (лучше, чем SetActive если хочешь анимацию)
        // controller.enabled = false;
    }
}