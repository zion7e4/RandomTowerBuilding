using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Building_Change : MonoBehaviour
{
    MainCameraController mainCameraController;
    Building_Movement bm;
    Block block;
    public List<BlockData> blockPool;
    public Transform spawnPoint; // ��� ���� ��ġ

    public GameObject currentBlock;
    [SerializeField]
    private int remainingSwaps = 2; // �ٲٱ� ���� Ƚ��
    [SerializeField]
    public int blockcount;
    private float previousHighestY = 0f; // ���� �ְ� ���� ���
    private bool isFirstBlock = true; // ù ��° ��� ���θ� ����
    [SerializeField]
    public float currentHeight;
    [SerializeField]
    private float spawnY;
    [SerializeField]
    private float towerHeight;


    private void Start()
    {
        SpawnNewBlock();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && remainingSwaps > 0)
        {
            SwapBlock();
        }

        // ���� ����Ʈ ����
        AdjustSpawnPoint();
    }

    public void SpawnNewBlock()
    {
        int randomIndex = Random.Range(0, blockPool.Count);
        BlockData data = blockPool[randomIndex];
        GameObject newData = Instantiate(blockPool[randomIndex].prefab, spawnPoint.position, Quaternion.identity);

        block = newData.GetComponent<Block>();
        block.Init(data);

        // `currentBlock` ����
        currentBlock = newData;

        // ��� ���� ��� ���
        GameManager.Instance.RegisterBlock(newBlock.transform);

        // �� ��Ͽ� `Building_Movement` ����
        Building_Movement movement = newData.GetComponent<Building_Movement>();
        if (movement != null)
        {
            movement.buildingchange = this; // �� ����� `Building_Change`�� ����
        }
    }

    private void SwapBlock()
    {
        if (currentBlock != null)
        {
            Destroy(currentBlock); // ���� ��� ����
        }

        remainingSwaps--;
        SpawnNewBlock(); // ���ο� ��� ����
    }

    private void AdjustSpawnPoint()
    {
        Transform highestBlock = MainCameraController.Instance.GetHighestBlock();
        if (highestBlock == null) return;

         currentHeight = highestBlock.position.y;
         spawnY = spawnPoint.position.y;

        // ù ��° ����� ��� ���� ����Ʈ ���� �ǳʶ�
        if (isFirstBlock)
        {
            previousHighestY = currentHeight; // �ְ� ���� ��ϸ� ������Ʈ
            isFirstBlock = false; // ù ��� ó���� �Ϸ�Ǿ����� ǥ��
            return;
        }

        // �ְ� ���� ���� ����
        if (currentHeight > previousHighestY)
        {
            // ���� ����Ʈ �ø���
            if (spawnY - currentHeight <= 4.5f)
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x, currentHeight + 6f, spawnPoint.position.z);
            }
        }
        else if (currentHeight < previousHighestY)
        {
            // ���� ����Ʈ ���߱�
            if (spawnY - currentHeight > 6f) 
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x, currentHeight + 6f, spawnPoint.position.z);
            }
        }

        // ���� �ְ� ���� ������Ʈ
        previousHighestY = currentHeight;
    }

    public void IncrementBlockCount()
    {
        blockcount++;
    }
}
