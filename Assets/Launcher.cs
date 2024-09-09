using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField Username;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject button;
    public static Launcher Instance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }
    void Awake()
    {
        Instance = this;
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("title");
        



    }
    public void confirmuser()
    {
        PhotonNetwork.NickName = Username.text;
    }
    // Update is called once per frame
    void Update()
    {
        //PhotonNetwork.NickName = Username.text;
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        //PhotonNetwork.LoadLevel(1);
        Debug.Log("created room");
    }
    public override void OnJoinedRoom()

    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.Instance.OpenMenu("GameLobby");
        Debug.Log("joined the room");
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log(players.LongLength);
        for (int i = 0; i < players.LongLength; i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        button.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        button.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //errorText.text = "Room Creation Failed: " + message;
        Debug.LogError("Room Creation Failed: " + message);
       // MenuManager.Instance.OpenMenu("error");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    public void joingame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
