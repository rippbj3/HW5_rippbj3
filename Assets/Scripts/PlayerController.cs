using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private AudioSource audio;
	private int count;
	private Vector3 jump;
	private bool canJump = false;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		if (Input.GetKeyDown (KeyCode.Space) && canJump) {
			jump = new Vector3 (0.0f, 250.0f, 0.0f);
			canJump = false;
		} else {
			jump = Vector3.zero;
		}

		rb.AddForce ((movement * speed) + jump);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name.Equals("Ground")) {
			canJump = true;
		}
		if(other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.transform.parent.gameObject.SetActive(false);
			audio.Play ();
			count++;
			SetCountText ();
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString ();
		if(count >= 12) {
			winText.text = "You Win!";
		}
	}
}
