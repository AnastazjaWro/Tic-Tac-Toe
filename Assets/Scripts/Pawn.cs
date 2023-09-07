using UnityEngine;

public class Pawn : MonoBehaviour
{
  public void ShowPawn()
  {
    gameObject.SetActive(true);
  }

  public void HidePawn()
  {
    gameObject.SetActive(false);
  }

  public void destroyPawn()
  {
    Destroy(gameObject);
  }
}