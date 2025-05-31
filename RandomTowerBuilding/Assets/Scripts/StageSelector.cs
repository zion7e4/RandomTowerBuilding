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

    // 버튼의 Image 컴포넌트도 받아오기
    private Image stage1ButtonImage;
    private Image stage2ButtonImage;
    private Image stage3ButtonImage;

    private string selectedStage = "";
    private bool stageSelected = false;

    private void Start()
    {
        GameStartButton.gameObject.SetActive(false);

        stage1ButtonImage = stage1Button.GetComponent<Image>();
        stage2ButtonImage = stage2Button.GetComponent<Image>();
        stage3ButtonImage = stage3Button.GetComponent<Image>();

        stage1Button.onClick.AddListener(() => SelectStage("Stage1"));
        stage2Button.onClick.AddListener(() => SelectStage("Stage2"));
        stage3Button.onClick.AddListener(() => SelectStage("Stage3"));

        GameStartButton.onClick.AddListener(StartGame);

        // 시작 시 이미지 모두 비활성화
        stage1Image.gameObject.SetActive(false);
        stage2Image.gameObject.SetActive(false);
        stage3Image.gameObject.SetActive(false);
    }

    void SelectStage(string stageName)
    {
        selectedStage = stageName;
        stageSelected = true;
        Debug.Log("Selected Stage: " + selectedStage);

        GameStartButton.gameObject.SetActive(true);

        // 선택된 이미지 활성화, 나머지 비활성화
        stage1Image.gameObject.SetActive(stageName == "Stage1");
        stage2Image.gameObject.SetActive(stageName == "Stage2");
        stage3Image.gameObject.SetActive(stageName == "Stage3");

        // 이미지 음영 처리: 선택된 것은 밝게, 나머지는 어둡게(알파 낮춤)
        SetStageImageOpacity(stage1Image, stageName == "Stage1" ? 1f : 0.5f);
        SetStageImageOpacity(stage2Image, stageName == "Stage2" ? 1f : 0.5f);
        SetStageImageOpacity(stage3Image, stageName == "Stage3" ? 1f : 0.5f);

        // 버튼 이미지도 음영 처리
        SetStageImageOpacity(stage1ButtonImage, stageName == "Stage1" ? 1f : 0.5f);
        SetStageImageOpacity(stage2ButtonImage, stageName == "Stage2" ? 1f : 0.5f);
        SetStageImageOpacity(stage3ButtonImage, stageName == "Stage3" ? 1f : 0.5f);
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
