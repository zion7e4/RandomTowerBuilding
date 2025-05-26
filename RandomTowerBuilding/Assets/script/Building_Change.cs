using Unity.VisualScripting;
using UnityEngine;

public class Building_Change : MonoBehaviour
{
    MainCameraController mainCameraController;
    public GameObject[] blockPrefabs; // ��� ������ �迭
    public Transform spawnPoint; // ��� ���� ��ġ

    public GameObject currentBlock;
    [SerializeField]
    private int remainingSwaps = 2; // �ٲٱ� ���� Ƚ��
    private float previousHighestY = 0f; // ���� �ְ� ���� ���
    private bool isFirstBlock = true; // ù ��° ��� ���θ� ����


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

    private void AdjustSpawnPoint()
    {
        Transform highestBlock = MainCameraController.Instance.GetHighestBlock();
        if (highestBlock == null) return;

        float currentHeight = highestBlock.position.y;
        float spawnY = spawnPoint.position.y;

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
            if (spawnY - currentHeight <= 4f)
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x, currentHeight + 3f, spawnPoint.position.z);
            }
        }
        else if (currentHeight < previousHighestY)
        {
            // ���� ����Ʈ ���߱�
            if (spawnY - currentHeight > 3f) 
            {
                spawnPoint.position = new Vector3(spawnPoint.position.x, currentHeight + 3f, spawnPoint.position.z);
            }
        }

        // ���� �ְ� ���� ������Ʈ
        previousHighestY = currentHeight;
    }
}
