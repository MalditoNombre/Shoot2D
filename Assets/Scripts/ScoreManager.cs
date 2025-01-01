using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text playerScoreText; // ������ �� ��������� UI Text ��� ������
    public Text aiScoreText; // ������ �� ��������� UI Text ��� ��
    private int playerScore; // ���������� ��� �������� ����� ������
    private int aiScore; // ���������� ��� �������� ����� ��

    void Start()
    {
        playerScore = 0; // ������������� ����� ������
        aiScore = 0; // ������������� ����� ��
        UpdateScoreText(); // ���������� UI
    }

    // ����� ��� ���������� ����� ������
    public void IncrementPlayerScore(int points)
    {
        playerScore += points; // ���������� ����� ������
        UpdateScoreText(); // ���������� UI
    }

    // ����� ��� ���������� ����� ��
    public void IncrementAIScore(int points)
    {
        aiScore += points; // ���������� ����� ��
        UpdateScoreText(); // ���������� UI
    }

    // ����� ��� ���������� ����������� �����
    private void UpdateScoreText()
    {
        playerScoreText.text = "���� ������: " + playerScore; // ���������� ������ ����� ������
        aiScoreText.text = "���� ��: " + aiScore; // ���������� ������ ����� ��
    }
}
