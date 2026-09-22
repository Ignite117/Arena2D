using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 3.2f;
    public Rigidbody2D rb;

    [Header("Aim")]
    public Transform aimPivot; // пустой объект у игрока, вращаем его на мышь

    [Header("Stamina")]
    public float staminaMax = 100f;
    public float staminaRegenPerSec = 22f;
    public float rollCost = 35f;
    public float shootCost = 12f;

    [Header("Roll")]
    public float rollSpeed = 8.5f;
    public float rollDuration = 0.25f;
    public float rollIFrames = 0.20f;

    Vector2 moveInput;
    Vector2 lastMoveDir = Vector2.right;
    float stamina;
    bool isRolling;
    float rollTimer;
    float iFrameTimer;

    void Awake()
    {
        stamina = staminaMax;
        if (!rb) rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveInput.sqrMagnitude > 0.01f) lastMoveDir = moveInput;

        AimAtMouse();

        // Roll
        if (!isRolling && Input.GetKeyDown(KeyCode.Space) && stamina >= rollCost)
        {
            stamina -= rollCost;
            isRolling = true;
            rollTimer = rollDuration;
            iFrameTimer = rollIFrames;
        }

        // Regen stamina (простое правило)
        stamina = Mathf.Min(staminaMax, stamina + staminaRegenPerSec * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (isRolling)
        {
            rb.linearVelocity = lastMoveDir * rollSpeed;
            rollTimer -= Time.fixedDeltaTime;
            iFrameTimer -= Time.fixedDeltaTime;
            if (rollTimer <= 0f)
            {
                isRolling = false;
            }
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
    }

    void AimAtMouse()
    {
        if (!aimPivot) return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouse - aimPivot.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        aimPivot.rotation = Quaternion.Euler(0, 0, angle);
    }

    public bool TrySpendStamina(float cost)
    {
        if (stamina < cost) return false;
        stamina -= cost;
        return true;
    }

    public bool IsInvulnerable()
    {
        return isRolling && iFrameTimer > 0f;
    }

    public float Stamina01() => stamina / staminaMax;
}
