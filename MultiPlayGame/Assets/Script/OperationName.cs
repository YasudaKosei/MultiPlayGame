using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class OperationName : MonoBehaviourPunCallbacks
{

    private GameObject namePlate;   //�@���O��\�����Ă���v���[�g
    public Text otherNameText;   //�@���O��\������e�L�X�g
    public Text myNameText;

    void Start()
    {
        namePlate = otherNameText.transform.parent.gameObject;
    }

    void LateUpdate()
    {
        //���O����ɃJ�����̌����ɂȂ�
        //�Ȃ�LateUpdate�ōs���Ă��邩�Ƃ����Ɩ��O�̓L�����N�^�[�̎q�v�f�ɂȂ��Ă��ăL�����N�^�[�𓮂����Ɠ��������ɖ��O����]���܂����A
        //LateUpdate�Ŗ��O���J�����̌����ɂ���΃L�����N�^�[�̈ړ����l��������ɖ��O���J�����̌����ɂ��Ă��̌�`�悳���̂ł����ƕ\������܂��B
        namePlate.transform.rotation = Camera.main.transform.rotation;
    }

    [PunRPC]
    void SetName(string name)
    {
        if (!photonView.IsMine) otherNameText.text = name;
        else myNameText.text = name;
    }
}