using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour { 


	[Space(10f)]

	public float cameraSpeed;

	GameController gc;


	// Use this for initialization
	void Start () {
		gc = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp(
			transform.position, 
			new Vector3(
				gc.player[gc.leadingPlayer].transform.position.x,
				gc.player[gc.leadingPlayer].transform.position.y,
				transform.position.z), 
			Time.deltaTime * cameraSpeed);

	}
}
