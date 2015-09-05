using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float toungeSpeed = 1;
	public float toungeCooldown = 1;
	public float maxRange;
	public float playerSpeed = 1;
	public float wallRunSpeed = 5;
	[Space(10f)]
	public LayerMask toungeLayer;
	public LayerMask stickyLayer;
	public LayerMask slimeLayer;
	public LineRenderer lineRenderer;
	public HookshotState currentHookshotState;

	Vector3 joystickRight;
	Vector3 joystickLeft;

	Rigidbody2D rb;
	GameController gc;

	//Input
	string rightTrigger;
	string rightHorizontal;
	string rightVertical;
	string leftHorizontal;
	string leftVertical;


	// Use this for initialization
	void Start() {
		gc = FindObjectOfType<GameController>();
		rb = GetComponent<Rigidbody2D>();

		if (gc.player[0] == this) {
			rightTrigger = "RightTrigger0";
			rightHorizontal = "RightHorizontal0";
			rightVertical = "RightVertical0";
			leftHorizontal = "LeftHorizontal0";
			leftVertical = "LeftVertical0";
		}
		else if (gc.player[1] == this) {
			rightTrigger = "RightTrigger1";
			rightHorizontal = "RightHorizontal1";
			rightVertical = "RightVertical1";
			leftHorizontal = "LeftHorizontal1";
			leftVertical = "LeftVertical1";
		}
	}

	// Update is called once per frame
	void Update() {
		GetInput();
		Hookshot();
	}

	void FixedUpdate() {
		WallRun();
		Strafe();
	}

	void GetInput() {
		joystickLeft = new Vector3(
			Input.GetAxis(leftHorizontal),
			Input.GetAxis(leftVertical),
			0);
		if (new Vector3(
			Input.GetAxis(rightHorizontal),
			Input.GetAxis(rightVertical),
			0) != Vector3.zero) {
			joystickRight = new Vector3(
			Input.GetAxis(rightHorizontal),
			Input.GetAxis(rightVertical),
			0);
		}
	}

	Vector3 savedJoystickRight;
	Vector3 savedPlayerPos;

	float toungeLength;
	RaycastHit2D toungeRaycast;
	float currentToungeCooldown;

	void Hookshot() {
		lineRenderer.SetPosition(0, transform.position);

		currentToungeCooldown -= Time.deltaTime;

		switch (currentHookshotState) {
			case HookshotState.Available:

				lineRenderer.SetPosition(1, (new Vector3(0, .01f) + joystickRight).normalized + transform.position);

				if (Input.GetAxis(rightTrigger) < 0) {
					savedJoystickRight = (new Vector3(0, .01f) + joystickRight).normalized;
					savedPlayerPos = transform.position;
					toungeLength = 1;
					currentHookshotState = HookshotState.Shooting;
				}
				break;

			case HookshotState.Unavailable:
				lineRenderer.SetPosition(1, transform.position);
				if (currentToungeCooldown <= 0) {
					currentHookshotState = HookshotState.Available;
				}
				break;

			case HookshotState.Shooting:
				currentToungeCooldown = toungeCooldown;
				toungeLength = toungeLength + Time.deltaTime * toungeSpeed;
				Vector3 lineEnd = savedJoystickRight * toungeLength + savedPlayerPos;
				lineRenderer.SetPosition(1, lineEnd);
				toungeRaycast = Physics2D.CircleCast(transform.position, .2f, savedJoystickRight, toungeLength, toungeLayer);
				if (toungeRaycast.collider != null) { //Träffar ngt

					if (toungeRaycast.collider.tag == "Player" && toungeRaycast.collider.name != this.name) {
						currentHookshotState = HookshotState.Switch;
					}
					else {
						savedJoystickRight = transform.position;
						currentHookshotState = HookshotState.Dragging;
					}
                    
				}
				else if ((transform.position - lineEnd).magnitude > maxRange) { //Max range
					currentHookshotState = HookshotState.Unavailable;
				}
				break;

			case HookshotState.Dragging:
				transform.position = Vector3.Lerp(transform.position, toungeRaycast.point, Time.deltaTime * toungeSpeed);
				if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), toungeRaycast.point) < .5f) {
					rb.velocity = Vector3.zero;
					currentHookshotState = HookshotState.Unavailable;
				}
				break;

			case HookshotState.Switch:



				Vector3 myPosition = transform.position;
				transform.position = toungeRaycast.collider.transform.position;
				toungeRaycast.collider.transform.position = myPosition;
				rb.velocity = new Vector2(rb.velocity.x, wallRunSpeed);
				toungeRaycast.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				gc.SetSlowMotion();


				currentHookshotState = HookshotState.Unavailable;
				break;

			default:
				break;
		}

	}

	void Strafe() {
		if (currentHookshotState != HookshotState.Dragging) {
			rb.velocity = new Vector2(joystickLeft.x * playerSpeed, rb.velocity.y);
		}
	}

	void WallRun() {
		if (currentHookshotState != HookshotState.Dragging) {

			if (Physics2D.OverlapCircle(transform.position + Vector3.down * .2f, .6f, stickyLayer)) {
				rb.velocity = new Vector2(rb.velocity.x, wallRunSpeed * joystickLeft.y);
			}
			else if (Physics2D.OverlapCircle(transform.position + Vector3.down * .2f, .6f, slimeLayer)) {
				rb.velocity = new Vector2(rb.velocity.x, wallRunSpeed/4 * joystickLeft.y);
			} 

		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		print(other.name);
	}
}