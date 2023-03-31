using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  [SerializeField] private bool _isGameOver = false;


  public void Update()
  {
    if (_isGameOver && Input.GetKeyDown(KeyCode.R))
    {
      // Scene scene = SceneManager.GetActiveScene();
      // SceneManager.LoadScene(scene.name);
      SceneManager.LoadScene("MainMenu");

    }
  }

  public void GameOver()
  {
    _isGameOver = true;
  }
}
