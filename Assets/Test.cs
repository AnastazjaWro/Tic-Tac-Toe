using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
  private GameManager gameManager;
  private Button btn;
  // Start is called before the first frame update
  void Start()
    {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    btn = GetComponent<Button>();
    btn.onClick.AddListener(showme);
  }

  // Update is called once per frame
  private void showme()
  {
  }
}
