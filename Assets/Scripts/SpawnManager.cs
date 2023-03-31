using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

  [SerializeField] GameObject _enemyPrefab;
  [SerializeField] GameObject _enemyContainer;

  [SerializeField] GameObject[] _powerups;


  bool _stopSpawning = false;


  public void StartSpawning()
  {
    StartCoroutine(SpawnEnemyRoutine());
    StartCoroutine(SpawnPowerUpRoutine());
  }


  IEnumerator SpawnEnemyRoutine()
  {

    yield return new WaitForSeconds(3.0f);
    while (!_stopSpawning)
    {
      Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
      Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity, _enemyContainer.transform);

      yield return new WaitForSeconds(5);
    }
  }

  IEnumerator SpawnPowerUpRoutine()
  {
    yield return new WaitForSeconds(3.0f);
    while (!_stopSpawning)
    {
      Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
      GameObject powerup = _powerups[Random.Range(0, _powerups.Length)];
      Instantiate(powerup, postToSpawn, Quaternion.identity);

      yield return new WaitForSeconds(Random.Range(7, 10));
    }
  }


  public void OnPlayerDeath()
  {
    _stopSpawning = true;
  }

}
