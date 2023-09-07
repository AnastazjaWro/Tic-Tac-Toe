using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideBoard : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI EndTitleText;
  [SerializeField] TextMeshProUGUI winnerText;
  [SerializeField] Button restartBtn;
  [SerializeField] Button mainMenuBtn;
  [SerializeField] Button toggleBoardBtn;
  [SerializeField] TMP_Text toggleBtnText;
  private GameManager gameManager;
  private bool isBoardActive = false;

  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    toggleBoardBtn.onClick.AddListener(ShowHideUI);
  }
  private void ShowHideUI()
  {
    if (isBoardActive)
    {
      gameManager.HidePawns();
      SetGameSetting(true);
      toggleBtnText.text = "Show Board";
    }
    else
    {
      gameManager.ShowPawns();
      SetGameSetting(false);
      toggleBtnText.text = "Hide Board";
    }
  }

  private void SetGameSetting(bool val)
  {
    gameManager.ToggleBoard(val);
    EndTitleText.gameObject.SetActive(val);
    winnerText.gameObject.SetActive(val);
    restartBtn.gameObject.SetActive(val);
    mainMenuBtn.gameObject.SetActive(val);
    isBoardActive = !val;
  }
}