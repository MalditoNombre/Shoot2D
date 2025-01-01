using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
        public float speed = 10f; // �������� ����
        public ScoreManager scoreManager; // ������ �� ScoreManager

        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * speed; // ������������� �������� ����
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // ��������� ���� ������
                if (scoreManager != null)
                {
                    scoreManager.IncrementPlayerScore(10);
                }
            RestartScene(); // ������������� �����
            DestroyAllProjectiles(); // ���������� ��� �������
                //ResetCharacters(); // ���������� ���������� �� ����� ������
                Destroy(gameObject); // ���������� ����
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                // ��������� ���� ������ ��� ��������� � ��
                if (scoreManager != null)
                {
                    scoreManager.IncrementPlayerScore(10);
                }
            RestartScene(); // ������������� �����
            DestroyAllProjectiles(); // ���������� ��� �������
                //ResetCharacters(); // ���������� ���������� �� ����� ������
                Destroy(gameObject); // ���������� ����
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
            // ����� �� ������ ������� ����� ��� �������� ���������� �� ����� ������
            // ��������, ���� � ��� ���� ������ �� ����������, �� ������ ������� ���:
            PlayerController player = FindObjectOfType<PlayerController>();
            EnemyAI enemy = FindObjectOfType<EnemyAI>();

            if (player != null)
            {
                player.transform.position = player.spawnPoint; // ��������������, ��� � ��� ���� ���������� spawnPoint
            }

            if (enemy != null)
            {
                enemy.transform.position = enemy.spawnPoint; // ��������������, ��� � ��� ���� ���������� spawnPoint
            }
        }
    */
    void RestartScene()
    {
        // �������� ��� ������� �����
        string currentSceneName = SceneManager.GetActiveScene().name;
        // ������������� �����
        SceneManager.LoadScene(currentSceneName);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DestroyZone")) // ���������, ��� � ������ ���������� ��� "DestroyZone"
        {
            Destroy(gameObject); // ���������� ������
        }
    }
}
 
