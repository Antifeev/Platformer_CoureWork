using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;        // Скорость
    public float distance = 3f;     // Дальность прогулки

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position; // Запоминаем точку старта
    }

    void Update()
    {
        // Вычисляем границы
        float rightLimit = startPos.x + distance;
        float leftLimit = startPos.x - distance;

        // Двигаем врага
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit) movingRight = false; // Разворот влево
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit) movingRight = true; // Разворот вправо
        }
    }
}