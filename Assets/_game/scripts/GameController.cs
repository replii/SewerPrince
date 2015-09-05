using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;

    public Player[] player;

	public int leadingPlayer;

	// Use this for initialization
	void Start () {
		player[0] = Instantiate(playerPrefab).GetComponent<Player>();
		player[1] = Instantiate(playerPrefab).GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player[0].transform.position.y > player[1].transform.position.y) {
			leadingPlayer = 0;
		}
		else {
			leadingPlayer = 1;
		}
	}
}
