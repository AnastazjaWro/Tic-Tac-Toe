using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
  [SerializeField] Button toggleBoardBtn;
  [SerializeField] Button restartBtn;
  [SerializeField] Button mainMenuBtn;
  [SerializeField] TextMeshProUGUI winnerText;
  private GameManager gameManager;
  private bool isSolo;
  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    restartBtn.onClick.AddListener(RestartGame);
    mainMenuBtn.onClick.AddListener(BackToMenu);    
  }
  public void SetWinnerText(int endcode)
  {
    switch (endcode)
    {
      case -1:
        winnerText.text = "Player " + 1 + " won this game";
        break;
      case 1:
        winnerText.text = "Player "+ 2+ " won this game";
        break;
      case 2:
        winnerText.text = "Draw";
        break;
    }
  }
    
  private void RestartGame()
  {
    gameManager.ClearBoard();
    gameManager.PrepareBoard();
    gameObject.SetActive(false);
    isSolo = gameManager.isSolo;
    if (isSolo)
    {
      gameManager.StartSoloGame();
    }
    else
    {
      gameManager.StartMultiGame();
    }
  }

  private void BackToMenu()
  {
    gameManager.BackToMenu();
  }
}