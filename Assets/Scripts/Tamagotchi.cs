using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;
using Unity.VisualScripting;

public class Tamagotchi : MonoBehaviour
{
    public enum State { EGG, CHILD, TEEN, ADULT, DEAD }
    public State state;
    public float timer;
    public float timeToLive = 259200.0f;
    public GameObject[] evolutionStages;

    public float money = 0f; // 돈
    public Button sleepButton; // 잠자기 버튼

    public int dayCounter = 1; // 현재 날짜 카운터
    public Text dayCounterText; // 날짜를 표시할 UI 텍스트 (TextMeshProUGUI로 변경 가능)


    public Text hungerText;
    public Text trainingText;
    public Text playfulnessText;
    public Text cleanlinessText;
    public Text timerText;
    public Text socialText; // 사회성을 표시할 UI 텍스트
    public Text moneyText; // 돈을 표시할 UI 텍스트

    // hp 관리 부분 변수
    public int hp = 4; // HP는 4로 시작
    public Sprite[] heartSprites; // 하트 스프라이트 배열
    public Image heartImage; // 하트 이미지를 보여줄 UI 컴포넌트



    void Start()
    {
        EggMonStat.InitializeStat();

        state = State.EGG;
        timer = 0;
        if (evolutionStages != null)
        {
            foreach (GameObject stage in evolutionStages)
            {
                stage.SetActive(false);
            }
            evolutionStages[0].SetActive(true);
        }
        // 초기 날짜 표시
        UpdateDayCounterUI();
        // 잠자기 버튼 클릭 이벤트에 Sleep 메서드 연결
        sleepButton.onClick.AddListener(Sleep);

    }

    // 날짜 카운터 UI 업데이트
    void UpdateDayCounterUI()
    {
        dayCounterText.text = "DAY\n" + dayCounter;
    }

    

    void Update()
    {
        if (state != State.DEAD)
        {
            DecreaseStatsOverTime();
            UpdateUI();

            // 진화 조건 확인 및 처리
            CheckForEvolution();
        }
    }

    void UpdateUI()
    {
        hungerText.text = "먹이기 : " + (int)Mathf.Clamp(EggMonStat.full, 0, 100);
        trainingText.text = "훈련하기 : " + (int)Mathf.Clamp(EggMonStat.intellect, 0, 100);
        playfulnessText.text = "놀아주기 : " + (int)Mathf.Clamp(EggMonStat.playfulness, 0, 100);
        cleanlinessText.text = "씻기기 : " + (int)Mathf.Clamp(EggMonStat.cleanliness, 0, 100);
        socialText.text = "사회성 : " + (int)Mathf.Clamp(EggMonStat.social, 0, 100);
        moneyText.text = "돈 : " + money;


        float remainingTime = timeToLive - timer;
        int remainingHours = Mathf.FloorToInt(remainingTime / 3600);
        int remainingMinutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        int remainingSeconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:0}h {1:0}m {2:0}s", remainingHours, remainingMinutes, remainingSeconds);
    }

    // 잠자기 기능을 수행하는 메서드
    public void Sleep()
    {
        // 날짜 카운터 증가
        dayCounter++;

        // 체력을 최대로 회복
        hp = heartSprites.Length - 1;
        UpdateHeartSprite();

        // 날짜 카운터 UI 업데이트
        UpdateDayCounterUI();

        Debug.Log("잠자기 버튼이 눌렸습니다. 다음 날로 넘어갑니다: DAY " + dayCounter);
    }
    public void Feed()
    {

    }

    public void Train()
    {
        EggMonStat.DecreaseStat("health", 30);


    }

    public void Play()
    {
    }

    public void Clean()
    {
        EggMonStat.IncreaseStat("cleanliness", 10f);
        EggMonStat.IncreaseStat("likeability", 1f);
    }

    public void MeetFriends()
    {
        EggMonStat.IncreaseStat("social", 10f);
        EggMonStat.DecreaseStat("cleanliness", 1f);
    }
    public void Work()
    {
        money += 50f; // 일정 금액을 추가
        EggMonStat.DecreaseStat("full", 40f);
        if (EggMonStat.full <= 0)
        {
            DecreaseHP(); // 배고픔이 0 이하이면 HP 감소
        }
        Debug.Log("돈 : " + money);
    }


    void DecreaseStatsOverTime()
    {
        if (state != State.EGG)
        {
            EggMonStat.DecreaseStat("full", 10f);
            EggMonStat.DecreaseStat("cleanliness", 10f);
            EggMonStat.DecreaseStat("playfulness", 10f);

            // 새로운 HP 감소 조건
            if (EggMonStat.full <= 0 || EggMonStat.cleanliness <= 0)
            {
                DecreaseHP(); // HP 감소 함수 호출
            }
        }


    }

    void UpdateHeartSprite()
    {
        if (hp >= 0 && hp < heartSprites.Length)
        {
            heartImage.sprite = heartSprites[hp]; // 적절한 스프라이트로 업데이트
        }
    }

    public void DecreaseHP()
    {
        if (hp > 0) // HP가 0보다 클 때만 감소
        {
            hp -= 1; // HP를 1 감소
            UpdateHeartSprite(); // 스프라이트 업데이트
        }
    }

    void Evolve()
    {
        switch (state)
        {
            case State.EGG:
                state = State.CHILD;
                UpdateEvolutionStage(1);
                timer = 0;
                break;
            case State.CHILD:
                state = State.TEEN;
                UpdateEvolutionStage(2);
                timer = 0;
                break;
            case State.TEEN:
                state = State.ADULT;
                UpdateEvolutionStage(3);
                timer = 0;
                break;
            case State.ADULT:
                state = State.DEAD;
                Debug.Log("다마고치가 죽었습니다!!");
                break;
        }
    }

    void UpdateEvolutionStage(int index)
    {
        foreach (GameObject stage in evolutionStages)
        {
            stage.SetActive(false);
        }
        evolutionStages[index].SetActive(true);
    }

    void CheckForEvolution()
    {
        // 각 상태별 진화에 필요한 최소 일수 + 1일
        int daysForEvolution = 3;

        // 현재 상태가 EGG이고, dayCounter가 진화에 필요한 최소 일수 + 1일을 초과했는지 확인
        if (state == State.EGG && dayCounter > daysForEvolution)
        {
            Evolve();
        }
        // CHILD에서 TEEN으로 진화하는 경우
        else if (state == State.CHILD && dayCounter > daysForEvolution * 2) // 예를 들어 7일째에 CHILD에서 TEEN으로 진화
        {
            Evolve();
        }
        // TEEN에서 ADULT로 진화하는 경우
        else if (state == State.TEEN && dayCounter > daysForEvolution * 3) // 예를 들어 10일째에 TEEN에서 ADULT로 진화
        {
            Evolve();
        }
        // 추가 상태에 대한 진화 조건
        // ...
    }

}
