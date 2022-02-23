using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;

    private void Awake()
    {
        pv = this.GetComponent<PhotonView>();
    }

    void Start()
    {
        if(pv.IsMine)
        {
            createController();
        }
    }

    private void createController()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0,2,0), Quaternion.identity);
    }
}
