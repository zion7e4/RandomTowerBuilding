using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public float fallLimitY = -15f; // 블록이 이 Y값 아래로 떨어지면 게임 오버

    private List<Transform> activeBlocks = new List<Transform>(); // 감시할 블록들 리스트
    private bool isMonitoring = true; // 게임 오버 발생 후에는 감시 중단

    public void RegisterBlock(Transform block)
    {
        activeBlocks.Add(block);
    }

    void Update()
    {
        if (!isMonitoring) return; // 이미 게임 오버 상태면 감시 중단

        // 모든 블록을 검사한다
        foreach (Transform block in activeBlocks)
        {
            if (block == null) continue; // 블록이 이미 파괴되었을 수도 있음

            if (block.position.y < fallLimitY)
            {
                // 기준보다 아래로 떨어졌다면 게임 오버
                isMonitoring = false; // 중복 감지 방지
                GameManager.Instance.GameOver(); // 게임 오버 호출
                return;
            }
        }
    }

    /// 스테이지 재시작 등에서 감시를 초기화할 때 호출
    public void ResetChecker()
    {
        activeBlocks.Clear(); // 감시 중이던 블록 목록 비우기
        isMonitoring = true;  // 다시 감시 시작
    }
}