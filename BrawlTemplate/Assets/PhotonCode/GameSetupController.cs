using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetupController : MonoBehaviour
{
    public Transform SpawnPoint;
    public string ResourcesFolderPlayerIsIn = "PhotonPrefabs";  // exabmple  Resources/ResoursesFolderPlayerisIn/PlayerPrefabName <<
    public string PlayerPrefabName = "Player";
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer scene
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine(ResourcesFolderPlayerIsIn, PlayerPrefabName), SpawnPoint.position, Quaternion.identity);
    }
}
