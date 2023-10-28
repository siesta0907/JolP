using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public Text hungerText;
    public Text trainingText;
    public Text playfulnessText;
    public Text cleanlinessText;
    public Text timerText;

    void Start()
    {
        state = State.EGG;
        timer = 0;
        foreach (GameObject stage in evolutionStages)
        {
            stage.SetActive(false); // ��� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        }
        evolutionStages[0].SetActive(true); // �ʱ� ���� (��) Ȱ��ȭ
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
        hungerText.text = "������ : " + (int)Mathf.Clamp(hunger, 0, 100);
        trainingText.text = "�Ʒõ� : " + (int)Mathf.Clamp(training, 0, 100);
        playfulnessText.text = "�ų��� : " + (int)Mathf.Clamp(playfulness, 0, 100);
        cleanlinessText.text = "û�ᵵ : " + (int)Mathf.Clamp(cleanliness, 0, 100);

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
        Debug.Log("�ٸ���ġ �� ���̱� ! �������� ���� : " + hunger);
    }

    public void Train()
    {
        training += 10f;
        careScore += 1;
        Debug.Log("�ٸ���ġ �Ʒ��ϱ� ! �Ʒõ��� ���� : " + training);
    }

    public void Play()
    {
        playfulness += 10f;
        careScore += 1;
        Debug.Log("�ٸ���ġ ����ֱ� ! �ų����� ���� : " + playfulness);
    }

    public void Clean()
    {
        cleanliness += 10f;
        careScore += 1;
        Debug.Log("�ٸ���ġ �ı�� ! û�ᵵ�� ���� : " + cleanliness);
    }

    void DecreaseStatsOverTime()
    {
        if (state != State.EGG)
        {
            hunger -= Time.deltaTime;
            training -= Time.deltaTime;
            playfulness -= Time.deltaTime;
            cleanliness -= Time.deltaTime;
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
                Debug.Log("�ٸ���ġ�� �� ������ ���Ͽ� �ٸ���ġ�� �׾����ϴ�.. ���� ���� �� �� Ű���ּ��� !!");
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
