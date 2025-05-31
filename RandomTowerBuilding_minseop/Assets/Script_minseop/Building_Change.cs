using UnityEngine;

public class Building_Change : MonoBehaviour
{

    public GameObject[] blockPrefabs; // 블록 프리팹 배열
    public Transform spawnPoint; // 블록 생성 위치

    public GameObject currentBlock;
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
        GameObject newBlock = Instantiate(blockPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        // `currentBlock` 갱신
        currentBlock = newBlock;

        // 새 블록에 `Building_Movement` 연결
        Building_Movement movement = newBlock.GetComponent<Building_Movement>();
        if (movement != null)
        {
            movement.buildingchange = this; // 새 블록이 `Building_Change`를 참조
        }
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
