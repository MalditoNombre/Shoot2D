using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVisibility : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public float detectionRange = 10f; // ��������� ����������� ������
    public LayerMask obstacleLayer; // ����, �� ������� ��������� �����������

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
                Debug.Log("����� �����!");
                // ����� ����� �������� ������ ��� �������� � ������ ��� ��������
            }
            else
            {
                Debug.Log("����� �� ����� ��-�� �����������.");
            }
        }
    }

    bool CanSeePlayer()
    {
        // �������� ����������� � ������
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // ��������� ������� ����������� � ������� Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, obstacleLayer);

        // ���� ��� �� ����� �� �� ���, ������ ����� �����
        return hit.collider == null; // ����� �����, ���� hit.collider ����� null
    }

    void OnDrawGizmos()
    {
        // ���������� ��� � ��������� ��� �������
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
