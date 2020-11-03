﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab = null;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            if (!playerPrefab) return;
            int randomPoint = Random.Range(-20, 20);

            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity); 
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
