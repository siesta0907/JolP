using UnityEngine;
using UnityEngine.UI;

public static class online_play 
{
    public static string playerIcon;
    public static string playerName;

    // �����͸� �����ϴ� �Լ�
    public static void SavePlayerData(string icon = "egg", string name = "player")
    {
            PlayerPrefs.SetString("PlayerIcon", icon);
            PlayerPrefs.SetString("PlayerName", name);
            PlayerPrefs.Save();
            Debug.Log("�����Ͱ� ����Ǿ����ϴ�.");
        
    }

    // �����͸� �ҷ����� �Լ�
    public static void LoadPlayerData()
    {
        playerIcon = PlayerPrefs.GetString("PlayerIcon", "egg");
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
        Debug.Log($"{playerName}�� ������ ������ �̸��� {playerIcon}�Դϴ�..");
    }
}
