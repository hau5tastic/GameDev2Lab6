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
                // Debug.Log("owner hit");
            }
        }
    }

    [Command]
    void CmdDestroy()
    {
        RpcDestroy();
        capture.spawnOccupied = false;
    }

    [ClientRpc]
    void RpcDestroy()
    {
        capture.spawnOccupied = false;
        NetworkServer.Destroy(gameObject);
    }

    void OnDestroy()
    {
        CmdDestroy();
    }




    // ---------
    [Command]
    void CmdSetColor(Color _color)
    {
        RpcChangeColor(_color);
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
        GetComponent<Renderer>().material.color = newColor;
    }
}
