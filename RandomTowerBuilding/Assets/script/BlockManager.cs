using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public List<BlockData> blockPool; // ScriptableObject ����Ʈ
    public Transform spawnPoint; // �� ���� ��ġ

    private Block currentBlock; // ���� Ȱ�� ��

    // ���ο� ���� �������� �����ϰ� ��ȯ
    public Block SpawnBlock()
    {
        // ���� ó��: pool ������� Ȯ��
        if (blockPool == null || blockPool.Count == 0)
        {
            Debug.LogError("BlockManager: blockPool�� ��� �ֽ��ϴ�.");
            return null;
        }

        // ������ �� ����
        BlockData data = blockPool[Random.Range(0, blockPool.Count)];
        Debug.Log("������ ��: " + data.blockName);

        // ���� ó��: ������ ���� �� �Ǿ� ������
        if (data.prefab == null)
        {
            Debug.LogError($"BlockManager: BlockData '{data.blockName}'�� �������� ����Ǿ� ���� �ʽ��ϴ�.");
            return null;
        }

        // ������ ����
        GameObject obj = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);

        // Block ��ũ��Ʈ ��ȿ�� üũ
        currentBlock = obj.GetComponent<Block>();
        if (currentBlock == null)
        {
            Debug.LogError($"BlockManager: ������ ������ '{data.prefab.name}'�� Block.cs�� �����ϴ�.");
            return null;
        }

        currentBlock.Init(data);
        return currentBlock;
    }

    // ���� �� ����
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