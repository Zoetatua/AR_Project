using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public TMP_InputField roomInputField; // Reference to RoomInputField
    public Button createRoomButton;
    public Button joinRoomButton;

    void Start()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRoom);
    }

    public void CreateRoom()
    {
        string roomName = roomInputField.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName);
        }
    }

    public void JoinRoom()
    {
        string roomName = roomInputField.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}
