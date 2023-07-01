using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int totalAIEnemies;
    private int enemiesKilled;

    public Text enemyCountText; // Reference to the UI text element

    public TriggerObjectScript triggerObjectScript; // Reference to the TriggerObjectScript

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        enemiesKilled = 0;

        if (triggerObjectScript != null)
        {
            triggerObjectScript.SetActive(false); // Disable the trigger at the start
        }

        UpdateEnemyCountText();
    }

    public void IncrementKillCount()
    {
        enemiesKilled++;
        UpdateEnemyCountText();
        CheckAllEnemiesKilled();
    }

    private void UpdateEnemyCountText()
    {
        int remainingEnemies = totalAIEnemies - enemiesKilled;

        if (remainingEnemies > 0)
        {
            enemyCountText.text = "Enemies Remaining: " + remainingEnemies;
        }
        else
        {
            enemyCountText.text = "ATM Activated";
        }
    }

    private void CheckAllEnemiesKilled()
    {
        if (enemiesKilled >= totalAIEnemies)
        {
            // All AI enemies are killed
            if (triggerObjectScript != null)
            {
                triggerObjectScript.SetActive(true); // Enable the trigger
            }
        }
    }
}
