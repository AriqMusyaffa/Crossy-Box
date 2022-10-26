using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] GameObject player;
    Player p;

    void Start()
    {
        p = player.GetComponent<Player>();
    }

    void Update()
    {
        transform.rotation = player.transform.rotation;

        if (p.standingOnLog || p.standingOnLily)
        {
            transform.position = new Vector3(player.transform.position.x, 0.075f, player.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, 0.025f, player.transform.position.z);
        }
        
    }
}
