using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

  [SerializeField] public float _rotateSpeed = 19f;
  [SerializeField] GameObject _explosionPrefab;
  [SerializeField] SpawnManager _spawnManager;

  // Start is called before the first frame update
  void Start()
  {
    _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Laser")
    {
      Destroy(other.gameObject);
      GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
      _spawnManager.StartSpawning();
      Destroy(this.gameObject, 0.25f);
      Destroy(explosion, 3f);
    }
  }


}
