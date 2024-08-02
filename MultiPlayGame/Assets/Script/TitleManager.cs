using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void CreateRoom()
    {
        PlayerPrefs.SetString("RoomFlag", "Create");
        SceneManager.LoadScene("RoomScene");
    }

    // Update is called once per frame
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
