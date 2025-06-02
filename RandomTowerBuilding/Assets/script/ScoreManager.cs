using UnityEngine;
using TMPro;
using System.Collections.Generic;
using TMPro.Examples;

/// ������ ����� ������ �ְ� ���̸� �����ϰ� UI�� ǥ����
public class ScoreManager : MonoBehaviour
{
    private List<Transform> stabilizedBlocks = new List<Transform>();

    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI switchText;
    public TextMeshProUGUI blockNameText;
    Building_Change bc;

    [Header("���� ���� (floor)")]
    public Transform floor;

    [Header("��ǥ�� (Ŭ���� ����)")]
    public int targetBlockCount = 15;
    public float targetHeight = 15f;
    public float maxHeight = 0f;

    void Update()
    {
        UpdateUI();
    }

    void Start()
    {
        bc = FindObjectOfType<Building_Change>();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// ����� ������ �� ȣ���
    public void RegisterBlock(Transform block)
    {
        if (!stabilizedBlocks.Contains(block))
            stabilizedBlocks.Add(block);

        UpdateUI();  // UI�� ������ ������ ���� ž ���� ���
    }
    private void UpdateUI()
    {
        if (blockCountText != null)
            blockCountText.text = $"BlockCount: {bc.blockcount}";

        if (heightText != null)
            heightText.text = $"Height: {GetCurrentTowerHeight():F2}m";
    }
    public void UpdateBlockName(string blockName)
    {
        if (blockNameText != null)
        {
            blockNameText.text = $"Block Name\n: {blockName}";
        }
    }

    private float GetCurrentTowerHeight()
    {
        float highest = 0f;

        foreach (Transform block in stabilizedBlocks)
        {
            if (block == null) continue;

            Renderer rend = block.GetComponent<Renderer>();
            if (rend == null) continue;

            float topY = block.position.y + (rend.bounds.size.y / 2f);
            float baseY = floor.position.y;
            float height = (topY - baseY) * 100f;

            if (height > highest)
                highest = height;
        }

        return highest;
    }

    public void ResetScore()
    {
        bc.blockcount = 0;
        maxHeight = 0f;
        UpdateUI();
    }
}