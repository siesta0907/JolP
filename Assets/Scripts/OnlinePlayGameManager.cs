using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePlayGameManager : MonoBehaviour
{
    public Text playerName;
    public Image icon;
    public InputField inputField;

    void Update()
    {
        playerName.text = online_play.playerName;
        icon.sprite = Resources.Load<Sprite>(online_play.playerIcon);
    }
    public void RenameNickName()
    {
        string inputText = inputField.text; // 입력된 텍스트 가져오기
        Debug.Log("입력된 텍스트: " + inputText); // 텍스트 디버그로 출력
        online_play.playerName = inputText;
    }

    public void GoBtnClicked()
    {
        
    }
}
