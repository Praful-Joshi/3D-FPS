using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput, joinInput;

    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void joinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
