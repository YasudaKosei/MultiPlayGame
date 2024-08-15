using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PohotonMoveScene : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("TestMoveScene");
        }
    }
}
