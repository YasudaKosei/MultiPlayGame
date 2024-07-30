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
            // 非マスタークライアントである場合、回転情報をマスタークライアントにリクエスト
            photonView.RPC("RequestRotation", RpcTarget.MasterClient);
        }
    }

    void Update()
    {
        // オブジェクトを一定の速度で回転させる
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
    }

    [PunRPC]
    void RequestRotation()
    {
        // マスタークライアントがこのメソッドを受け取った際に、現在の回転角度を他のクライアントに送信する
        photonView.RPC("SetRotation", RpcTarget.Others, transform.rotation.eulerAngles.x);
    }

    [PunRPC]
    void SetRotation(float xRotation)
    {
        // 受け取った回転角度をもとに、オブジェクトの回転を設定
        transform.rotation = Quaternion.Euler(xRotation, 0, 0);
    }
}


