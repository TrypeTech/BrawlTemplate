using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandeler : MonoBehaviourPunCallbacks
{

    [Header("Tutorial Game Manager")]
    public GameObject PlayerPrefab;

    [HideInInspector]
    public PlayerStats localPlayer;

    public Transform SpawnPostion;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Menu");
            return;
        }
    }


    void Start()
    {
      //  PlayerStats.RefreshInstance(ref localPlayer, PlayerPrefab);

    }

    // when player enters the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //  PlayerStats.RefreshInstance(ref localPlayer, PlayerPrefab);
        PhotonNetwork.Instantiate(PlayerPrefab.gameObject.name, SpawnPostion.position, Quaternion.identity).GetComponent<PlayerStats>();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
