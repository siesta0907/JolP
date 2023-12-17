using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 게임 내에서 돈을 관리하는 클래스
public class MoneyManager : MonoBehaviour
{
    public static int money; // 정적 변수로 돈을 관리
    public Text coinText; // 돈을 표시하는 UI 텍스트

    // 시작 시 호출되는 함수
    void Start()
    {
        money = 500; // 초기 돈 설정
    }

    // 프레임마다 호출되는 함수
    void Update()
    {
        // 돈을 표시하는 UI 텍스트 업데이트
        coinText.text = money.ToString();
    }
}