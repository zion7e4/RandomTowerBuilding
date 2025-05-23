using UnityEngine;

// ��ü ������ �帧�� �����ϴ� ��ũ��Ʈ
public class GameManager : MonoBehaviour
{
    public BlockManager blockManager; // �� ������ ����

    void Start()
    {
        SpawnAndListen(); // ù �� ����
    }

    void Update()
    {
        Block current = blockManager.GetCurrentBlock();

        if (current != null)
        {
            // �¿� �̵� (0.5 ���־�)
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                current.Move(Vector2.left * 0.5f);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                current.Move(Vector2.right * 0.5f);

            // ȸ��
            if (Input.GetKeyDown(KeyCode.UpArrow))
                current.Rotate(15f); // �ݽð� ����

            if (Input.GetKeyDown(KeyCode.DownArrow))
                current.Rotate(-15f); // �ð� ����

            // ���� (���� Ȱ��ȭ)
            if (Input.GetKeyDown(KeyCode.Return))
                blockManager.DropCurrentBlock();
        }
    }

    // �� ���� + ���� ���� ������ ���
    void SpawnAndListen()
    {
        Block newBlock = blockManager.SpawnBlock();
        newBlock.OnStopped = HandleBlockStopped;
    }

    // ���� �����Ǹ� ���� �� ����
    void HandleBlockStopped(Block block)
    {
        blockManager.ClearCurrentBlock();
        Invoke(nameof(SpawnAndListen), 0.5f);
    }
}