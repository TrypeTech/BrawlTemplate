using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    // create a empty object call it death box
    // add a box collider to it  and set it to be the size of the bottom of the level
    // set the box collider to be trigger so when the player falls it triggers the respawn event

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStats.PlayerDie();
        }
    }
}
