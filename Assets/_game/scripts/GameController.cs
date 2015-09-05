using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;

    public Player[] player;

	public int leadingPlayer;

	public Material[] playerMat;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 2; i++) {
			player[i] = Instantiate(playerPrefab).GetComponent<Player>();
			player[i].GetComponent<MeshRenderer>().material = playerMat[i];
			player[i].gameObject.name = "player " + i;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player[0].transform.position.y > player[1].transform.position.y) {
			leadingPlayer = 0;
		}
		else {
			leadingPlayer = 1;
		}
		SlowMotion();
	}

	float slowMotionTime;

	void SlowMotion() {
		if (slowMotionTime > 0f) {
			slowMotionTime -= Time.deltaTime;
		}
		else {
			Time.timeScale = 1f;
		}

	}

	public void SetSlowMotion() {
		Time.timeScale = .01f;
		slowMotionTime = .0165f;
	}
}
