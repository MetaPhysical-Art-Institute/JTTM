using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{

    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    public GameObject StartGameState;
    public GameObject ClaimNFTState;
    public GameObject LoadingState;

    string address;
    Contract contract;

    // Start is called before the first frame update
    void Start()
    {
        contract = ThirdwebManager.Instance.SDK.GetContract("0xbcaF4F275C315D4d092A122e20fF23e56D6A0043");

        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
        StartGameState.SetActive(false);
        ClaimNFTState.SetActive(false); 
        LoadingState.SetActive(false);
        
    }

    async public void ConnectWallet()

    {
     address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
       

        ConnectedState.SetActive(true);
        DisconnectedState.SetActive(false);

        WalletNFTBalance();
        
    }

       public void DisconnectWallet()

    {
        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
        
    }
    
    async public void WalletNFTBalance()
    {
        var shrumezBalance = await contract.ERC1155.BalanceOf(address, "0");
        var shrumezBalanceInt = int.Parse(shrumezBalance);

        if (shrumezBalanceInt > 0)
        {
            StartGameState.SetActive(true);
            ClaimNFTState.SetActive(false);
        }
        else 
        {
            StartGameState.SetActive(false);
            ClaimNFTState.SetActive(true);
        }
    }

    async public void ClaimShrumez()
    {

        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(true);

        var claimResult = await contract.ERC1155.Claim("0",1);

        if (claimResult.isSuccessful())
        {
            LoadingState.SetActive(false);
            WalletNFTBalance();

        }
        else
        {
            Debug.Log("Failed to claim!");
        }

    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Boston");
    }
   
}
