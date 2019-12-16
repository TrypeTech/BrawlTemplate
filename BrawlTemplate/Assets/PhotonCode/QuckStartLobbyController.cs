using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuckStartLobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject quickStartButton; // Button used for creating and joining a game.
    [SerializeField]
    private GameObject quickCancelButton; // button used to stop searching for a game to join.
    [SerializeField]
    private int RoomSize; // Mmaual set the nuber of player in the room at one time.

    public override void OnConnectedToMaster() // Callback function for when the first connection is established
    {
        base.OnConnectedToMaster();
        PhotonNetwork.AutomaticallySyncScene = true; // Makes it so whatever scene the master client has
        quickStartButton.SetActive(true);
    }


    public void QuickStart() // Paired to the quck Start Button
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); // First tries to jjoin and existing room
        Debug.Log("Quick Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom() //trying to create our own room
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000); // creating a radom name for the room
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); // attemting to create a new room
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) // calback function for if create room failed
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Failed to create room... trying again");
        CreateRoom(); // Retrying to create a new room with a different name.

    }

    public void QuckCancel() // Paired to the canecl button. use to stop lookin for a room to join.
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

}
