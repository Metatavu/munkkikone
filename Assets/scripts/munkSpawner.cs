using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class munkSpawner : MonoBehaviour {

  public Transform munkki;
  public float spawnDelay;

  private float timeSinceSpawned;
  private bool paused;

	// Use this for initialization
	void Start () {
    paused = false;
    timeSinceSpawned = 0;
	}

  public void pause() {
    paused = true;
  }

  public void resume() {
    paused = false;
  }

  // Update is called once per frame
  void Update () {
    if (paused) {
      timeSinceSpawned = 0;
      return;
    }

    timeSinceSpawned += Time.deltaTime * 1000;
    if (timeSinceSpawned >= spawnDelay) {
      Instantiate(munkki, transform.position, Quaternion.identity);
      timeSinceSpawned = 0;
    }
	}
}
