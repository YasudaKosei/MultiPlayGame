using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class OperationName : MonoBehaviourPunCallbacks
{

    private GameObject namePlate;
    public Text otherNameText;
    public Text myNameText;
    public Text roomNameText;

    void Start()
    {
        namePlate = otherNameText.transform.parent.gameObject;
    }

    void LateUpdate()
    {
        namePlate.transform.rotation = Camera.main.transform.rotation;
    }

    [PunRPC]
    void SetName(string name)
    {
        if (!photonView.IsMine) otherNameText.text = name;
        else myNameText.text = name;
    }

    [PunRPC]
    void SetRoomName(string name)
    {
        roomNameText.text = "Room ID : " + name;
    }
}