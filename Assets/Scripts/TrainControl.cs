using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 필요

public class TrainControl : MonoBehaviour
{
    public GameObject panelToActivate; // 활성화할 패널의 참조를 저장

    // Start 메서드는 스크립트 인스턴스가 로딩될 때 호출됩니다.
    void Start()
    {
        if (panelToActivate != null)
            panelToActivate.SetActive(false); // 게임 시작 시 패널을 비활성화
    }

    // 버튼 클릭 시 호출될 메서드
    public void ActivatePanel()
    {
        if (panelToActivate != null)
            panelToActivate.SetActive(true); // 패널을 활성화
    }
}

