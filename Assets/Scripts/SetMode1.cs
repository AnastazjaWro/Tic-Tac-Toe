using UnityEngine;
using UnityEngine.UI;

public class SetMode1 : MonoBehaviour
{
  private GameManager gameManager;
  private Button btn;

  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    btn = GetComponent<Button>();
    btn.onClick.AddListener(SetPvC);
  }

  private void SetPvC()
  {
    gameManager.isSolo = true;
    gameManager.StartSoloGame();
  }
}