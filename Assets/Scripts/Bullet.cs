using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
        public float speed = 10f; // Скорость пули
        public ScoreManager scoreManager; // Ссылка на ScoreManager

        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * speed; // Устанавливаем скорость пули
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Добавляем очки игроку
                if (scoreManager != null)
                {
                    scoreManager.IncrementPlayerScore(10);
                }
            RestartScene(); // Перезапускаем сцену
            DestroyAllProjectiles(); // Уничтожаем все снаряды
                //ResetCharacters(); // Возвращаем персонажей на точки спавна
                Destroy(gameObject); // Уничтожаем пулю
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                // Добавляем очки игроку при попадании в ИИ
                if (scoreManager != null)
                {
                    scoreManager.IncrementPlayerScore(10);
                }
            RestartScene(); // Перезапускаем сцену
            DestroyAllProjectiles(); // Уничтожаем все снаряды
                //ResetCharacters(); // Возвращаем персонажей на точки спавна
                Destroy(gameObject); // Уничтожаем пулю
            }
        }

        void DestroyAllProjectiles()
        {
            foreach (var projectile in FindObjectsOfType<Bullet>())
            {
                Destroy(projectile.gameObject);
            }
        }
    /*
        void ResetCharacters()
        {
            // Здесь вы можете вызвать метод для возврата персонажей на точки спавна
            // Например, если у вас есть ссылки на персонажей, вы можете сделать так:
            PlayerController player = FindObjectOfType<PlayerController>();
            EnemyAI enemy = FindObjectOfType<EnemyAI>();

            if (player != null)
            {
                player.transform.position = player.spawnPoint; // Предполагается, что у вас есть переменная spawnPoint
            }

            if (enemy != null)
            {
                enemy.transform.position = enemy.spawnPoint; // Предполагается, что у вас есть переменная spawnPoint
            }
        }
    */
    void RestartScene()
    {
        // Получаем имя текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneName);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DestroyZone")) // Убедитесь, что у границ установлен тег "DestroyZone"
        {
            Destroy(gameObject); // Уничтожаем снаряд
        }
    }
}
 
