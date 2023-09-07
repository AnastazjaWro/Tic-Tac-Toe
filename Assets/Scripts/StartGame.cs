using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
  private GameManager gameManager;
  private Button btn;

  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    btn = GetComponent<Button>();
    btn.onClick.AddListener(startGame);
  }

  void startGame()
  {
    gameManager.SetDifficulty();
  }
}