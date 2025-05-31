using System.Collections.Generic;
using UnityEngine;

/// ��ü ������ �帧�� ��� ����, ���� ���� ������ �����ϴ� ��ũ��Ʈ.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // --- ������ ---
    public GameObject gameOverUI;

    // --- ���� ���� ���� ---
    private bool isGameOver = false;
    private List<Transform> activeBlocks = new(); // ���� ��� ��ϵ�
    public float fallLimitY = -5f;                // Y�� ���ؼ�. �Ʒ��� �������� ���� ó����

    void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    void Update()
    {
        if (!isGameOver)
            MonitorBlocksFall();
    }


    /// ����� ���� ��� �߰���
    public void RegisterBlock(Transform block)
    {
        activeBlocks.Add(block);
    }

    /// ��� ���� ���� ����� �˻��ؼ� ȭ�� �Ʒ��� ���������� Ȯ��
    void MonitorBlocksFall()
    {
        foreach (Transform block in activeBlocks)
        {
            if (block == null) continue;

            if (block.position.y < fallLimitY)
            {
                GameOver();
                break;
            }
        }
    }
    /// ���� ���� ó�� �Լ�. �� �� ���� �����.
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("���� ����!");

        Time.timeScale = 0; // ���� ����

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    /// ���� ���� üũ �ʱ�ȭ
    public void ResetGameOverSystem()
    {
        activeBlocks.Clear();
        isGameOver = false;
    }
}