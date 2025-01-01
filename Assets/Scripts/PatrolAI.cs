using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public Transform[] patrolPoints; // ������ ����� ��������������
    public float speed = 2f; // �������� �������� ��
    public float waitTime = 1f; // ����� �������� �� ������ �����

    private int currentPointIndex = 0; // ������ ������� ����� ��������������
    private float waitTimer; // ������ ��������

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // ���� ���� ����� ��������������
        if (patrolPoints.Length > 0)
        {
            // ��������� � ������� ����� ��������������
            Transform targetPoint = patrolPoints[currentPointIndex];
            float step = speed * Time.deltaTime; // ����������, ������� �� ������ ������ � ���� �����

            // ���������� �� � ������� �����
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, step);

            // ���������, �������� �� �� ������� �����
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                // ����������� ������ ��������
                waitTimer += Time.deltaTime;

                // ���� ����� �������� �������, ��������� � ��������� �����
                if (waitTimer >= waitTime)
                {
                    waitTimer = 0f; // ���������� ������
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length; // ������� � ��������� �����
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // ���������� ����� �������������� � ���������
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
