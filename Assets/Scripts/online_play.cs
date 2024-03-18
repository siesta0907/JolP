using UnityEngine;
using UnityEngine.UI;

public static class online_play 
{
    public static string playerIcon;
    public static string playerName;

    // 데이터를 저장하는 함수
    public static void SavePlayerData()
    {
        if(string.IsNullOrEmpty(playerIcon) || string.IsNullOrEmpty(playerName))
        {
            Debug.Log("플레이어 아이콘 또는 플레이어 이름이 비어있습니다.");
        }
        else
        {
            PlayerPrefs.SetString("PlayerIcon", playerIcon);
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();
            Debug.Log("데이터가 저장되었습니다.");
        }
    }

    // 데이터를 불러오는 함수
    public static void LoadPlayerData()
    {
        playerIcon = PlayerPrefs.GetString("PlayerIcon", "egg");
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
        Debug.Log($"{playerName}의 프로필 사진의 이름은 {playerIcon}입니다..");
    }
}
