using UnityEngine;
using UnityEngine.UI;

public class SetMode : MonoBehaviour
{
  private GameManager gameManager;
  private Button btn;

  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    btn = GetComponent<Button>();
    btn.onClick.AddListener(SetPvP);
  }

  private void SetPvP()
  {
    gameManager.isSolo = false;
    gameManager.StartMultiGame();
  }
}