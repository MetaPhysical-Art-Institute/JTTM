using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int totalAIEnemies;
    private int enemiesKilled;

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
    }

    public void IncrementKillCount()
    {
        enemiesKilled++;
        CheckAllEnemiesKilled();
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
