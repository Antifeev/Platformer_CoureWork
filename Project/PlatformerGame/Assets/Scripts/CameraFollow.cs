using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Ссылка на игрока
    public Vector3 offset;         // Смещение (чтобы камера не влезала внутрь игрока)

    void Start()
    {
        // Автоматически вычисляем разницу координат между камерой и игроком
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Плавно двигаем камеру за игроком (только по X и Y)
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}