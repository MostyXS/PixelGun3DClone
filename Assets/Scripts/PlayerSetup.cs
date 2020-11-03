using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject FPSCamera = null;
    [SerializeField] TextMeshProUGUI playerName = null;

    void Awake()
    {
        bool amIPlayer = photonView.IsMine;
        GetComponent<MovementController>().enabled = amIPlayer;
        FPSCamera.GetComponent<Camera>().enabled = amIPlayer;

        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if (!playerName) return;
        playerName.text = photonView.Owner.NickName;
    }
}
