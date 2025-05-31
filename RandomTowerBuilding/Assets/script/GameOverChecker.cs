using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public float fallLimitY = -15f; // ����� �� Y�� �Ʒ��� �������� ���� ����

    private List<Transform> activeBlocks = new List<Transform>(); // ������ ��ϵ� ����Ʈ
    private bool isMonitoring = true; // ���� ���� �߻� �Ŀ��� ���� �ߴ�

    public void RegisterBlock(Transform block)
    {
        activeBlocks.Add(block);
    }

    void Update()
    {
        if (!isMonitoring) return; // �̹� ���� ���� ���¸� ���� �ߴ�

        // ��� ����� �˻��Ѵ�
        foreach (Transform block in activeBlocks)
        {
            if (block == null) continue; // ����� �̹� �ı��Ǿ��� ���� ����

            if (block.position.y < fallLimitY)
            {
                // ���غ��� �Ʒ��� �������ٸ� ���� ����
                isMonitoring = false; // �ߺ� ���� ����
                GameManager.Instance.GameOver(); // ���� ���� ȣ��
                return;
            }
        }
    }

    /// �������� ����� ��� ���ø� �ʱ�ȭ�� �� ȣ��
    public void ResetChecker()
    {
        activeBlocks.Clear(); // ���� ���̴� ��� ��� ����
        isMonitoring = true;  // �ٽ� ���� ����
    }
}