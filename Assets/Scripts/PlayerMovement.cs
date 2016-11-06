using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

    private Transform charTransform;
    private float moveSpeed = 10.0f;

	void Start () {
        charTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
	}

    private void UpdateMovement() {
        if (!isLocalPlayer) return;

        float xMove = Input.GetAxis("Vertical");
        float yMove = Input.GetAxis("Horizontal");

        charTransform.position += charTransform.forward * xMove * moveSpeed * Time.deltaTime;
        charTransform.position += charTransform.right * yMove * moveSpeed * Time.deltaTime;
    }
}
