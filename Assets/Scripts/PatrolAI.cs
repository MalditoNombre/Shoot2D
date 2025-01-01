using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Массив точек патрулирования
    public float speed = 2f; // Скорость движения ИИ
    public float waitTime = 1f; // Время ожидания на каждой точке

    private int currentPointIndex = 0; // Индекс текущей точки патрулирования
    private float waitTimer; // Таймер ожидания

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Если есть точки патрулирования
        if (patrolPoints.Length > 0)
        {
            // Двигаемся к текущей точке патрулирования
            Transform targetPoint = patrolPoints[currentPointIndex];
            float step = speed * Time.deltaTime; // Расстояние, которое ИИ должен пройти в этом кадре

            // Перемещаем ИИ к текущей точке
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, step);

            // Проверяем, достигли ли мы текущей точки
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                // Увеличиваем таймер ожидания
                waitTimer += Time.deltaTime;

                // Если время ожидания истекло, переходим к следующей точке
                if (waitTimer >= waitTime)
                {
                    waitTimer = 0f; // Сбрасываем таймер
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length; // Переход к следующей точке
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Отображаем точки патрулирования в редакторе
        Gizmos.color = Color.green;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);
            if (i > 0)
            {
                Gizmos.DrawLine(patrolPoints[i - 1].position, patrolPoints[i].position);
            }
        }
    }
}
