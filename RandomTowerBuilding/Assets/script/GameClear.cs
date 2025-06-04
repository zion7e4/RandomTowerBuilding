using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public ScoreManager scoreManager;
    [SerializeField]
    private bool isGameClear = false;
    [SerializeField]
    private float targetHeight = 1500f;
    [SerializeField]
    private int targetCount = 15;
    public Building_Change bc;

    private void Update()
    {
        if (bc.blockcount >= targetCount || scoreManager.highest >= targetHeight)
        {
            isGameClear = true;
            GameManager.LastPlayedStage = SceneManager.GetActiveScene().name;
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Stage1" || currentScene == "Stage2")
            {
                SceneManager.LoadScene("StageClear");
            }
            else if (currentScene == "Stage3")
            {
                SceneManager.LoadScene("GameClear");
            }
        }

    }
}
