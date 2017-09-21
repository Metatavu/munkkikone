using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class munkkiDestructor : MonoBehaviour {

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag.Equals("munkki")) {
      Destroy(collision.gameObject);
    }
  }

}
