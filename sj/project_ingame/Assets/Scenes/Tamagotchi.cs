using UnityEngine;
using UnityEngine.UI;

public class Tamagotchi : MonoBehaviour
{
    public enum State { ALIVE, DEAD }
    public State state;
    public float timer;
    public float timeToLive = 259200.0f; // 72시간 * 60분 * 60초

    public int hunger = 100;
    public int training = 100;
    public int playfulness = 100;
    public int cleanliness = 100;

    public Text hungerText;
    public Text trainingText;
    public Text playfulnessText;
    public Text cleanlinessText;
    public Text timerText;

    void Start()
    {
        state = State.ALIVE;
        timer = 0;
    }

    void Update()
    {
        if (state == State.ALIVE)
        {
            timer += Time.deltaTime * 2;  // 여기서 2는 시간을 빠르게 흐르게 하는 상수, 상시 변경 가능

            if (timer >= timeToLive)
            {
                state = State.DEAD;
                Debug.Log("다마고치를 잘 돌보지 못하여 다마고치가 죽었습니다.. 다음 번엔 더 잘 키워주세요 !!");
            }

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        hungerText.text = "포만감 : " + hunger;
        trainingText.text = "훈련도 : " + training;
        playfulnessText.text = "신남도 : " + playfulness;
        cleanlinessText.text = "청결도 : " + cleanliness;

        float remainingTime = timeToLive - timer;
        int remainingHours = Mathf.FloorToInt(remainingTime / 3600);
        int remainingMinutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        int remainingSeconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:0}h {1:0}m {2:0}s", remainingHours, remainingMinutes, remainingSeconds);
    }

    public void Feed()
    {
        hunger += 10;
        Debug.Log("다마고치 밥 먹이기 ! 포만감은 현재 : " + hunger);
    }

    public void Train()
    {
        training += 10;
        Debug.Log("다마고치 훈련하기 ! 훈련도는 현재 : " + training);
    }

    public void Play()
    {
        playfulness += 10;
        Debug.Log("다마고치 놀아주기 ! 신남도는 현재 : " + playfulness);
    }

    public void Clean()
    {
        cleanliness += 10;
        Debug.Log("다마고치 씻기기 ! 청결도는 현재 : " + cleanliness);
    }
}
