using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Destructible : NetworkBehaviour {

    Player owner = null;

    public void setOwner(Player _owner)
    {
        owner = _owner;
        GetComponent<Renderer>().material.color = owner.color;
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
