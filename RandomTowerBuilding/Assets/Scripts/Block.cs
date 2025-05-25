using UnityEngine;

public class Block : MonoBehaviour
{
    void Start()
    {
        MainCameraController mainCam = Camera.main.GetComponent<MainCameraController>();
        if(mainCam != null)
        {
            mainCam.RegisterBlock(transform);
        }

        MiniMapCameraController miniMapCam = FindObjectOfType<MiniMapCameraController>();
        if (miniMapCam != null)
        {
            miniMapCam.RegisterBlock(transform);
        }
    }
}
