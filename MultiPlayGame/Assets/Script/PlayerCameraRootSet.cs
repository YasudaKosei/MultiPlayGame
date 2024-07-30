using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRootSet : MonoBehaviourPunCallbacks
{
    public CinemachineVirtualCamera cvc;

    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(gameObject);
        }
        cvc.Follow = GameObject.FindGameObjectWithTag("CinemachineTarget").transform;
    }
}
