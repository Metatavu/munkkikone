using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class munkSpawner : MonoBehaviour {

  public Transform munkki;
  public float spawnDelay;

  private float timeSinceSpawned;

	// Use this for initialization
	void Start () {
    timeSinceSpawned = 0;
	}
	
	// Update is called once per frame
	void Update () {
    timeSinceSpawned += Time.deltaTime * 1000;
    if (timeSinceSpawned >= spawnDelay) {
      Instantiate(munkki, transform.position, Quaternion.identity);
      timeSinceSpawned = 0;
    }
	}
}
