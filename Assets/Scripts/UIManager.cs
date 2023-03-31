using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
  // Start is called before the first frame update

  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _gameOverText;
  [SerializeField] private TextMeshProUGUI _restartLevelText;
  [SerializeField] private GameManager _gameManager;


  [SerializeField] private Sprite[] _liveSprites;
  [SerializeField] private Image _livesImage;


  void Start()
  {
    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  public void UpdateScoreText(int newScore)
  {
    _scoreText.text = "Score: " + newScore;
  }

  public void UpdateLives(int currentLives)
  {
    _livesImage.sprite = _liveSprites[currentLives];

    if (currentLives == 0)
    {
      _gameManager.GameOver();
      _restartLevelText.gameObject.SetActive(true);
      StartCoroutine("ShowGameOver");
    }
  }

  IEnumerator ShowGameOver()
  {
    while (true)
    {
      _gameOverText.gameObject.SetActive(true);
      yield return new WaitForSeconds(0.5f);
      _gameOverText.gameObject.SetActive(false);
      yield return new WaitForSeconds(0.5f);
    }

  }


}
