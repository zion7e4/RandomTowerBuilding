using UnityEngine;
using TMPro;

/// ������ ����� ������ �ְ� ���̸� �����ϰ� UI�� ǥ����
public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI heightText;
    Building_Change bc;

    [Header("���� ���� (floor)")]
    public Transform floor;

    [Header("��ǥ�� (Ŭ���� ����)")]
    public int targetBlockCount = 15;
    public float targetHeight = 15f;
    private float maxHeight = 0f;

    /// ����� ������ �� ȣ���
    public void RegisterBlock(Transform block)
    {

        float blockTop = block.position.y + (block.GetComponent<Renderer>().bounds.size.y / 2f);
        float baseY = floor.position.y;
        float height = (blockTop - baseY) * 100f;

        if (height > maxHeight)
            maxHeight = height;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (blockCountText != null)
            blockCountText.text = $"blockCount: {bc.blockcount}";

        if (heightText != null)
            heightText.text = $"height: {maxHeight:F2}m";
    }

    public void ResetScore()
    {
        bc.blockcount = 0;
        maxHeight = 0f;
        UpdateUI();
    }
}