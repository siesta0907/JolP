using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic; // List를 사용하기 위해 필요
//using UnityEditor.Animations;
using UnityEngine.SceneManagement;


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
    public Canvas leave_endingCanvas; // 엔딩 화면 참조
    public Canvas rich_endingCanvas;
    public Canvas pirate_endingCanvas;
    public Canvas ScreenHiding;

    // 캐릭터가 더러워지는 과정 다루는 변수

    public Sprite[] dirtinessSprites; // 더러워지는 스프라이트 배열
   // public SpriteRenderer characterSpriteRenderer; // 스프라이트를 렌더링하는 SpriteRenderer
                                                   // 캐릭터 스프라이트를 보여주는 UI 컴포넌트
    private Vector3 brushStartPosition;

    public GameObject errorMessagePanel; // 스킨 에러 메시지를 표시할 패널

    public GameObject EGGTalkPanel;

    public GameObject TrainErrorPanel; // 스킨 에러 메시지를 표시할 패널

    public GameObject PlayErrorPanel; // 스킨 에러 메시지를 표시할 패널
    public inventoryItemClick inventoryItemClick;

    public Image EggMon;
    public Animator animator;
    public RuntimeAnimatorController[] animatorController;
    public Sprite[] EvolveSprite;

    private bool isEvolve = false;

    public static Tamagotchi instance; // 싱글턴 인스턴스
    public Canvas TrainCanvas;
    public Canvas WorkCanvas;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (state != State.DEAD && state != State.ENDING)
        {
            CheckForEvolution();

            if (state == State.CHILD)
            {
                UpdateDirtinessLevel();
            }

            // 체력 검사
            if (EggMonStat.health <= 0)
            {
                TriggerEndingDueToHealth(); // 가출 엔딩
                return; // 체력이 0 이하면 나머지 업데이트 로직을 수행하지 않음
            }

            // 코인이 3000 이상일 때 특별 엔딩 조건 체크
            if (MoneyManager.money >= 3000)
            {
                TriggerSpecialEnding("Coin");
                return;
            }

            // 지능이 5000 이상일 때 또 다른 특별 엔딩 조건 체크
            if (EggMonStat.intellect >= 100)
            {
                TriggerSpecialEnding("Intelligence");
                return;
            }


            if (dayCounter > 10)
            {
                TriggerEnding();
            }
        }
    }

    public void TriggerEndingDueToHealth()
    {
        state = State.DEAD;

        // 모든 캔버스를 찾아서 메인 캔버스와 엔딩 캔버스를 제외하고 비활성화
        
        TrainCanvas.gameObject.SetActive(false);
        WorkCanvas.gameObject.SetActive(false);
        ScreenHiding.gameObject.SetActive(false);
        // 엔딩 캔버스를 활성화
        leave_endingCanvas.gameObject.SetActive(true);
        leave_endingCanvas.transform.SetAsLastSibling(); // 엔딩 캔버스를 최상위로 설정
        Debug.Log("다마고치가 체력 부족으로 사망했습니다. 게임 오버!");
    }



    // 잠자기 기능을 수행하는 메서드
    public void Sleep()
    {
        // 날짜 카운터 증가
        dayCounter++;
        EggMonStat.health = EggMonStat.maxHealth; // 체력을 최대로 회복
        EggMonStat.DecreaseStat("cleanliness", 10); //청결도 30 감소
        EggMonStat.DecreaseStat("full", 10); //
        EggMonStat.DecreaseStat("playfulness", 10); //

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

    public void Work()
    {
        // 먹이 주기에 대한 로직 추가
    }

    // 훈련하기 기능을 수행하는 메서드
    public void Train()
    {

        if (state == State.CHILD)
        {
            // CHILD 상태일 때 훈련 로직 수행
            // 예: 훈련 관련 애니메이션 표시, 스탯 증가 등
            Debug.Log("훈련을 시작합니다.");
            // 여기에 훈련 로직을 추가하세요.
        }
        else
        {
            // CHILD 상태가 아니면 오류 메시지 패널을 활성화
            TrainErrorPanel.SetActive(true);
            Debug.Log("CHILD 상태에서만 훈련할 수 있습니다.");
            // errorMessagePanel에 있는 텍스트 컴포넌트를 찾아서 오류 메시지를 업데이트할 수도 있습니다.
            // 예: errorMessagePanel.GetComponentInChildren<Text>().text = "CHILD 상태에서만 훈련할 수 있습니다.";
        }
    }

    // 놀아주기 기능을 수행하는 메서드
    public void Play()
    {

        if (state == State.EGG)
        {
            PlayErrorPanel.SetActive(true);
            Debug.Log("EGG 상태에서는 장난감을 사용할 수 없습니다.");
        }

        if (state == State.CHILD)
        {
            // CHILD 상태일 때 훈련 로직 수행
            // 예: 훈련 관련 애니메이션 표시, 스탯 증가 등
            Debug.Log("놀이를 시작합니다.");
            // 여기에 훈련 로직을 추가하세요.
        }
        else
        {
            // CHILD 상태가 아니면 오류 메시지 패널을 활성화
            PlayErrorPanel.SetActive(true);
            Debug.Log("CHILD 상태에서만 훈련할 수 있습니다.");
            // errorMessagePanel에 있는 텍스트 컴포넌트를 찾아서 오류 메시지를 업데이트할 수도 있습니다.
            // 예: errorMessagePanel.GetComponentInChildren<Text>().text = "CHILD 상태에서만 훈련할 수 있습니다.";
        }
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
                online_play.playerIcon = "ch_n1_01";
                online_play.SavePlayerData(online_play.playerIcon);
                UpdateEvolutionStage(1);
                animator.runtimeAnimatorController = animatorController[1];
                break;

            case State.CHILD:
                state = State.TEEN;
                Debug.Log("CHILD에서 TEEN으로 진화하였습니다!");
                UpdateEvolutionStage(2);
                online_play.playerIcon = "ch_n1_02";
                online_play.SavePlayerData(online_play.playerIcon);
                animator.runtimeAnimatorController = animatorController[2];
                break;

            case State.TEEN:
                state = State.ADULT;
                Debug.Log("TEEN에서 ADULT로 진화하였습니다!");
                UpdateEvolutionStage(3);
                online_play.playerIcon = "ch_n1_03";
                online_play.SavePlayerData(online_play.playerIcon);
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
    public void RestartGame()
    {
        ResetGameState();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // 게임 상태를 초기화하는 메서드
    private void ResetGameState()
    {
        // 여기에 모든 게임 상태를 초기화하는 코드를 추가
        // 예: 체력, 스탯, UI 업데이트, 에러 패널 비활성화 등

        // 게임 로직 상태 초기화
        state = State.EGG;
        dayCounter = 1;
        EggMonStat.InitializeStat(); // 이 부분은 EggMonStat 클래스 내에 적절한 초기화 메서드가 정의되어 있어야 합니다.

        // UI 업데이트
        UpdateDayCounterUI();
        UpdateDirtinessLevel();

        // 스탯 텍스트 UI 초기화
        hungerText.text = "100";
        trainingText.text = "100";
        playfulnessText.text = "100";
        cleanlinessText.text = "100";
        socialText.text = "100";
        moneyText.text = "100";

        // 기타 UI 컴포넌트 초기화
        errorMessagePanel.SetActive(false);
        EGGTalkPanel.SetActive(false);
        TrainErrorPanel.SetActive(false);
        PlayErrorPanel.SetActive(false);

        // 캐릭터 스프라이트와 애니메이션 초기화
        EggMon.sprite = EvolveSprite[0];
        animator.runtimeAnimatorController = animatorController[0];

        // 게임 속도 정상화
        Time.timeScale = 1;
    }
    public void TriggerEnding()
    {
        state = State.ENDING;
        leave_endingCanvas.transform.SetAsLastSibling(); // Ensure the ending panel is the topmost UI element
        Time.timeScale = 0;
        leave_endingCanvas.gameObject.SetActive(true);
        Debug.Log("게임 엔딩 도달 가출!");
    }

    // 특별 엔딩을 처리하는 메서드
    public void TriggerSpecialEnding(string endingType)
    {
        switch (endingType)
        {
            case "Coin":
                // 코인에 의한 엔딩 처리
                state = State.ENDING;
                TrainCanvas.gameObject.SetActive(false);
                WorkCanvas.gameObject.SetActive(false);
                ScreenHiding.gameObject.SetActive(false);
                rich_endingCanvas.transform.SetAsLastSibling(); // Ensure the ending panel is the topmost UI element
                Time.timeScale = 0;
                rich_endingCanvas.gameObject.SetActive(true);
                Debug.Log("코인이 3000 이상입니다. 부자 엔딩!");
                break;
            case "Intelligence":
                // 지능에 의한 엔딩 처리
                state = State.ENDING;
                TrainCanvas.gameObject.SetActive(false);
                WorkCanvas.gameObject.SetActive(false);
                ScreenHiding.gameObject.SetActive(false);
                pirate_endingCanvas.transform.SetAsLastSibling(); // Ensure the ending panel is the topmost UI element
                Time.timeScale = 0;
                pirate_endingCanvas.gameObject.SetActive(true);
                Debug.Log("지능이 5000 이상입니다. 지능 엔딩!");
              
                break;
            /*default:
                // 기본 엔딩
                TriggerEnding();
                break;*/
        }

       /* // 엔딩 상태로 변경
        state = State.ENDING;
        // 엔딩 캔버스를 활성화하고 최상위로 설정
        leave_endingCanvas.transform.SetAsLastSibling();
        leave_endingCanvas.gameObject.SetActive(true);*/
        Time.timeScale = 0; // 게임 시간 정지
    }

}