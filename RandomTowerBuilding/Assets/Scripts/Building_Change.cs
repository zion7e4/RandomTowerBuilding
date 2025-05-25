using UnityEngine;

public class Building_Change : MonoBehaviour
{
    public GameObject[] blockPrefabs; // ��� ������ �迭
    public Transform spawnPoint; // ��� ���� ��ġ
   
    private GameObject currentBlock;
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
        currentBlock = Instantiate(blockPrefabs[randomIndex], spawnPoint.transform.position, Quaternion.identity);
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