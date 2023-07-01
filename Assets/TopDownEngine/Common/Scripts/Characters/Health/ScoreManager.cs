using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int totalAIEnemies;
    private int enemiesKilled;

    public Text enemyCountText; // Reference to the UI text element
    public GameObject glowObject; // Reference to the game object with the glow effect

    public TriggerObjectScript triggerObjectScript; // Reference to the TriggerObjectScript

    private bool isClaiming = false; // Track if a claim is in progress
    private bool isClaimed = false; // Track if the claim has been made

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

        if (isClaiming)
        {
            enemyCountText.text = "Loading...";
        }
        else if (remainingEnemies > 0)
        {
            enemyCountText.text = "Enemies Remaining: " + remainingEnemies;
        }
        else if (!isClaimed)
        {
            enemyCountText.text = "ATM Activated";
        }
        else
        {
            enemyCountText.text = "Enter the Portal";
            DisableGlowEffect();
        }
    }

    public void SetClaimingStatus(bool claiming)
    {
        isClaiming = claiming;
        UpdateEnemyCountText();
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

    private void DisableGlowEffect()
    {
        if (glowObject != null)
        {
            glowObject.SetActive(false); // Disable the game object with the glow effect
        }
    }
}
