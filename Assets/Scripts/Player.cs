using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed = 5f;
  public float speedUp = 10f;

  [SerializeField] private float fireRate = 0.15f;
  private float canFire = -1f;

  [SerializeField] private int _lives = 3;

  [SerializeField] private GameObject _laserPrefab;
  [SerializeField] private GameObject _tripleShotPrefab;
  [SerializeField] private GameObject _shieldVisualizer;

  [SerializeField] private GameObject _leftEngineDamage;
  [SerializeField] private GameObject _rightEngineDamage;

  private AudioSource _audioSource;
  [SerializeField] private AudioClip _laserSound;


  private SpawnManager _spawnManager;
  private UIManager _uiManager;
  [SerializeField] bool _isTripleShotActive = false;
  [SerializeField] bool _isSpeedActive = false;
  [SerializeField] bool _isShieldActive = false;

  [SerializeField] private int _score = 0;


  void Start()
  {
    transform.position = new Vector3(0, 0, 0);
    _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    _audioSource = GetComponent<AudioSource>();

    if (_spawnManager == null)
    {
      Debug.LogError("Could not grab spawn manager");
    }

    if (_uiManager == null)
    {
      Debug.LogError("Could not grab UI Manager");
    }


  }

  // Update is called once per frame
  void Update()
  {
    CalculatMovement();
    if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
    {
      FireLaser();
    }
  }


  void CalculatMovement()
  {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    float movementSpeed;
    if (_isSpeedActive)
    {
      movementSpeed = speedUp;
    }
    else
    {
      movementSpeed = speed;
    }

    transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * movementSpeed * Time.deltaTime);

    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);


    if (transform.position.x >= 11.3f)
    {
      transform.position = new Vector3(-11.3f, transform.position.y);
    }
    else if (transform.position.x < -11.3f)
    {
      transform.position = new Vector3(11.3f, transform.position.y);
    }

  }

  void FireLaser()
  {

    if (_isTripleShotActive)
    {
      Vector3 position = transform.position;
      Instantiate(_tripleShotPrefab, position, Quaternion.identity);
    }
    else
    {
      Vector3 position = transform.position + new Vector3(0, 1.05f, 0);
      Instantiate(_laserPrefab, position, Quaternion.identity);
    }

    canFire = Time.time + fireRate;

    _audioSource.PlayOneShot(_laserSound);


  }

  public void Damage()
  {

    if (_isShieldActive)
    {
      DeactivateShield();
      return;
    }

    _lives -= 1;
    _uiManager.UpdateLives(_lives);

    if (_lives == 2)
    {
      _leftEngineDamage.SetActive(true);
    }

    if (_lives == 1)
    {
      _rightEngineDamage.SetActive(true);
    }


    if (_lives < 1)
    {
      _spawnManager.OnPlayerDeath();
      Destroy(this.gameObject);
    }
  }


  public void ActivateTripleShot()
  {
    _isTripleShotActive = true;
    StopCoroutine("TripleShotPowerDown");
    StartCoroutine("TripleShotPowerDown");
  }

  public void ActivateSpeed()
  {
    _isSpeedActive = true;
    StopCoroutine("SpeedPowerDown");
    StartCoroutine("SpeedPowerDown");
  }

  public void ActivateShield()
  {
    _isShieldActive = true;
    _shieldVisualizer.SetActive(true);
  }

  void DeactivateShield()
  {
    _isShieldActive = false;
    _shieldVisualizer.SetActive(false);
  }


  IEnumerator TripleShotPowerDown()
  {
    yield return new WaitForSeconds(5);
    _isTripleShotActive = false;
  }

  IEnumerator SpeedPowerDown()
  {
    yield return new WaitForSeconds(5);
    _isSpeedActive = false;
  }

  public void IncreaseScore(int points)
  {
    _score += points;
    _uiManager.UpdateScoreText(_score);
  }

}
