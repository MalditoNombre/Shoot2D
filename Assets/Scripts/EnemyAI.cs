using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public float detectionRange = 10f; // ��������� ����������� ������
    public float speed = 2f; // �������� �������� ��
    public LayerMask obstacleLayer; // ����, �� ������� ��������� �����������
    public GameObject projectilePrefab; // ������ �������
    public Transform firePoint; // �����, ������ ����� �������� �������
    public float shootingRange = 5f; // ���������, �� ������� �� ����� ��������
    public float shootDelay = 1f; // �������� ����� ����������
    public float rotationSpeed = 200f;

    private float lastShootTime; // ����� ���������� ��������
    private Vector2 movementDirection; // ����������� ��������
    public ScoreManager scoreManager; // ������ �� ScoreManager

    void Update()
    {
        // ��������� ���������� �� ������
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ���� ����� � �������� ������������
        if (distanceToPlayer < detectionRange)
        {
            // ��������� ��������� ������
            if (CanSeePlayer())
            {
                // ���� ����� � �������� ������������ ��� ��������
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

        // �������������� � ����������� ��������
        RotateTowards(player.position);
    }

    void RotateTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized; // ����������� � ������
        if (direction != Vector2.zero) // ���������, ��� ����������� �� �������
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // -90 ��� ������������� ��������
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // ������� �������
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // ����������� � ������
        movementDirection = direction; // ��������� ����������� ��������

        // ������������ �������� ������ �� ��� X ��� Y
        direction.y = 0; // ������� �������� �� ��� Y, ���� ������ ��������� ������ �� X
        // direction.x = 0; // ������� �������� �� ��� X, ���� ������ ��������� ������ �� Y

        float step = speed * Time.deltaTime; // ����������, ������� �� ������ ������ � ���� �����
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, step);
    }

    bool CanSeePlayer()
    {
        // ����� ��� ����������� � ������
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // ��������� ������� ����������� � ������� Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleLayer);
        return hit.collider == null; // ����� �����, ���� hit.collider ����� null
    }

    void Shoot()
    {
        // ���������, ������ �� ���������� ������� � ���������� ��������
        if (Time.time >= lastShootTime + shootDelay)
        {
            // ������� ������
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            lastShootTime = Time.time; // ��������� ����� ���������� ��������

            scoreManager.IncrementAIScore(10);
        }
    }

    void OnDrawGizmos()
    {
        // ���������� ��� � ��������� ��� �������
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
