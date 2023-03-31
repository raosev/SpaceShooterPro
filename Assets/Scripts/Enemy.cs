using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  [SerializeField] private float _speed = 4f;

  private float minYCameraBoundary = -8f;
  private float maxYCameraBoundary = 7f;
  private float minXCameraBoundary = -9f;
  private float maxXCameraBoundary = 9f;
  Player player;
  Animator _animator;

  AudioSource audioSource;


  // Update is called once per frame
  private void Start()
  {
    player = GameObject.Find("Player").GetComponent<Player>();
    audioSource = GetComponent<AudioSource>();
    _animator = GetComponent<Animator>();
  }


  void Update()
  {
    transform.Translate(Vector3.down * Time.deltaTime * _speed);

    if (transform.position.y < minYCameraBoundary)
    {
      float newX = Random.Range(minXCameraBoundary, maxXCameraBoundary);
      transform.position = new Vector3(newX, maxYCameraBoundary, 0);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {

    if (other.tag == "Laser")
    {
      Destroy(other.gameObject);
      player.IncreaseScore(10);
      _speed = 0;
      _animator.SetTrigger("OnEnemyDeath");
      audioSource.Play();
      Destroy(GetComponent<Collider2D>());
      Destroy(this.gameObject, 2.8f);
    }


    if (other.tag == "Player")
    {
      player.Damage();
      _speed = 0;
      _animator.SetTrigger("OnEnemyDeath");
      audioSource.Play();
      Destroy(GetComponent<Collider2D>());
      Destroy(this.gameObject, 2.8f);
    }

  }
}
