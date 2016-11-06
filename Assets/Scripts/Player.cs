using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Color color;

	void Start () {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        color = new Color(r, g, b);

        GetComponent<Renderer>().material.color = color;
	}
}
