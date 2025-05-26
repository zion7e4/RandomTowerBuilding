using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public static MainCameraController Instance { get; private set; } // �̱��� �ν��Ͻ�

    public float yOffset = 5f;
    public float smoothTime = 0.4f;
    public float zoomOutFOV = 120f;
    public float baseFOV = 80f;
    public float fovLerpSpeed = 2f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    // ��ϵ��� ��� ����ص� ����Ʈ
    public List<Transform> allBlocks = new List<Transform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // �̱��� �ʱ�ȭ
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �̱��� ����
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Transform highestBlock = GetHighestBlock();
        if (highestBlock == null) return;

        float targetY = highestBlock.position.y + yOffset;
        /*if (targetY > transform.position.y)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }*/

        Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        float targetFOV = Mathf.Lerp(baseFOV, zoomOutFOV, highestBlock.position.y / 100f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
    }

     public Transform GetHighestBlock()
    {
        if (allBlocks.Count == 0) return null;

        Transform highest = allBlocks[0];
        foreach (var block in allBlocks)
        {
            Building_Movement movement = block.GetComponent<Building_Movement>();
            if (movement != null && movement.isGrounded) // isGrounded ���� Ȯ��
            {
                if (highest == null || block.position.y > highest.position.y)
                {
                    highest = block;
                }
            }
        }
        return highest;
    }

    public float GetTowerHeight()
    {
        Transform highestBlock = GetHighestBlock();
        return highestBlock != null ? highestBlock.position.y : 0f;
    }

    public void RegisterBlock(Transform block)
    {
        if (!allBlocks.Contains(block))
        {
            allBlocks.Add(block);
        }
    }
}