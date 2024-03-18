using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Text.RegularExpressions; 


public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Text connectionStatus;
    public Text idText;
    public Button GoBtn;

    private void Start()
    {
        GoBtn.interactable = false;
    }
    public void LoginBtnClick()
    {
        PhotonNetwork.ConnectUsingSettings();
        connectionStatus.text = "마스터 서버 연결 중..";
    }

    public void Connect()
    {
        
            PhotonNetwork.LocalPlayer.NickName = idText.text;
            GoBtn.interactable = false;

            if (PhotonNetwork.IsConnected)
            {
                connectionStatus.text = "방 입장 중..";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                connectionStatus.text = "(오프라인) 연결 실패\n재시도 중..";
                PhotonNetwork.ConnectUsingSettings();
            }
        
    }

    public override void OnConnectedToMaster()
    {
        GoBtn.interactable = true;
        connectionStatus.text = "(온라인) 마스터 서버에 연결됨";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        GoBtn.interactable = false;
        connectionStatus.text = "(오프라인) 연결 실패\n재시도 중..";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionStatus.text = "새 방 생성 중..";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        // MaxPlayers를 0으로 하면 제한없음
    }

    public override void OnJoinedRoom()
    {
        connectionStatus.text = "참가 성공";
        PhotonNetwork.LoadLevel("MiniGame");
    }
}