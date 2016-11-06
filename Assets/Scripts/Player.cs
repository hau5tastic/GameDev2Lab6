using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public Color color;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Start () {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        color = new Color(r, g, b);

        GetComponent<Renderer>().material.color = color;
	}

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Bullet>().SetOwner(this);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 25;
            NetworkServer.Spawn(bullet);
            Destroy(bullet, 0.5f);
    }
}
