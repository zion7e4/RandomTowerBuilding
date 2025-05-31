using Unity.VisualScripting;
using UnityEngine;

public class Building_Change : MonoBehaviour
{
    MainCameraController mainCameraController;
    Building_Movement bm;
    public GameObject[] blockPrefabs; // 블록 프리팹 배열
    public Transform spawnPoint; // 블록 생성 위치

    public GameObject currentBlock;
    [SerializeField]
    private int remainingSwaps = 2; // 바꾸기 가능 횟수
    [SerializeField]
    public int blockcount;
    private float previousHighestY = 0f; // 이전 최고 높이 기록
    private bool isFirstBlock = true; // 첫 번째 블록 여부를 추적
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

        // 스폰 포인트 조정
        AdjustSpawnPoint();
    }

    public void SpawnNewBlock()
    {
        int randomIndex = Random.Range(0, blockPrefabs.Length);
        GameObject newBlock = Instantiate(blockPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        // `currentBlock` 갱신
        currentBlock = newBlock;

        // 블록 감시 대상 등록
        GameManager.Instance.RegisterBlock(newBlock.transform);

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

    private void AdjustSpawnPoint()
    {
        Transform highestBlock = MainCameraController.Instance.GetHighestBlock();
        if (highestBlock == null) return;

         currentHeight = highestBlock.position.y;
         spawnY = spawnPoint.position.y;

        // 첫 번째 블록일 경우 스폰 포인트 조정 건너뜀
        if (isFirstBlock)
        {
            previousHighestY = currentHeight; // 최고 높이 기록만 업데이트
            isFirstBlock = false; // 첫 블록 처리가 완료되었음을 표시
            return;
        }

        // 최고 높이 변경 감지
        if (currentHeight > previousHighestY)
        {
            // 스폰 포인트 올리기
            if (spawnY - currentHeight <= 4.5f)
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x, currentHeight + 6f, spawnPoint.position.z);
            }
        }
        else if (currentHeight < previousHighestY)
        {
            // 스폰 포인트 낮추기
            if (spawnY - currentHeight > 6f) 
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x, currentHeight + 6f, spawnPoint.position.z);
            }
        }

        // 이전 최고 높이 업데이트
        previousHighestY = currentHeight;
    }

    public void IncrementBlockCount()
    {
        blockcount++;
    }
}
