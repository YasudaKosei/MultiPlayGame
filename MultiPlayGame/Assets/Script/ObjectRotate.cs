using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ObjectRotate : MonoBehaviourPunCallbacks
{
    private float rotationSpeed = 5.0f;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RequestRotation", RpcTarget.MasterClient);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
    }

    [PunRPC]
    void RequestRotation()
    {
        photonView.RPC("SetRotation", RpcTarget.Others, transform.rotation.eulerAngles.x);
    }

    [PunRPC]
    void SetRotation(float xRotation)
    {
        transform.rotation = Quaternion.Euler(xRotation, 0, 0);
    }
}


