using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Destructible : NetworkBehaviour {

    Player owner = null;
    Capture capture = null;

    [SyncVar]
    Color color;

    public void setOwner(Player _owner)
    {
        owner = _owner;
        OnColor(owner.color);
    }

    public void setCapture(Capture _capture)
    {
        capture = _capture;
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("collision");
        if (col.gameObject.tag == "Bullet")
        {
            if(col.gameObject.GetComponent<Bullet>().owner != owner)
            {
                Destroy(col.gameObject);
                OnDestroy();
            }
            else
            {
                Debug.Log("owner hit");
            }
        }
    }

    [Command]
    void CmdDestroy()
    {
        NetworkServer.Destroy(this.gameObject);
    }

    [ClientRpc]
    void RpcDestroy()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        CmdDestroy();
        RpcDestroy();
        capture.spawnOccupied = false;
        Destroy(this.gameObject);
    }

    [Command]
    void CmdSetColor(Color _color)
    {
        color = _color;
        GetComponent<Renderer>().material.color = color;
    }

    [ClientRpc]
    void RpcChangeColor(Color newColor)
    {
        color = newColor;
        GetComponent<Renderer>().material.color = newColor;
    }

    void OnColor(Color newColor)
    {
        CmdSetColor(newColor);
        RpcChangeColor(newColor);
        GetComponent<Renderer>().material.color = newColor;
    }
}
