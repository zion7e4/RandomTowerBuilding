using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public ScoreManager scoreManager;
    [SerializeField]
    private bool isGameClear = false;
    [SerializeField]
    private float targetHeight = 1500f;
    public Building_Change bc;

    private void Update()
    {
        if(bc.blockcount >= 15 || scoreManager.maxHeight >= targetHeight)
        {
            isGameClear = true;
            SceneManager.LoadScene("GameClear");
        }
    }
}
