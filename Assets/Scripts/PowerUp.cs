using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

  [SerializeField] private float _speed = 3f;

  // 0 = Triple Shot
  // 1 = Speed
  // 2 = Shield
  [SerializeField] private int _powerUpId;
  [SerializeField] private AudioClip _clip;
  private float minYCameraBoundary = -8f;


  void Update()
  {
    transform.Translate(Vector3.down * Time.deltaTime * _speed);

    if (transform.position.y < minYCameraBoundary)
    {
      Destroy(this.gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {

    if (other.tag == "Player")
    {
      Player player = other.GetComponent<Player>();

      // AudioSource.PlayClipAtPoint(_clip, transform.position + new Vector3(0, 0, -10), 3f);
      AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
      // Debug.Break();

      switch (_powerUpId)
      {
        case 0:
          player.ActivateTripleShot();
          break;
        case 1:
          player.ActivateSpeed();
          break;
        case 2:
          player.ActivateShield();
          break;
        default:
          Debug.Log("default value");
          break;
      }

      Destroy(this.gameObject);
    }
  }


}
