using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager; // Ссылка на ScoreManager
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float minX, maxX, minY, maxY;

    private Rigidbody2D rb; // Ссылка на Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
    }

    void Update()
    {
        Aim();
        Shoot();
    }

    void FixedUpdate()
    {
        Move(); // Перемещение вызываем в FixedUpdate для физики
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveX, moveY) * moveSpeed * Time.fixedDeltaTime;

        // Перемещаем игрока с помощью Rigidbody2D
        Vector2 newPosition = rb.position + move;

        // Ограничиваем движение игрока в заданной области
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rb.MovePosition(newPosition); // Устанавливаем новую позицию
    }

    void Aim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        transform.up = direction; // Поворачиваем персонажа в сторону мыши
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) // Проверка нажатия кнопки стрельбы
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Создание пули
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.scoreManager = scoreManager; // Передача ссылки на ScoreManager
        }
    }
}
