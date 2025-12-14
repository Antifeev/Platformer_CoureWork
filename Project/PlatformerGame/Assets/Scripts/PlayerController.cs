using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Подключаем библиотеку для работы с Текстом

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    // Переменные для счета
    public int score = 0;              // Текущий счет
    public TextMeshProUGUI scoreText;  // Ссылка на текст на экране

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateScoreUI(); // Обновляем текст при старте
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Обработка столкновений с триггерами (Монетки и Финиш)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если коснулись монетки
        if (collision.CompareTag("Coin"))
        {
            score++; // Увеличиваем счет
            UpdateScoreUI(); // Обновляем текст
            Destroy(collision.gameObject); // Удаляем монетку со сцены
        }

        // Если коснулись финиша
        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Победа! Собрано монет: " + score);
            // Тут скоро сделаем экран победы
        }
    }

    // Функция для обновления текста
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + score;
        }
    }
}