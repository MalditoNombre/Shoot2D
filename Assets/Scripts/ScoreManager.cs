using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text playerScoreText; // Ссылка на компонент UI Text для игрока
    public Text aiScoreText; // Ссылка на компонент UI Text для ИИ
    private int playerScore; // Переменная для хранения очков игрока
    private int aiScore; // Переменная для хранения очков ИИ

    void Start()
    {
        playerScore = 0; // Инициализация очков игрока
        aiScore = 0; // Инициализация очков ИИ
        UpdateScoreText(); // Обновление UI
    }

    // Метод для увеличения очков игрока
    public void IncrementPlayerScore(int points)
    {
        playerScore += points; // Увеличение очков игрока
        UpdateScoreText(); // Обновление UI
    }

    // Метод для увеличения очков ИИ
    public void IncrementAIScore(int points)
    {
        aiScore += points; // Увеличение очков ИИ
        UpdateScoreText(); // Обновление UI
    }

    // Метод для обновления отображения очков
    private void UpdateScoreText()
    {
        playerScoreText.text = "Очки игрока: " + playerScore; // Обновление текста очков игрока
        aiScoreText.text = "Очки ИИ: " + aiScore; // Обновление текста очков ИИ
    }
}
