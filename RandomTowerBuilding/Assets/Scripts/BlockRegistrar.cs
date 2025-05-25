using UnityEngine;

public class BlockRegistrar : MonoBehaviour
{
    Building_Movement building_Movement;

    void Start()
    {
        building_Movement = GetComponent<Building_Movement>();

        /*MainCameraController mainCam = Camera.main.GetComponent<MainCameraController>();
        if(mainCam != null)
        {
            mainCam.RegisterBlock(transform);
        }

        MiniMapCameraController miniMapCam = FindObjectOfType<MiniMapCameraController>();
        if (miniMapCam != null)
        {
            miniMapCam.RegisterBlock(transform);
        }*/
    }

    void Update()
    {
        if(building_Movement.isGrounded)
        {
            MainCameraController mainCam = Camera.main.GetComponent<MainCameraController>();
            if (mainCam != null)
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
}
