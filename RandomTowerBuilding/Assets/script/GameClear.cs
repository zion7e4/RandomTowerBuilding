using UnityEngine;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private bool isGameClear = false;
    [SerializeField]
    private float targetHeight = 12f;
    public Building_Change bc;

    private void Update()
    {
        if(bc.blockcount >= 15 ||  bc.currentHeight >= targetHeight)
        {
            isGameClear = true;
        }
    }
}
