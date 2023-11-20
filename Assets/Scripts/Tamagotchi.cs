using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class Tamagotchi : MonoBehaviour
{
    public enum State { EGG, CHILD, TEEN, ADULT, DEAD }
    public State state;
    public float timer;
    public float timeToLive = 259200.0f; // 72�ð� * 60�� * 60��
    public GameObject[] evolutionStages; // ��ȭ ���º� ���� ������Ʈ �迭

    public float hunger = 100f; // float�� ����
    public float training = 100f; // float�� ����
    public float playfulness = 100f; // float�� ����
    public float cleanliness = 100f; // float�� ����
    public int careScore = 0; // �ɾ� ����
    public float social = 100f; // 사회성
    public float money = 0f; // 돈

    

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
        if(evolutionStages != null)
        {
            foreach (GameObject stage in evolutionStages)
            {
                stage.SetActive(false); // ��� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
            }
            evolutionStages[0].SetActive(true); // �ʱ� ���� (��) Ȱ��ȭ
        }
     
    }

    void Update()
    {
        if (state != State.DEAD)
        {
            timer += Time.deltaTime * 2; // ���⼭ 2�� �ð��� ������ �帣�� �ϴ� ���, ��� ���� ����

            if (timer >= timeToLive)
            {
                Evolve();
            }

            DecreaseStatsOverTime();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        hungerText.text = "먹이기 : " + (int)Mathf.Clamp(hunger, 0, 100);
        trainingText.text = "훈련하기 : " + (int)Mathf.Clamp(training, 0, 100);
        playfulnessText.text = "놀아주기 : " + (int)Mathf.Clamp(playfulness, 0, 100);
        cleanlinessText.text = "씻기기 : " + (int)Mathf.Clamp(cleanliness, 0, 100);
        socialText.text = "사회성 : " + (int)Mathf.Clamp(social, 0, 100);
        moneyText.text = "돈 : " + money;


        float remainingTime = timeToLive - timer;
        int remainingHours = Mathf.FloorToInt(remainingTime / 3600);
        int remainingMinutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        int remainingSeconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:0}h {1:0}m {2:0}s", remainingHours, remainingMinutes, remainingSeconds);
    }

    public void Feed()
    {
        hunger += 10f; // float ������ �߰�
        careScore += 1;
        Debug.Log("배고픔 : " + hunger);
    }

    public void Train()
    {
        training += 10f;
        careScore += 1;
        Debug.Log("훈련도 : " + training);
    }

    public void Play()
    {
        playfulness += 10f;
        careScore += 1;
        Debug.Log("즐거움 : " + playfulness);
    }

    public void Clean()
    {
        cleanliness += 10f;
        careScore += 1;
        Debug.Log("청결도 : " + cleanliness);
    }

    public void MeetFriends()
    {
        social += 10f; // 사회성을 증가
        cleanliness -= 10f; // 친구를 만나면 청결도가 감소
        Debug.Log("사회성 : " + social);
    }
    public void Work()
    {
        money += 50f; // 일정 금액을 추가
        hunger -= 10f; // 알바를 하면 배고픔이 증가
        if (hunger <= 0)
        {
            DecreaseHP(); // 배고픔이 0 이하이면 HP 감소
        }
        Debug.Log("돈 : " + money);
    }


    void DecreaseStatsOverTime()
    {
        if (state != State.EGG)
        {
            hunger -= Time.deltaTime;
            training -= Time.deltaTime;
            playfulness -= Time.deltaTime;
            cleanliness -= Time.deltaTime;

            // 새로운 HP 감소 조건
            if (hunger <= 0 || cleanliness <= 0)
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
}
