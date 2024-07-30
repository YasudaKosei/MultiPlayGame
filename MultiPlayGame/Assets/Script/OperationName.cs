using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class OperationName : MonoBehaviourPunCallbacks
{

    private GameObject namePlate;   //　名前を表示しているプレート
    public Text otherNameText;   //　名前を表示するテキスト
    public Text myNameText;

    void Start()
    {
        namePlate = otherNameText.transform.parent.gameObject;
    }

    void LateUpdate()
    {
        //名前が常にカメラの向きになる
        //なぜLateUpdateで行っているかというと名前はキャラクターの子要素になっていてキャラクターを動かすと同じ向きに名前も回転しますが、
        //LateUpdateで名前をカメラの向きにすればキャラクターの移動を考慮した後に名前をカメラの向きにしてその後描画されるのでちゃんと表示されます。
        namePlate.transform.rotation = Camera.main.transform.rotation;
    }

    [PunRPC]
    void SetName(string name)
    {
        if (!photonView.IsMine) otherNameText.text = name;
        else myNameText.text = name;
    }
}