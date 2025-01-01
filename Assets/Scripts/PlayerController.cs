using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager; // ������ �� ScoreManager
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float minX, maxX, minY, maxY;

    private Rigidbody2D rb; // ������ �� Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D
    }

    void Update()
    {
        Aim();
        Shoot();
    }

    void FixedUpdate()
    {
        Move(); // ����������� �������� � FixedUpdate ��� ������
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveX, moveY) * moveSpeed * Time.fixedDeltaTime;

        // ���������� ������ � ������� Rigidbody2D
        Vector2 newPosition = rb.position + move;

        // ������������ �������� ������ � �������� �������
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rb.MovePosition(newPosition); // ������������� ����� �������
    }

    void Aim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        transform.up = direction; // ������������ ��������� � ������� ����
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) // �������� ������� ������ ��������
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // �������� ����
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.scoreManager = scoreManager; // �������� ������ �� ScoreManager
        }
    }
}
