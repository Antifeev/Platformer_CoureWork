using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;       // Скорость бега
    public float jumpForce = 10f;  // Сила прыжка
    private Rigidbody2D rb;        // Ссылка на физику

    void Start()
    {
        // Находим компонент физики на персонаже
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Движение влево-вправо (A/D или стрелки)
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // 2. Прыжок (Пробел)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Прыгаем, только если вертикальная скорость почти ноль (значит, стоим на земле)
            if (Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}