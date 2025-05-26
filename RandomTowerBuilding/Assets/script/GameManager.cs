using UnityEngine;

// 전체 게임의 흐름을 관리하는 스크립트
public class GameManager : MonoBehaviour
{
    public BlockManager blockManager; // 블럭 관리자 참조

    void Start()
    {
        SpawnAndListen(); // 첫 블럭 생성
    }

    void Update()
    {
        Block current = blockManager.GetCurrentBlock();

        if (current != null)
        {
            // 좌우 이동 (0.5 유닛씩)
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                current.Move(Vector2.left * 0.5f);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                current.Move(Vector2.right * 0.5f);

            // 회전
            if (Input.GetKeyDown(KeyCode.UpArrow))
                current.Rotate(15f); // 반시계 방향

            if (Input.GetKeyDown(KeyCode.DownArrow))
                current.Rotate(-15f); // 시계 방향

            // 낙하 (물리 활성화)
            if (Input.GetKeyDown(KeyCode.Return))
                blockManager.DropCurrentBlock();
        }
    }

    // 블럭 생성 + 정지 감지 리스너 등록
    void SpawnAndListen()
    {
        Block newBlock = blockManager.SpawnBlock();
        newBlock.OnStopped = HandleBlockStopped;
    }

    // 블럭이 정지되면 다음 블럭 생성
    void HandleBlockStopped(Block block)
    {
        blockManager.ClearCurrentBlock();
        Invoke(nameof(SpawnAndListen), 0.5f);
    }
}