using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    private const string ROOMNAME_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";

    private void Start()
    {
        PhotonNetwork.SendRate = 20; // 1秒間にメッセージ送信を行う回数
        PhotonNetwork.SerializationRate = 10; // 1秒間にオブジェクト同期を行う回数

        // プレイヤー自身の名前を"Player"に設定する
        //ここを今後steamの名前にする
        PhotonNetwork.NickName = "くりーむの奴隷";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    //ここでロビーを作ったりマッチングを行う
    public override void OnConnectedToMaster()
    {
        if (PlayerPrefs.GetString("RoomFlag") == "Create")
        {
            // ルームを作成する
            string roomName = GenerateRoomID(10);
            PlayerPrefs.SetString("CreateRoomName", roomName);
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
        }
        else
        {
            //ルームに参加する
            PhotonNetwork.JoinRoom(PlayerPrefs.GetString("JoinRoomName"));
        }
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ローカルプレイヤーがマスタークライアントかどうかを判定する
        MasterClientCheck();
    }

    // ルーム内のプレイヤー全員のプレイヤー名とIDをコンソールに出力する
    public void PlayersView()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }
    }

    // ローカルプレイヤーがマスタークライアントかどうかを判定する
    private void MasterClientCheck()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("あなたはマスタークライアントです");

            PhotonNetwork.InstantiateRoomObject("RoomObject", Vector3.zero, Quaternion.identity);

            //自身のアバタ-を生成する
            var position = new Vector3(0, 0, 0);
            GameObject player = PhotonNetwork.Instantiate("Player" + Random.Range(1, 5), position, Quaternion.identity);
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = true;
            PhotonNetwork.Instantiate("PlayerFlowCamera", position, Quaternion.identity);
            player.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName + "(マスター)");
            player.GetPhotonView().RPC("SetRoomName", RpcTarget.AllBuffered, PlayerPrefs.GetString("CreateRoomName"));
        }
        else
        {
            Debug.Log("あなたはローカルプレイヤーです");

            //自身のアバタ-を生成する
            var position = new Vector3(0, 0, 0);
            GameObject player = PhotonNetwork.Instantiate("Player" + Random.Range(1, 5), position, Quaternion.identity);
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = true;
            PhotonNetwork.Instantiate("PlayerFlowCamera", position, Quaternion.identity);
            player.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName + "(ローカル)");
        }
    }

    public static string GenerateRoomID(int length)
    {
        var sb = new System.Text.StringBuilder(length);
        var r = new System.Random();

        for (int i = 0; i < length; i++)
        {
            int pos = r.Next(ROOMNAME_CHARS.Length);
            char c = ROOMNAME_CHARS[pos];
            sb.Append(c);
        }

        return sb.ToString();
    }
}