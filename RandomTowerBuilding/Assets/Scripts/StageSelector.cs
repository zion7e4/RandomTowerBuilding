using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button GameStartButton;

    public Image stage1Image;
    public Image stage2Image;
    public Image stage3Image;

    private string selectedStage = "";
    private bool stageSelected = false;

    private void Start()
    {
        GameStartButton.gameObject.SetActive(false);

        stage1Button.onClick.AddListener(() => SelectStage("Stage1"));
        stage2Button.onClick.AddListener(() => SelectStage("Stage2"));
        stage3Button.onClick.AddListener(() => SelectStage("Stage3"));

        GameStartButton.onClick.AddListener(StartGame);
    }

    void SelectStage(string stageName)
    {
        selectedStage = stageName;
        stageSelected = true;
        Debug.Log("Selected Stage: " + selectedStage);

        GameStartButton.gameObject.SetActive(true);

        // 이미지 음영 처리
        SetStageImageOpacity(stage1Image, stageName == "Stage1" ? 0.5f : 1f);
        SetStageImageOpacity(stage2Image, stageName == "Stage2" ? 0.5f : 1f);
        SetStageImageOpacity(stage3Image, stageName == "Stage3" ? 0.5f : 1f);
    }

    void SetStageImageOpacity(Image img, float alpha)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }

    void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedStage))
        {
            SceneManager.LoadScene(selectedStage);
        }
    }
}
