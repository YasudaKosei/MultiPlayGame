using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks
{
    private const string ROOMNAME_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";

    private void Start()
    {
        PhotonNetwork.SendRate = 20; // 1�b�ԂɃ��b�Z�[�W���M���s����
        PhotonNetwork.SerializationRate = 10; // 1�b�ԂɃI�u�W�F�N�g�������s����

        // �v���C���[���g�̖��O��"Player"�ɐݒ肷��
        //����������steam�̖��O�ɂ���
        PhotonNetwork.NickName = "����[�ނ̓z��";

        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    //�����Ń��r�[���������}�b�`���O���s��
    public override void OnConnectedToMaster()
    {
        if (PlayerPrefs.GetString("RoomFlag") == "Create")
        {
            // ���[�����쐬����
            string roomName = GenerateRoomID(10);
            PlayerPrefs.SetString("CreateRoomName", roomName);
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
        }
        else
        {
            //���[���ɎQ������
            PhotonNetwork.JoinRoom(PlayerPrefs.GetString("JoinRoomName"));
        }
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // ���[�J���v���C���[���}�X�^�[�N���C�A���g���ǂ����𔻒肷��
        MasterClientCheck();
    }

    // ���[�����̃v���C���[�S���̃v���C���[����ID���R���\�[���ɏo�͂���
    public void PlayersView()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }
    }

    // ���[�J���v���C���[���}�X�^�[�N���C�A���g���ǂ����𔻒肷��
    private void MasterClientCheck()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("���Ȃ��̓}�X�^�[�N���C�A���g�ł�");

            PhotonNetwork.InstantiateRoomObject("RoomObject", Vector3.zero, Quaternion.identity);

            //���g�̃A�o�^-�𐶐�����
            var position = new Vector3(0, 0, 0);
            GameObject player = PhotonNetwork.Instantiate("Player" + Random.Range(1, 5), position, Quaternion.identity);
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = true;
            PhotonNetwork.Instantiate("PlayerFlowCamera", position, Quaternion.identity);
            player.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName + "(�}�X�^�[)");
            player.GetPhotonView().RPC("SetRoomName", RpcTarget.AllBuffered, PlayerPrefs.GetString("CreateRoomName"));
        }
        else
        {
            Debug.Log("���Ȃ��̓��[�J���v���C���[�ł�");

            //���g�̃A�o�^-�𐶐�����
            var position = new Vector3(0, 0, 0);
            GameObject player = PhotonNetwork.Instantiate("Player" + Random.Range(1, 5), position, Quaternion.identity);
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = true;
            PhotonNetwork.Instantiate("PlayerFlowCamera", position, Quaternion.identity);
            player.GetPhotonView().RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.NickName + "(���[�J��)");
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