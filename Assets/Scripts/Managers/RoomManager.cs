using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 2)
        {
            PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
        }
    }
}
