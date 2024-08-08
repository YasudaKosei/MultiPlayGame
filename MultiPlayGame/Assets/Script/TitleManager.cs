using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public void CreateRoom()
    {
        PlayerPrefs.SetString("RoomFlag", "Create");
        SceneManager.LoadScene("RoomScene");
    }

    public void JoinRoom()
    {
        PlayerPrefs.SetString("RoomFlag", "Join");
    }

    public void JoinRoomName(Text roomText)
    {
        if (roomText.text == null) return;
        PlayerPrefs.SetString("JoinRoomName", roomText.text);
        SceneManager.LoadScene("RoomScene");
    }
}
