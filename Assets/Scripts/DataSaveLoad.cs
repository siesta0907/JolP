using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveLoad : MonoBehaviour
{
    public string playerIcon;
    public string playerName;

    // 데이터를 저장하는 함수
    public void SavePlayerData()
    {
        PlayerPrefs.SetString("PlayerIcon", playerIcon);
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        Debug.Log("데이터가 저장되었습니다.");
    }

    // 데이터를 불러오는 함수
    public void LoadPlayerData()
    {
        playerIcon = PlayerPrefs.GetString("PlayerIcon");
        playerName = PlayerPrefs.GetString("PlayerName");
        Debug.Log($"{playerName}의 아이콘은 {playerIcon}입니다.");
    }
}
