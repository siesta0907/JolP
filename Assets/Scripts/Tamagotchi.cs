using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic; // List를 사용하기 위해 필요
using UnityEditor.Animations;


// 다마고치 게임을 관리하는 클래스
public class Tamagotchi : MonoBehaviour
{
    // 다마고치의 상태를 정의하는 열거형
    public enum State { EGG, CHILD, TEEN, ADULT, DEAD, ENDING }
    public State state; // 현재 다마고치의 상태


    public Button sleepButton; // 잠자기 버튼

    public int dayCounter = 1; // 현재 날짜 카운터
    public Text dayCounterText; // 날짜를 표시할 UI 텍스트 (TextMeshProUGUI로 변경 가능)

    // 각종 스탯을 표시할 UI 텍스트들
    public Text hungerText;
    public Text trainingText;
    public Text playfulnessText;
    public Text cleanlinessText;
    public Text socialText;
    public Text moneyText;
    public GameObject endingPanel; // 엔딩 패널의 참조

    // 캐릭터가 더러워지는 과정 다루는 변수

    public Sprite[] dirtinessSprites; // 더러워지는 스프라이트 배열
   // public SpriteRenderer characterSpriteRenderer; // 스프라이트를 렌더링하는 SpriteRenderer
                                                   // 캐릭터 스프라이트를 보여주는 UI 컴포넌트
    private Vector3 brushStartPosition;

    public GameObject errorMessagePanel; // 스킨 에러 메시지를 표시할 패널
    public GameObject EGGTalkPanel;

    public Image EggMon;
    public Animator animator;
    public AnimatorController[] animatorController;
    public Sprite[] EvolveSprite;

    private bool isEvolve = false;

    // 다마고치의 초기화 함수
    void Start()
    {
        EggMonStat.InitializeStat();

        state = State.EGG;
        EggMon.sprite = EvolveSprite[0];
        animator.runtimeAnimatorController = animatorController[0];

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
            CheckForEvolution();

            if (state == State.CHILD) // CHILD 상태에서만 dirtinessLevel 업데이트
            {
                UpdateDirtinessLevel();
            }

            // 10일이 지나면 엔딩을 트리거
            if (dayCounter > 10)
            {
                TriggerEnding();
            }

        }
    }

    // 잠자기 기능을 수행하는 메서드
    public void Sleep()
    {
        // 날짜 카운터 증가
        dayCounter++;
        EggMonStat.health = EggMonStat.maxHealth; // 체력을 최대로 회복
        EggMonStat.DecreaseStat("cleanliness", 30); //청결도 30 감소
        EggMonStat.DecreaseStat("full", 30); //
        EggMonStat.DecreaseStat("playfulness", 30); //

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
        EggMonStat.DecreaseStat("cleanliness", 10f);
        // 친구 만나기에 대한 로직 추가
    }


    // 스탯 감소 메서드
    

    // 진화 메서드
    void Evolve()
    {
        if (!isEvolve) return;

        switch (state)
        {
            case State.EGG:
                state = State.CHILD;
                Debug.Log("EGG에서 CHILD로 진화하였습니다!");
                UpdateEvolutionStage(1);
                animator.runtimeAnimatorController = animatorController[1];
                break;

            case State.CHILD:
                state = State.TEEN;
                Debug.Log("CHILD에서 TEEN으로 진화하였습니다!");
                UpdateEvolutionStage(2);
                animator.runtimeAnimatorController = animatorController[2];
                break;

            case State.TEEN:
                state = State.ADULT;
                Debug.Log("TEEN에서 ADULT로 진화하였습니다!");
                UpdateEvolutionStage(3);
                animator.runtimeAnimatorController = animatorController[3];
                break;

            case State.ADULT:
                state = State.DEAD;
                Debug.Log("다마고치가 죽었습니다!!");
                break;
        }
        isEvolve = false;
    }

    // 진화 스테이지 업데이트 메서드
    void UpdateEvolutionStage(int state)
    {
        EggMon.sprite = EvolveSprite[state];
        Debug.Log("바뀜!");
    }

    // 진화 조건을 확인하는 메서드
    void CheckForEvolution()
    {
        // 각 상태별 진화에 필요한 최소 일수 + 1일
        int daysForEvolution = 3;
        
        // EGG에서 CHILD로 진화하는 경우
        if (state == State.EGG && dayCounter > daysForEvolution)
        {
            isEvolve = true;
        }
        // CHILD에서 TEEN으로 진화하는 경우
        else if (state == State.CHILD && dayCounter > daysForEvolution * 2)
        {
            isEvolve = true;
        }
        // TEEN에서 ADULT로 진화하는 경우
        else if (state == State.TEEN && dayCounter > daysForEvolution * 3)
        {
            isEvolve = true;
        }
        // 추가 상태에 대한 진화 조건
        // ...

        Evolve();

    }

    // 캐릭터 스프라이트 업데이트 함수 - 서정 추가
    void UpdateCharacterSprite()
    {
        // 현재 상태가 CHILD일 때만 더러워지는 스프라이트 적용
        if (state == State.CHILD && dirtinessLevel > 1)
        {
            int spriteIndex = Mathf.Clamp(6 - dirtinessLevel, 0, dirtinessSprites.Length - 1);
            //  EggMon.sprite = dirtinessSprites[spriteIndex];
        }
        else if (state == State.EGG)
        {
            // EGG 상태일 때의 스프라이트 설정, 필요하다면
            // 예: characterImage.sprite = eggSprite;
        }
        // 기타 상태에 대한 스프라이트 설정도 여기에 추가할 수 있습니다.
    }

    // 스킨 적용 함수
    public void ApplySkin(string skinName)
    {
        if (state == State.CHILD)
        {
            EggMon.sprite = Resources.Load<Sprite>(skinName);
        }
        else
        {
            // 모든 상태에 대한 공통 에러 처리
            errorMessagePanel.SetActive(true);
        }
    }

    public void TriggerEnding()
    {
        state = State.ENDING; // 게임 상태를 엔딩으로 변경
                              // 게임을 멈추거나 엔딩 관련 UI를 활성화하는 로직 추가
        endingPanel.SetActive(true);
        Debug.Log("게임 엔딩 도달!");

        
    }

}