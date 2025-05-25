using UnityEngine;

public class Building_Change : MonoBehaviour
{
    public GameObject[] blockPrefabs; // 블록 프리팹 배열
    public Transform spawnPoint; // 블록 생성 위치
   
    private GameObject currentBlock;
    private int remainingSwaps = 2; // 바꾸기 가능 횟수

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
            Destroy(currentBlock); // 현재 블록 제거
        }
        remainingSwaps--;
        SpawnNewBlock(); // 새로운 블록 생성
    }
}