using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


public class Capture : NetworkBehaviour {

    public float captureTime;

    public GameObject flag;

    [SerializeField]
    private float currentTime;

    [SerializeField]
    private Player owner = null;

    [SerializeField]
    List<Player> playersInPoint;

    [SerializeField]
    private int playersWithinRadius = 0;

    void Start () {
        playersInPoint = new List<Player>();
        currentTime = captureTime;
	}
	

	void Update () {
        
        if (CaptureConditionsOK()) {
            Debug.Log( "Capture Conditions Passed" );
            if (CapturePoint()) {
                owner = playersInPoint[0];
                flag.GetComponent<Renderer>().material.color = playersInPoint[0].color;
            }
        }
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            playersWithinRadius++;
            playersInPoint.Add(col.gameObject.GetComponent<Player>());
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            playersWithinRadius--;
            playersInPoint.Remove(col.gameObject.GetComponent<Player>());
        }
    }

    private bool CapturePoint() {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) {
            currentTime = captureTime;
            return true;
        }
        return false;
    }

    private bool CaptureConditionsOK() {
        if (playersWithinRadius == 1 && playersInPoint[0] != owner) {
            return true;
        }
        return false;
    }
}
