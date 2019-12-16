using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{

    // scene navigation index
    [SerializeField]
    private int waitingRoomSceneIndex = 1;
    public override void OnEnable()
    {
        base.OnEnable();
        // register to photn callback functions
            PhotonNetwork.AddCallbackTarget(this);
       
    }

    public override void OnDisable()
    {
        base.OnDisable();
        // unregister to photon callback functions
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() // callback function for when we successfulluy create or join a game
    {
        // called when our player joins the room
        // load into waiting room scene
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
