using UnityEngine;

public class Building_Change : MonoBehaviour
{

    public GameObject[] blockPrefabs; // ��� ������ �迭
    public Transform spawnPoint; // ��� ���� ��ġ

    public GameObject currentBlock;
    private int remainingSwaps = 2; // �ٲٱ� ���� Ƚ��

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
    }

    public void SpawnNewBlock()
    {
        int randomIndex = Random.Range(0, blockPrefabs.Length);
        GameObject newBlock = Instantiate(blockPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        // `currentBlock` ����
        currentBlock = newBlock;

        // �� ��Ͽ� `Building_Movement` ����
        Building_Movement movement = newBlock.GetComponent<Building_Movement>();
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
}
