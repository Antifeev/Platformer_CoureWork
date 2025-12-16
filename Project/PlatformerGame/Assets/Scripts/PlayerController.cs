using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Звуки")]
    public AudioSource audioSource;
    public AudioClip jumpSound;     // Прыжок
    public AudioClip coinSound;     // Монетка
    public AudioClip hitSound;      // Удар врага
    public AudioClip deathSound;    // Смерть
    public AudioClip winSound;      // ЗВУК ПОБЕДЫ

    [Header("Настройки игрока")]
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGameStopped = false; // Блокировка управления (при смерти или победе)

    [Header("UI")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateScoreUI();
    }

    void Update()
    {
        // Если игра остановлена (умерли или победили) — кнопки не работают
        if (isGameStopped) return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                PlaySound(jumpSound);
            }
        }

        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGameStopped) return;

        if (collision.CompareTag("Coin"))
        {
            PlaySound(coinSound);
            score++;
            UpdateScoreUI();
            Destroy(collision.gameObject);
        }

        // --- ЛОГИКА ФИНИША ---
        if (collision.CompareTag("Finish"))
        {
            WinGame(); // Запускаем победу
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameStopped) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y > 0.5f)
                {
                    PlaySound(hitSound);
                    Destroy(collision.gameObject);
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce / 1.5f);
                    return;
                }
            }
            Die();
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    // --- ФУНКЦИЯ ПОБЕДЫ ---
    void WinGame()
    {
        if (isGameStopped) return;
        isGameStopped = true; // Блокируем игрока

        // Останавливаем движение
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        // Играем победный звук
        PlaySound(winSound);

        // Ждем 1.5 секунды и грузим экран победы
        Invoke("LoadWinScene", 1.5f);
    }

    void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    // --- ФУНКЦИЯ СМЕРТИ ---
    void Die()
    {
        if (isGameStopped) return;
        isGameStopped = true;

        PlaySound(deathSound);

        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        Invoke("RestartLevel", 1f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Coins: " + score;
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null) audioSource.PlayOneShot(clip);
    }
}