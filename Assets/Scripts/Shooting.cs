using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Shooting : MonoBehaviour
{

    [SerializeField] Camera fpsCamera;
    [SerializeField] float shootDelay = .1f;

    bool delayed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !delayed)
        {
            StartCoroutine(FireDelay());
            Shoot();
        }
    }
    private IEnumerator FireDelay()
    {
        delayed = true;
        yield return new WaitForSeconds(shootDelay);
        delayed = false;
    }
    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(.5f, .5f));
        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject player = hit.collider.gameObject;
            if (!player.CompareTag("Player") || player.GetComponent<PhotonView>().IsMine) return;

            player.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.AllBuffered, 20f);
            

        }
    }
}
