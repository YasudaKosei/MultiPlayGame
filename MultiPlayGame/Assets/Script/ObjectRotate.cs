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
            // ��}�X�^�[�N���C�A���g�ł���ꍇ�A��]�����}�X�^�[�N���C�A���g�Ƀ��N�G�X�g
            photonView.RPC("RequestRotation", RpcTarget.MasterClient);
        }
    }

    void Update()
    {
        // �I�u�W�F�N�g�����̑��x�ŉ�]������
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
    }

    [PunRPC]
    void RequestRotation()
    {
        // �}�X�^�[�N���C�A���g�����̃��\�b�h���󂯎�����ۂɁA���݂̉�]�p�x�𑼂̃N���C�A���g�ɑ��M����
        photonView.RPC("SetRotation", RpcTarget.Others, transform.rotation.eulerAngles.x);
    }

    [PunRPC]
    void SetRotation(float xRotation)
    {
        // �󂯎������]�p�x�����ƂɁA�I�u�W�F�N�g�̉�]��ݒ�
        transform.rotation = Quaternion.Euler(xRotation, 0, 0);
    }
}


