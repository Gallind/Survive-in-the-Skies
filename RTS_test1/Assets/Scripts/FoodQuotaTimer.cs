using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FoodQuotaTimer : MonoBehaviour
{
    public static FoodQuotaTimer Instance { get; private set; }

    [Header("Timer Settings")]
    public float startTimeInSeconds = 300f; // 5 minutes
    private float timeRemaining;
    private bool timerRunning = true;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;  // Use TextMeshProUGUI here
    public TextMeshProUGUI foodText;

    [Header("Quota Settings")]
    public int foodQuota = 5;
    public int currentFood = 0;

    [Header("Game Over Settings")]
    public string gameOverSceneName = "GameOver";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        timeRemaining = startTimeInSeconds;
        UpdateTimerDisplay();
        UpdateFoodDisplay();
    }

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            CheckQuota();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"Time to quota: {minutes:00}:{seconds:00}";
    }

    void CheckQuota()
    {
        if (currentFood < foodQuota)
        {
            Debug.Log("Food quota not met. Game Over.");
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            Debug.Log("Food quota met. Proceed to next stage or success logic.");
        }
    }

    public void AddFood(int amount)
    {
        currentFood += amount;
        UpdateFoodDisplay();
    }

    public bool RemoveFood(int amount)
    {
        if (currentFood >= amount)
        {
            currentFood -= amount;
            UpdateFoodDisplay();
            return true;
        }
        return false;
    }

    void UpdateFoodDisplay()
    {
        foodText.text = "Current food: " + currentFood + " / " + foodQuota;
    }
}
