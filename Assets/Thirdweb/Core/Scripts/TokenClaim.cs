using UnityEngine;
using UnityEngine.UI;
using Thirdweb;

public class TokenClaim : MonoBehaviour
{
    private bool isClaimed = false; // Track if the claim has been made
    private bool isClaiming = false; // Track if a claim is in progress

    public GameObject nextSceneTrigger; // Reference to the game object with the trigger

    public async void Claim()
    {
        if (!isClaimed && !isClaiming)
        {
            isClaiming = true; // Set the claiming flag to true

            Contract contract = ThirdwebManager.Instance.SDK.GetContract("0x7980602A62D0E133A318D193Ce495A55128a130A");
            await contract.ERC20.Claim("5");

            isClaimed = true; // Mark the claim as made
            DisableClaimButton(); // Disable the claim button

            // Enable the next scene trigger game object
            if (nextSceneTrigger != null)
            {
                nextSceneTrigger.SetActive(true);
            }

            isClaiming = false; // Reset the claiming flag
        }
    }

    private void DisableClaimButton()
    {
        // Disable the claim button or any other necessary UI element
        // For example:
        GetComponent<Button>().interactable = false;
    }
}
