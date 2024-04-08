using UnityEngine;
using UnityEngine.UI;

public static class online_play 
{
    public static string playerIcon;
    public static string playerName;

    // 데이터를 저장하는 함수
    public static void SavePlayerData(string icon = "egg", string name = "player")
    {
            PlayerPrefs.SetString("PlayerIcon", icon);
            PlayerPrefs.SetString("PlayerName", name);
            PlayerPrefs.Save();
            Debug.Log("데이터가 저장되었습니다.");
        
    }

    // 데이터를 불러오는 함수
    public static void LoadPlayerData()
    {
        playerIcon = PlayerPrefs.GetString("PlayerIcon", "egg");
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
        Debug.Log($"{playerName}의 프로필 사진의 이름은 {playerIcon}입니다..");
    }
}
