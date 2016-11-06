using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerView : NetworkBehaviour {


    private float XSensitivity = 10;
    private float YSensitivity = 10;

    private Transform charTransform;
    private Transform charCamera;
    public GameObject gun;

    void Start () {
        if (!isLocalPlayer)
            GetComponentInChildren<Camera>().enabled = false;

        charTransform = GetComponent<Transform>();
        charCamera = GetComponentInChildren<Camera>().transform;


        Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = false;
    }
	
	void Update () {
        UpdateView();
    }

    private void UpdateView() {
        if (!isLocalPlayer) return;

        float xRot = Input.GetAxis("Mouse X") * XSensitivity;
        float yRot = Input.GetAxis("Mouse Y") * YSensitivity;

        charCamera.localRotation *= Quaternion.Euler(-yRot, 0, 0f);
        charTransform.localRotation *= Quaternion.Euler(0, xRot, 0f);
        gun.transform.localRotation *= Quaternion.Euler(-yRot, 0, 0f);

        Debug.DrawRay(gun.transform.position, gun.transform.forward.normalized * 5.0f, Color.green);
    }
}
