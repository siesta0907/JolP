using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic; // List를 사용하기 위해 필요


// 다마고치 게임을 관리하는 클래스
public class Tamagotchi : MonoBehaviour
{
    // 다마고치의 상태를 정의하는 열거형
    public enum State { EGG, CHILD, TEEN, ADULT, DEAD }
    public State state; // 현재 다마고치의 상태
    public float timer; // 다마고치의 타이머
    public float timeToLive = 259200.0f; // 다마고치의 총 수명(3일)

    public GameObject[] evolutionStages; // 진화 스테이지를 담을 배열

    public float money = 0f; // 돈
    public Button sleepButton; // 잠자기 버튼

    public int dayCounter = 1; // 현재 날짜 카운터
    public Text dayCounterText; // 날짜를 표시할 UI 텍스트 (TextMeshProUGUI로 변경 가능)

    // 각종 스탯을 표시할 UI 텍스트들
    public Text hungerText;
    public Text trainingText;
    public Text playfulnessText;
    public Text cleanlinessText;
    public Text timerText;
    public Text socialText;
    public Text moneyText;

    // HP와 관련된 변수
    public int hp = 4; // HP는 4로 시작
    public Sprite[] heartSprites; // 하트 스프라이트 배열
    public Image heartImage; // 하트 이미지를 보여줄 UI 컴포넌트


    // 캐릭터가 더러워지는 과정 다루는 변수

    public Sprite[] dirtinessSprites; // 더러워지는 스프라이트 배열
    public SpriteRenderer characterSpriteRenderer; // 스프라이트를 렌더링하는 SpriteRenderer
                                                   // 캐릭터 스프라이트를 보여주는 UI 컴포넌트
    private Vector3 brushStartPosition;

    // 스킨 스프라이트 배열
    public Sprite[] skinSprites;
    public GameObject[] skinGameObjects; // 스킨들을 담고 있는 GameObject 배열
    private List<GameObject> activeSkins = new List<GameObject>(); // 현재 활성화된 스킨들을 추적하는 리스트
    public GameObject errorMessagePanel; // 스킨 에러 메시지를 표시할 패널
    public GameObject EGGTalkPanel;


    // 다마고치의 초기화 함수
    void Start()
    {
        EggMonStat.InitializeStat();

        state = State.EGG;
        timer = 0;

        // 진화 스테이지 초기화
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

        UpdateDirtinessLevel(); // 청결도에 따른 dirtinessLevel 초기 계산
        brushStartPosition = brush.transform.position; // 브러시의 초기 위치 저장


    }

    // 날짜 카운터 UI 업데이트 함수
    void UpdateDayCounterUI()
    {
        dayCounterText.text = "DAY\n" + dayCounter;
    }

    // 프레임마다 호출되는 업데이트 함수
    void Update()
    {
        if (state != State.DEAD)
        {
            DecreaseStatsOverTime();
            UpdateUI();
            CheckForEvolution();
            UpdateCharacterSprite(); // 새로 추가된 함수 호출
            if (state == State.CHILD) // CHILD 상태에서만 dirtinessLevel 업데이트
            {
                UpdateDirtinessLevel();
            }

        }
    }

    // UI 업데이트 함수
    void UpdateUI()
    {
        // 각 스탯을 UI에 표시
        /*hungerText.text = "먹이기 : " + (int)Mathf.Clamp(EggMonStat.full, 0, 100);
        trainingText.text = "훈련하기 : " + (int)Mathf.Clamp(EggMonStat.intellect, 0, 100);
        playfulnessText.text = "놀아주기 : " + (int)Mathf.Clamp(EggMonStat.playfulness, 0, 100);
        cleanlinessText.text = "씻기기 : " + (int)Mathf.Clamp(EggMonStat.cleanliness, 0, 100);
        socialText.text = "사회성 : " + (int)Mathf.Clamp(EggMonStat.social, 0, 100);
        moneyText.text = "돈 : " + money;*/

        // 남은 수명을 표시
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
        EggMonStat.health = EggMonStat.maxHealth; // 체력을 최대로 회복
        UpdateHeartSprite(); // HP UI 업데이트
        MoneyManager.money += 50; // 돈 증가
        UpdateDayCounterUI(); // 날짜 카운터 UI 업데이트

        Debug.Log("잠자기 버튼이 눌렸습니다. 다음 날로 넘어갑니다: DAY " + dayCounter);
        Debug.Log("잠을 자서 충분한 휴식을 얻은 다마고치는 체력이 최대가 되었습니다!");
    }

    // 먹이주기 기능을 수행하는 메서드
    public void Feed()
    {
        // 먹이 주기에 대한 로직 추가
    }

    // 훈련하기 기능을 수행하는 메서드
    public void Train()
    {
        
        // 훈련하기에 대한 로직 추가
    }

    // 놀아주기 기능을 수행하는 메서드
    public void Play()
    {
        // 놀아주기에 대한 로직 추가
    }

    // 씻기기 기능을 수행하는 메서드
    public SpriteRenderer brush; // 솔 아이템의 Transform
    public Transform[] brushTargets; // 솔이 이동할 타겟 위치 배열
    private int currentTargetIndex = 0; // 현재 타겟 인덱스
    public int dirtinessLevel = 6; // 예를 들어, 최대 더러움 정도를 6으로 설정

    // 청결도에 따른 dirtinessLevel 계산
    void UpdateDirtinessLevel()
    {
        if (state >= State.CHILD) // CHILD 상태 이상부터 청결도가 적용
        {
            // 청결도가 50이면 dirtinessLevel을 3으로 설정하는 예시
            float cleanlinessPercentage = EggMonStat.cleanliness / 100f; // 0.0 ~ 1.0 사이의 값
            dirtinessLevel = Mathf.FloorToInt(6 - cleanlinessPercentage * 5); // 청결도 100일 때 1, 청결도 0일 때 6
            dirtinessLevel = Mathf.Clamp(dirtinessLevel, 1, 6); // 값이 1~6 사이로 유지되도록 함
        }
    }
    // 씻기기 기능을 수행하는 메서드

    public void Clean()
    {
        
        if (dirtinessLevel > 1) // dirtinessLevel이 1보다 크면 감소시킬 수 있음
        {
            dirtinessLevel--; // 씻길 때마다 dirtinessLevel 감소
            UpdateCharacterSprite(); // 캐릭터 스프라이트 업데이트
            brush.transform.position = brushStartPosition; // 브러시를 초기 위치로 되돌림
            StartCoroutine(BrushMovement()); // 씻기기 애니메이션 시작
            EggMonStat.IncreaseStat("cleanliness", 80);
        }
    }


    // 세 지점을 순서대로 찍는 코루틴

    IEnumerator BrushMovement()
    {
        Vector3 startPosition = brush.transform.position; // 브러시의 초기 위치 저장
        brush.gameObject.SetActive(true); // 브러시 활성화

        for (int i = 0; i < brushTargets.Length; i++)
        {
            // 각 타겟 위치로 이동
            while (brush.transform.position != brushTargets[i].position)
            {
                brush.transform.position = Vector3.MoveTowards(brush.transform.position, brushTargets[i].position, Time.deltaTime * 300f);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f); // 타겟에 도달하면 잠시 대기
        }

        /*// 모든 타겟을 방문한 후 초기 위치로 돌아가기
        while (brush.transform.position != startPosition)
        {
            
            brush.transform.position = Vector3.MoveTowards(brush.transform.position, startPosition, Time.deltaTime * 300f);
            yield return null;
        }*/

        brush.gameObject.SetActive(false); // 작업 완료 후 브러시 비활성화

    }




    // 친구 만나기 기능을 수행하는 메서드
    public void MeetFriends()
    {
        EggMonStat.IncreaseStat("social", 10f);
        EggMonStat.DecreaseStat("cleanliness", 1f);
        // 친구 만나기에 대한 로직 추가
    }

    // 일하기 기능을 수행하는 메서드
    public void Work()
    {
        //money += 50f; // 일정 금액을 추가
       
    }

    // 스탯 감소 메서드
    void DecreaseStatsOverTime()
    {
        if (state != State.EGG)
        {
            EggMonStat.DecreaseStat("full", 10f);
            EggMonStat.DecreaseStat("cleanliness", 10f); // 청결도 감소
            EggMonStat.DecreaseStat("playfulness", 10f);

            // HP 감소 조건 추가
            if (EggMonStat.full <= 0 || EggMonStat.cleanliness <= 0)
            {
                DecreaseHP(); // HP 감소 함수 호출
            }

            // 청결도 감소 후 dirtinessLevel 업데이트
            UpdateDirtinessLevel();
        }

        // 시간에 따른 청결도 감소 로직이 정확히 동작하는지 확인
        if (state != State.EGG && state == State.CHILD)
        {
            // 청결도 감소 로직
            EggMonStat.DecreaseStat("cleanliness", Time.deltaTime * 10);

            // dirtinessLevel 업데이트
            UpdateDirtinessLevel();
        }
    }


    // HP UI 업데이트 메서드
    void UpdateHeartSprite()
    {
        if (hp >= 0 && hp < heartSprites.Length)
        {
            heartImage.sprite = heartSprites[hp]; // 적절한 스프라이트로 업데이트
        }
    }

    // HP 감소 메서드
    public void DecreaseHP()
    {
        if (hp > 0) // HP가 0보다 클 때만 감소
        {
            hp -= 1; // HP를 1 감소
            UpdateHeartSprite(); // 스프라이트 업데이트
        }
    }

    // 진화 메서드
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
                ResetSkins(); // 진화 시 모든 스킨 비활성화


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

    // 진화 스테이지 업데이트 메서드
    void UpdateEvolutionStage(int index)
    {
        foreach (GameObject stage in evolutionStages)
        {
            stage.SetActive(false);
        }
        evolutionStages[index].SetActive(true);
    }

    // 진화 조건을 확인하는 메서드
    void CheckForEvolution()
    {
        // 각 상태별 진화에 필요한 최소 일수 + 1일
        int daysForEvolution = 3;

        // EGG에서 CHILD로 진화하는 경우
        if (state == State.EGG && dayCounter > daysForEvolution)
        {
            Evolve();
            Debug.Log("EGG에서 CHILD로 진화하였습니다!");
        }
        // CHILD에서 TEEN으로 진화하는 경우
        else if (state == State.CHILD && dayCounter > daysForEvolution * 2)
        {
            Evolve();
            Debug.Log("CHILD에서 TEEN으로 진화하였습니다!");
        }
        // TEEN에서 ADULT로 진화하는 경우
        else if (state == State.TEEN && dayCounter > daysForEvolution * 3)
        {
            Evolve();
            Debug.Log("TEEN에서 ADULT로 진화하였습니다!");
        }
        // 추가 상태에 대한 진화 조건
        // ...
    }

    // 캐릭터 스프라이트 업데이트 함수 - 서정 추가
    void UpdateCharacterSprite()
    {
        // 현재 상태가 CHILD일 때만 더러워지는 스프라이트 적용
        if (state == State.CHILD)
        {
            //float cleanlinessPercentage = Mathf.Clamp(EggMonStat.cleanliness, 0, 100) / 100; // 0.0 ~ 1.0 사이의 값
            int spriteIndex = Mathf.Clamp(6 - dirtinessLevel, 0, dirtinessSprites.Length - 1);
            characterSpriteRenderer.sprite = dirtinessSprites[spriteIndex];
        }
        else if (state == State.EGG)
        {
            // EGG 상태일 때의 스프라이트 설정, 필요하다면
            // 예: characterImage.sprite = eggSprite;
        }
        // 기타 상태에 대한 스프라이트 설정도 여기에 추가할 수 있습니다.
    }

    // 스킨 적용 함수
    public void ApplySkin(int skinIndex)
    {
        if (state == State.CHILD && skinIndex >= 0 && skinIndex < skinGameObjects.Length)
        {
            // 스킨 적용 로직
            // 이전에 활성화된 스킨들 비활성화
            foreach (GameObject activeSkin in activeSkins)
            {
                activeSkin.SetActive(false); // 활성화된 스킨 비활성화
            }
            activeSkins.Clear(); // 리스트 클리어

            // 새 스킨 활성화
            skinGameObjects[skinIndex].SetActive(true);
            activeSkins.Add(skinGameObjects[skinIndex]); // 활성화된 스킨 리스트에 추가

            // 현재 상태의 오브젝트 비활성화 (해당 부분은 필요에 따라 조정)
            evolutionStages[(int)state].SetActive(false);

            // 새 스킨으로 캐릭터 스프라이트 변경
            characterSpriteRenderer.sprite = skinSprites[skinIndex];

            // 에러 메시지 패널 비활성화
            errorMessagePanel.SetActive(false);
        }
        else
        {
            // 모든 상태에 대한 공통 에러 처리
            errorMessagePanel.SetActive(true);
        }
    }
    

    // 에러 패널을 닫는 메서드

    // 진화 시 모든 스킨 비활성화
    void ResetSkins()
    {
        foreach (GameObject activeSkin in activeSkins)
        {
            activeSkin.SetActive(false); // 모든 활성화된 스킨 비활성화
        }
        activeSkins.Clear(); // 활성화된 스킨 리스트 클리어
    }
    

}