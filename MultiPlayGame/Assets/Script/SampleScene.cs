using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class SampleScene : MonoBehaviourPunCallbacks
{
    private const string ROOMNAME_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";

    private void Start()
    {
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10; 

        PhotonNetwork.NickName = "Player";

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (PlayerPrefs.GetString("RoomFlag") == "Create")
        {
            string roomName = GenerateRoomID(10);
            PlayerPrefs.SetString("CreateRoomName", roomName);
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
        }
        else
        {
            Debug.Log(PlayerPrefs.GetString("JoinRoomName"));
            PhotonNetwork.JoinRoom(PlayerPrefs.GetString("JoinRoomName"));
        }
    }

    public override void OnJoinedRoom()
    {
        MasterClientCheck();
    }

    public void PlayersView()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }
    }

    private void MasterClientCheck()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(">>>>MasterClient<<<");

            PhotonNetwork.InstantiateRoomObject("RoomObject", Vector3.zero, Quaternion.identity);

            var position = new Vector3(0, 0, 0);
            GameObject player = PhotonNetwork.Instantiate("Player" + Random.Range(1, 5), position, Quaternion.identity);
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = true;
            PhotonNetwork.Instantiate("PlayerFlowCamera", position, Quaternion.identity);
            player.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName + "(MasterClient)");
            player.GetPhotonView().RPC("SetRoomName", RpcTarget.AllBuffered, PlayerPrefs.GetString("CreateRoomName"));
        }
        else
        {
            Debug.Log(">>>>local Player<<<");

            var position = new Vector3(0, 0, 0);
            GameObject player = PhotonNetwork.Instantiate("Player" + Random.Range(1, 5), position, Quaternion.identity);
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = true;
            PhotonNetwork.Instantiate("PlayerFlowCamera", position, Quaternion.identity);
            player.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName + "(local Player)");
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