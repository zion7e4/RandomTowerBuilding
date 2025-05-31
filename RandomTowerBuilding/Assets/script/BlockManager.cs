using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public List<BlockData> blockPool; // ScriptableObject 리스트
    public Transform spawnPoint; // 블럭 생성 위치

    private Block currentBlock; // 현재 활성 블럭

    public ScoreManager scoreManager; // 점수 시스템 연결
    public float spawnDelay = 0.5f;   // 블록 멈춘 후 다음 블록 딜레이

    private int? lastIndex = null; // 마지막으로 생성된 블록 인덱스 저장용

    // 새로운 블럭을 무작위로 생성하고 반환
    public Block SpawnBlock()
    {
        // 예외 처리: pool 비었는지 확인
        if (blockPool == null || blockPool.Count == 0)
        {
            Debug.LogError("BlockManager: blockPool이 비어 있습니다.");
            return null;
        }

        // 중복 방지 랜덤 인덱스 선택
        int newIndex;
        do
        {
            newIndex = Random.Range(0, blockPool.Count);
        } while (lastIndex != null && blockPool.Count > 1 && newIndex == lastIndex.Value);

        lastIndex = newIndex;

        BlockData data = blockPool[newIndex];
        Debug.Log("생성된 블럭: " + data.blockName);

        // 예외 처리: 프리팹 연결 안 되어 있으면
        if (data.prefab == null)
        {
            Debug.LogError($"BlockManager: BlockData '{data.blockName}'에 프리팹이 연결되어 있지 않습니다.");
            return null;
        }

        // 프리팹 생성
        GameObject obj = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);

        // Block 스크립트 유효성 체크
        currentBlock = obj.GetComponent<Block>();
        if (currentBlock == null)
        {
            Debug.LogError($"BlockManager: 생성된 프리팹 '{data.prefab.name}'에 Block.cs가 없습니다.");
            return null;
        }

        currentBlock.Init(data);

        currentBlock.OnStopped = () =>
        {
            scoreManager.RegisterBlock(currentBlock.transform);
            ClearCurrentBlock();
            Invoke(nameof(SpawnBlock), spawnDelay);
        };

        return currentBlock;
    }
    // 현재 블럭 낙하
    public void DropCurrentBlock()
    {
        if (currentBlock != null)
        {
            currentBlock.ActivatePhysics();
        }
    }

    public Block GetCurrentBlock() => currentBlock;

    public void ClearCurrentBlock() => currentBlock = null;
}