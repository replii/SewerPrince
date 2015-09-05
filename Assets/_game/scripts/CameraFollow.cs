using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour { 

	public float cameraSpeed;

	GameController gc;

	public bool splitScreen;
	public int camNumber;

	// Use this for initialization
	void Start () {
		gc = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (splitScreen) {
			transform.position = Vector3.Lerp(
			transform.position,
			new Vector3(
				gc.player[camNumber].transform.position.x,
				gc.player[camNumber].transform.position.y,
				transform.position.z),
			Time.deltaTime * cameraSpeed);
		}
		else {
			transform.position = Vector3.Lerp(
			transform.position,
			new Vector3(
				gc.player[gc.leadingPlayer].transform.position.x,
				gc.player[gc.leadingPlayer].transform.position.y,
				transform.position.z),
			Time.deltaTime * cameraSpeed);
		}

		

	}
}
