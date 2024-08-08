using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerCallback : MonoBehaviourPunCallbacks
{
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName}???Q??????????");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName}?????o????????");
    }

}
