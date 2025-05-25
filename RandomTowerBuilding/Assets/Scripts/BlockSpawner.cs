using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs;  // 여러 개의 블록 프리팹을 담을 배열
    public Transform spawnPoint;        // 블록을 생성할 위치
    public MainCameraController cameraController;  // 카메라 컨트롤러

    // 블록을 생성하는 메서드
    public void SpawnBlock()
    {
        if (blockPrefabs.Length == 0) return;  // 블록 프리팹이 없으면 생성하지 않음

        // 랜덤으로 하나의 블록을 선택
        int randomIndex = Random.Range(0, blockPrefabs.Length);  // 배열 크기만큼 랜덤으로 인덱스를 선택
        GameObject selectedBlockPrefab = blockPrefabs[randomIndex];  // 랜덤으로 선택된 블록

        // 선택된 블록을 생성
        GameObject newBlock = Instantiate(selectedBlockPrefab, spawnPoint.position, Quaternion.identity);

        // 생성된 블록을 카메라 컨트롤러에 등록
        cameraController.RegisterBlock(newBlock.transform);

        // 블록 생성 후 spawnPoint 위치를 한 칸 위로 이동
        spawnPoint.position += new Vector3(0, 1f, 0);
    }
}