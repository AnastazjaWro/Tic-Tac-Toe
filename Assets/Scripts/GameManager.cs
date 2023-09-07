using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool isSolo;
  public bool isActiveMulti = false;
  public bool isActiveSolo = false;
  public bool isClicked = false;
  public player ActivePlayer;
  [SerializeField] List<field> scrFields = new List<field>();
  [SerializeField] TextMeshProUGUI playerText;
  [SerializeField] GameObject gameBoard;
  [SerializeField] GameObject chooseModeScreen;
  [SerializeField] GameObject titleScreen;
  [SerializeField] GameObject gameStuff;
  [SerializeField] GameObject endScreen;
  private player firstPlayer; 
  private player secondPlayer;
  private int filledFields = 0;
  private int[][] fields = new int[][] { new int[3], new int[3], new int[3]};
  private GameObject[] pawns;
  public void Start()
  {
    firstPlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<player>();
    secondPlayer = GameObject.FindGameObjectWithTag("Player2").GetComponent<player>();
    PrepareBoard();
  }

  public void StartSoloGame()
  {
    isSolo = true;
    filledFields = 0;
    PrepareBoard();
    chooseModeScreen.SetActive(false);
    gameStuff.SetActive(true);
    gameBoard.SetActive(true);
    isActiveSolo = true;
    ActivePlayer = firstPlayer;
    UpdatePlayerText();
  }

  public void StartMultiGame()
  {
      isSolo = false;
      filledFields = 0;
      PrepareBoard();
      chooseModeScreen.SetActive(false);
      gameStuff.SetActive(true);
      gameBoard.SetActive(true);
      isActiveMulti = true;
      ActivePlayer = firstPlayer;
      UpdatePlayerText();
  }

  public void NextMove()
  {
    int[] bestMove = new int[2];
    int bestScore = Int32.MinValue;
    for (int i = 0; i != 3; i++)
    {
      for (int j = 0; j != 3; j++)
      {
        if (fields[i][j] == 0)
        {
          fields[i][j] = 1;
          int score = minimax(0, false);
          fields[i][j] = 0;
          if (score > bestScore)
          {
            bestScore = score;
            bestMove[0] = i;
            bestMove[1] = j;
          }
        }   
      }
     }
     int fieldid = 3 * bestMove[1] + bestMove[0];
     scrFields[fieldid].BotMove();
     int? boardState = checkState();
     if (boardState != null && boardState != 0)
     {
       GameOver(boardState.Value);
     }
     isClicked = false;
  }

  private int minimax(int depth, bool isMaximaxing)
  {
    int? res = checkState();
    if (res != null) return res.Value;
    if (isMaximaxing)
    {
      int[] bestMove = new int[2];
      int bestScore = Int32.MinValue;
      for (int i = 0; i != 3; i++)
      {
        for (int j = 0; j != 3; j++)
        {
          if (fields[i][j] == 0)
          {
            fields[i][j] = 1;
            int score = minimax(depth + 1, false);
            fields[i][j] = 0;
            bestScore = Math.Max(score, bestScore);
          }
        }
      }
      return bestScore;
    }
    else
    {
      int[] bestMove = new int[2];
      int bestScore = Int32.MaxValue;
      for (int i = 0; i != 3; i++)
      {
        for (int j = 0; j != 3; j++)
        {
          if (fields[i][j] == 0)
          {
            fields[i][j] = -1;
            int score = minimax(depth + 1, true);
            fields[i][j] = 0;
            bestScore = Math.Min(score, bestScore);
          }
        }
      }
      return bestScore;
    }
  }

  public void UpdatePlayerText()
  {
    if (ActivePlayer == firstPlayer)
    {
      playerText.text = "Player: " + 1;
    }
    else
    {
      playerText.text = "Player: " + 2;
    }
  }

  public void UpdateGame(Tuple<int,int> id)
  {
    filledFields++;
    int? boardState = checkState();
    if (boardState != null)
    {
      GameOver(boardState.Value);
    }
    fields[id.Item1][id.Item2] = ActivePlayer.owner;
    ChangePlayer();
    UpdatePlayerText();

  }

  public void ToggleBoard(bool isBoardActive)
  {
    if (isBoardActive)
    {
      gameBoard.SetActive(false);
    }
    else
    {
      gameBoard.SetActive(true);
    }
    
  }

  public void ClearBoard()
  {
    foreach (field fieldist in scrFields)
    {
      fieldist.Clear();
    }
    GameObject[] pawns = GameObject.FindGameObjectsWithTag("Pawn");
    foreach (GameObject pawn in pawns)
    {
      Pawn pawnManager = pawn.GetComponent<Pawn>();
      pawnManager.destroyPawn();
    }
  }

  public void ChangePlayer()
  {
    ActivePlayer = ActivePlayer == secondPlayer ? firstPlayer : secondPlayer;
  }

  public void GameOver(int val)
  {
    if (isActiveMulti || isActiveSolo)
    {
      gameStuff.SetActive(false);
      pawns = GameObject.FindGameObjectsWithTag("Pawn");
      isActiveMulti = false;
      isActiveSolo = false;
      endScreen.GetComponent<EndGame>().SetWinnerText(val);
      endScreen.SetActive(true);
      ToggleBoard(true);
      HidePawns();
      PrepareBoard();
    }
  }

  public int? checkState()
  {
    int emptyfields = 0;
    for (int i = 0; i != 3; i++)
    {
      for (int j = 0; j != 3; j++)
      {
        if (fields[i][j] == 0)
        {
          emptyfields++;
        }
      }
    }
    if ((fields[0][0] != 0) &&
      (fields[0][0] == fields[0][1]
      && fields[0][0] == fields[0][2]))
    {
      return fields[0][0];
    }
    else if ((fields[0][0] != 0) &&
      (fields[0][0] == fields[1][0]
      && fields[0][0] == fields[2][0]))
    {
      return fields[0][0];
    }
    else if ((fields[0][0] != 0) &&
      (fields[0][0] == fields[1][1]
      && fields[0][0] == fields[2][2]))
    {
      return fields[0][0];
    }
    else if ((fields[1][0] != 0) &&
      (fields[1][0] == fields[1][1]
      && fields[1][0] == fields[1][2]))
    {
      return fields[1][0];
    }
    else if ((fields[2][0] != 0) &&
      (fields[2][0] == fields[2][1]
      && fields[2][0] == fields[2][2]))
    {
      return fields[2][0];
    }
    else if ((fields[0][1] != 0) &&
      (fields[0][1] == fields[1][1]
      && fields[0][1] == fields[2][1]))
    {
      return fields[0][1];
    }
    else if ((fields[0][2] != 0) &&
      (fields[0][2] == fields[1][2]
      && fields[0][2] == fields[2][2]))
    {
      return fields[0][2];
    }
    else if ((fields[0][2] != 0) &&
      (fields[0][2] == fields[1][1]
      && fields[0][2] == fields[2][0]))
    {
      return fields[0][2];
    }
    if (emptyfields == 0)
    {
      return 2;
    }
    return null;
  }

  public void PrepareBoard()
  {
    for(int i=0; i!=3; i++)
    {
      for(int j=0; j!=3; j++)
      {
        fields[i][j] = 0;
        scrFields[i].owner = 0;
      }
    }
  }

  public void ShowPawns()
  {
    foreach (GameObject pawn in pawns)
    {
      Pawn pawnManager = pawn.GetComponent<Pawn>();
      pawnManager.ShowPawn();
    }
  }

  public void HidePawns()
  {
    foreach (GameObject pawn in pawns)
    {
      Pawn pawnManager = pawn.GetComponent<Pawn>();
      pawnManager.HidePawn();
    }
  }

  public void SetDifficulty()
  {
    titleScreen.SetActive(false);
    chooseModeScreen.SetActive(true);
  }

  public void BackToMenu()
  {
    ClearBoard();
    PrepareBoard();
    endScreen.SetActive(false);
    titleScreen.SetActive(true);
  }
}
