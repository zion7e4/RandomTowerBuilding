using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MiniMapCameraController : MonoBehaviour
{
    public float baseSize = 5f;          // ���� ���� �� orthographicSize
    public float maxSize = 40f;           // �ִ� orthographicSize
    public float sizeLerpSpeed = 2f;      // �ε巴�� Ŀ���� �ӵ�

    public List<Transform> allBlocks = new List<Transform>();

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        cam.rect = new Rect(0.77f, 0f, 0.23f, 0.25f);
        cam.depth = 1;
    }

    void LateUpdate()
    {
        Transform highestBlock = GetHighestBlock();
        if (highestBlock == null) return;

        float height = highestBlock.position.y;

        // ��� ���̰� ���������� size ����
        float heightFactor = Mathf.Clamp01(height / 100f);
        float targetSize = Mathf.Lerp(baseSize, maxSize, heightFactor);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * sizeLerpSpeed);

        float yoffset = cam.orthographicSize;
        transform.position = new Vector3(0, yoffset - 3f, -10);
    }

    Transform GetHighestBlock()
    {
        if (allBlocks.Count == 0) return null;

        Transform highest = allBlocks[0];
        foreach (var block in allBlocks)
        {
            if (block != null && block.position.y > highest.position.y)
            {
                highest = block;
            }
        }
        return highest;
    }

    public void RegisterBlock(Transform block)
    {
        if (!allBlocks.Contains(block))
        {
            allBlocks.Add(block);
        }
    }
}