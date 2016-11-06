using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Player owner;

    public void SetOwner(Player player)
    {
        owner = player;
    }
}
