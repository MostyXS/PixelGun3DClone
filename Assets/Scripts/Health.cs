using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] Image healthBar = null;
    [SerializeField] float health = 100f;
     float maxHealth;

    private void Awake()
    {
        maxHealth = health;
        healthBar.fillAmount = health / maxHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if(health<=0)
        {
            Die();
        }
    }


    private void Die()
    {
        if (!photonView.IsMine) return;
        GameManager.Instance.LeaveRoom();
        Cursor.lockState = CursorLockMode.None;


    }
}
