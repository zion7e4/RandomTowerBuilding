using System.Collections.Generic;
using UnityEngine;

/// 전체 게임의 흐름과 블록 조작, 게임 오버 조건을 관리하는 스크립트.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // --- 참조들 ---
    public GameObject gameOverUI;

    // --- 내부 상태 변수 ---
    private bool isGameOver = false;
    private List<Transform> activeBlocks = new(); // 감시 대상 블록들
    public float fallLimitY = -5f;                // Y값 기준선. 아래로 떨어지면 오버 처리됨

    void Awake()
    {
        // 싱글턴 초기화
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    void Update()
    {
        if (!isGameOver)
            MonitorBlocksFall();
    }


    /// 블록을 감시 대상에 추가함
    public void RegisterBlock(Transform block)
    {
        activeBlocks.Add(block);
    }

    /// 모든 감시 중인 블록을 검사해서 화면 아래로 떨어졌는지 확인
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
    /// 게임 오버 처리 함수. 단 한 번만 실행됨.
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("게임 오버!");

        Time.timeScale = 0; // 게임 멈춤

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    /// 게임 오버 체크 초기화
    public void ResetGameOverSystem()
    {
        activeBlocks.Clear();
        isGameOver = false;
    }
}