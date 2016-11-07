using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar(hook = "OnColor")]
    public Color color;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    public override void OnStartClient () {
        base.OnStartClient();

        // Color newColor = new Color(Random.value, Random.value, Random.value);
        // OnColor(newColor);
    }


    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();

        Color newColor = new Color(Random.value, Random.value, Random.value);
        OnColor(newColor);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (isServer) {
            Debug.DrawRay(transform.position, Vector3.up * 2.0f, Color.red);
        }

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

    [Command]
    void CmdChangeColor(Color newColor) {
        color = newColor;
        GetComponent<Renderer>().material.color = newColor;
    }

    [ClientRpc]
    void RpcChangeColor(Color newColor) {
        color = newColor;
        GetComponent<Renderer>().material.color = newColor;
    }

    void OnColor(Color newColor) {
        CmdChangeColor(newColor);
        RpcChangeColor(newColor);
        GetComponent<Renderer>().material.color = newColor;
    }
}
