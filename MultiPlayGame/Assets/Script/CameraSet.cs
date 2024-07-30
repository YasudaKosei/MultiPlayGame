using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Security.Cryptography;
using UnityEditor.Rendering;

public class CameraSet : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!photonView.IsMine) return;
        gameObject.tag = "MainCamera";
    }
}
