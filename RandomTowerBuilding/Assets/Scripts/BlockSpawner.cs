using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs;  // ���� ���� ��� �������� ���� �迭
    public Transform spawnPoint;        // ����� ������ ��ġ
    public MainCameraController cameraController;  // ī�޶� ��Ʈ�ѷ�

    // ����� �����ϴ� �޼���
    public void SpawnBlock()
    {
        if (blockPrefabs.Length == 0) return;  // ��� �������� ������ �������� ����

        // �������� �ϳ��� ����� ����
        int randomIndex = Random.Range(0, blockPrefabs.Length);  // �迭 ũ�⸸ŭ �������� �ε����� ����
        GameObject selectedBlockPrefab = blockPrefabs[randomIndex];  // �������� ���õ� ���

        // ���õ� ����� ����
        GameObject newBlock = Instantiate(selectedBlockPrefab, spawnPoint.position, Quaternion.identity);

        // ������ ����� ī�޶� ��Ʈ�ѷ��� ���
        cameraController.RegisterBlock(newBlock.transform);

        // ��� ���� �� spawnPoint ��ġ�� �� ĭ ���� �̵�
        spawnPoint.position += new Vector3(0, 1f, 0);
    }
}