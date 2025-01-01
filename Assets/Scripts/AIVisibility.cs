using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVisibility : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float detectionRange = 10f; // Дистанция обнаружения игрока
    public LayerMask obstacleLayer; // Слой, на котором находятся препятствия

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
                Debug.Log("Игрок виден!");
                // Здесь можно добавить логику для движения к игроку или стрельбы
            }
            else
            {
                Debug.Log("Игрок не виден из-за препятствия.");
            }
        }
    }

    bool CanSeePlayer()
    {
        // Получаем направление к игроку
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Проверяем наличие препятствий с помощью Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleLayer);

        // Если луч не попал ни во что, значит игрок виден
        return hit.collider == null; // Игрок виден, если hit.collider равен null
    }

    void OnDrawGizmos()
    {
        // Отображаем луч в редакторе для отладки
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
