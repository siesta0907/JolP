using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 사용자에게 확인 문의를 제공하고 응답을 처리하는 클래스
public class QuestionConfirmController : MonoBehaviour
{
    public static QuestionConfirmController instance; // 클래스의 인스턴스를 정적으로 관리
    public Text QuestionTxt; // 확인 문의 텍스트를 표시하는 UI 텍스트
    public Button YesBtn, NoBtn; // 확인 및 취소 버튼
    public static bool? isYes = null; // 사용자의 응답 여부를 저장하는 변수
    public TaskCompletionSource<bool> buttonClickedTask; // 버튼 클릭 이벤트를 관리하는 TaskCompletionSource

    // 시작 시 호출되는 함수
    public void Start()
    {
        instance = this;
    }

    // 확인 문의 내용을 설정하는 함수
    public void SetQuestion(string text)
    {
        QuestionTxt.text = text;
        YesBtn.onClick.AddListener(confirmYes);
        NoBtn.onClick.AddListener(confirmNo);
    }

    // 확인 버튼 클릭 시 호출되는 함수
    private void confirmYes()
    {
        isYes = true;
        buttonClickedTask.TrySetResult(true);
        FrameManager.instance.CloseQuestionConfirm();
    }

    // 취소 버튼 클릭 시 호출되는 함수
    private void confirmNo()
    {
        isYes = false;
        buttonClickedTask.TrySetResult(true);
        FrameManager.instance.CloseQuestionConfirm();
    }
}
