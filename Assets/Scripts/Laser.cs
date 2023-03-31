using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
  [SerializeField] private float speed = 10f;


  void Update()
  {
    transform.Translate(Vector3.up * speed * Time.deltaTime);

    if (transform.position.y > 8)
    {

      if (this.transform.parent != null)
      {
        Destroy(this.transform.parent.gameObject);
      }
      Destroy(this.gameObject);
    }

  }
}
