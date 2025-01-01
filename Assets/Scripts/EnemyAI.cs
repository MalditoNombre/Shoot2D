using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float detectionRange = 10f; // Дистанция обнаружения игрока
    public float speed = 2f; // Скорость движения ИИ
    public LayerMask obstacleLayer; // Слой, на котором находятся препятствия
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка, откуда будут вылетать снаряды
    public float shootingRange = 5f; // Дистанция, на которой ИИ может стрелять
    public float shootDelay = 1f; // Задержка между выстрелами
    public float rotationSpeed = 200f;

    private float lastShootTime; // Время последнего выстрела
    private Vector2 movementDirection; // Направление движения
    public ScoreManager scoreManager; // Ссылка на ScoreManager

    void Update()
    {
        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Если игрок в пределах досягаемости
        if (distanceToPlayer < detectionRange)
        {
            // Проверяем видимость игрока
            if (CanSeePlayer())
            {
                // Если игрок в пределах досягаемости для стрельбы
                if (distanceToPlayer < shootingRange)
                {
                    Shoot();
                }
                else
                {
                    MoveTowardsPlayer();
                }
            }
        }

        // Поворачиваемся в направлении движения
        RotateTowards(player.position);
    }

    void RotateTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized; // Направление к игроку
        if (direction != Vector2.zero) // Проверяем, что направление не нулевое
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // -90 для корректировки поворота
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Плавный поворот
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // Направление к игроку
        movementDirection = direction; // Сохраняем направление движения

        // Ограничиваем движение только по оси X или Y
        direction.y = 0; // Убираем движение по оси Y, если хотите двигаться только по X
        // direction.x = 0; // Убираем движение по оси X, если хотите двигаться только по Y

        float step = speed * Time.deltaTime; // Расстояние, которое ИИ должен пройти в этом кадре
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, step);
    }

    bool CanSeePlayer()
    {
        // Получ аем направление к игроку
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Проверяем наличие препятствий с помощью Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleLayer);
        return hit.collider == null; // Игрок виден, если hit.collider равен null
    }

    void Shoot()
    {
        // Проверяем, прошло ли достаточно времени с последнего выстрела
        if (Time.time >= lastShootTime + shootDelay)
        {
            // Создаем снаряд
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            lastShootTime = Time.time; // Обновляем время последнего выстрела

            scoreManager.IncrementAIScore(10);
        }
    }

    void OnDrawGizmos()
    {
        // Отображаем луч в редакторе для отладки
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
