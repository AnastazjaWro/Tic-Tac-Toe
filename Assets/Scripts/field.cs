using System;
using UnityEngine;

public class field : MonoBehaviour
{ 
  public bool unfilled = false;
  public int owner;
  public int id;
  [SerializeField] int id_X;
  [SerializeField] int id_Y;
  private Tuple<int, int> cords;
  private GameManager gameManager;
  private GameObject original;
  private GameObject clone;

  void Start()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    cords = new Tuple<int, int>( id_X, id_Y );
    id = 3 * id_Y + id_X;
  }

  public void OnMouseDown()
  {
    if (gameManager.isActiveMulti && !unfilled)
    {
      UpdateField();
      gameManager.UpdateGame(this.cords);
      int? boardState = gameManager.checkState();
      if (boardState != null && boardState != 0)
      {
        gameManager.GameOver(boardState.Value);
      }
    }
    else if(gameManager.isActiveSolo && !unfilled && gameManager.isSolo)
    {
      gameManager.isClicked = true;
      UpdateField();
      gameManager.UpdateGame(this.cords);
      gameManager.NextMove();
      }
  }

  public void BotMove()
  {
    if (gameManager.isActiveSolo && !unfilled && gameManager.isSolo)
    {
      this.UpdateField();
      gameManager.UpdateGame(this.cords);
    }
  }

  public void UpdateField()
  {
    original = gameManager.ActivePlayer.prefab;
    clone = (GameObject)Instantiate(original, transform.position, Quaternion.identity);
    unfilled = true;
    owner = gameManager.ActivePlayer.owner;
  }

  public void Clear()
  {
    unfilled=false;
    DestroyImmediate(clone, true);
  }
}