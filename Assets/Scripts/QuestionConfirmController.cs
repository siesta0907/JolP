using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QuestionConfirmController : MonoBehaviour
{
    public static QuestionConfirmController instance;
    public Text QuestionTxt;
    public Button YesBtn, NoBtn;
    public static bool? isYes = null;
    public TaskCompletionSource<bool> buttonClickedTask;

    public void Start()
    {
        instance = this;
    }
    public void SetQuestion(string text)
    {
        QuestionTxt.text = text;
        YesBtn.onClick.AddListener(confirmYes);
        NoBtn.onClick.AddListener(confirmNo);
    }
    private void confirmYes()
    {
        isYes = true;
        buttonClickedTask.TrySetResult(true);
        FrameManager.instance.CloseQuestionConfirm();
    }
    private void confirmNo()
    {
        isYes = false;
        buttonClickedTask.TrySetResult(true);
        FrameManager.instance.CloseQuestionConfirm();
    }
}
