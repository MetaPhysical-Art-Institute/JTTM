using UnityEngine;
using UnityEngine.UI;

public class TriggerObjectScript : MonoBehaviour
{
    public Canvas uiCanvas;
    public Button claimButton;

    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            // Show the UI Canvas and button when the player enters the trigger
            if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(true);
            }

            if (claimButton != null)
            {
                claimButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the UI Canvas and button when the player leaves the trigger
            if (uiCanvas != null)
            {
                uiCanvas.gameObject.SetActive(false);
            }

            if (claimButton != null)
            {
                claimButton.gameObject.SetActive(false);
            }
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
