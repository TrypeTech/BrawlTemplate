using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{

    public Button BtnConnectMaster;
    public Button BtnConnectRoom;

    public bool TriesToConnectToMaster;
    public bool TriesToConnectToRoom;

    public string NetworkLevelToLoad = "TestLevel1";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(BtnConnectMaster != null)
        BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        if(BtnConnectRoom != null)
        BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToMaster && !TriesToConnectToRoom);
    }

   public void OnClickConnectToMaster()
    {
        //Settings (all optional and only for tutoiral purpose)
        PhotonNetwork.OfflineMode = false;  // true would "Fake " and online connection
        PhotonNetwork.NickName = " PlayerName";  // to set a player name
      //  PhotonNetwork.AutomaticallySyncScene = true;   // to call photonnetwork.loadlevel();
        PhotonNetwork.GameVersion = "v1";   // only people witht he same game version can play together


        TriesToConnectToMaster = true;
        // PhotonNetwork.ConnectToMaster(ip,port,appid); // manual Connection
        PhotonNetwork.ConnectUsingSettings();  //autamatic connection based ont he config file in photn /photnonunitynetoworking/resource/ photonserve
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        Debug.Log("Conncected to Master");
    }


    // on click join room

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        TriesToConnectToRoom = true;
        // PhotonNetwork.CreateRoom("Peter's Game 1"); // Create a specitifc Room - Error: oncreateRoom Fail
        // PhotonNetwork.JoinRoom("Peter's Game 1"); // Join a Specific Room - Error: OnJoinRoomFailed
        PhotonNetwork.JoinRandomRoom();

    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        TriesToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | Room");
        SceneManager.LoadScene(NetworkLevelToLoad);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        // no room available
        // create a room (null as a name " does not matter")
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
       
        TriesToConnectToRoom = false;
    }

}
