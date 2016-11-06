using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Destructible : NetworkBehaviour {

    Player owner = null;
    [SyncVar] Color color;



    [Command]
    void CmdSetColor(Color _color)
    {
        color = _color;
        GetComponent<Renderer>().material.color = color;
    }

    public void setOwner(Player _owner)
    {
        owner = _owner;
        CmdSetColor(_owner.color);
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("collision");
        if (col.gameObject.tag == "Bullet")
        {
            if(col.gameObject.GetComponent<Bullet>().owner != owner)
            {
                Destroy(col.gameObject);
                CmdDestroy();
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
}
