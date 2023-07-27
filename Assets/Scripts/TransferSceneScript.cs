using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Thirdweb;
using UnityEngine.SceneManagement;

public class TransferSceneScript : MonoBehaviour
{
    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    public GameObject StartGameState;
    public GameObject ClaimNFTState;
    public GameObject LoadingState;

    string address;
    Contract erc1155Contract;
    Contract erc20Contract;

    public InputField toAddressInputField; // Reference to the input field for recipient's address
    public InputField tokenIdInputField; // Reference to the input field for token ID
    public InputField amountInputField; // Reference to the input field for amount
    public Button transferNFTButton; // Reference to the button for transferring ERC1155 tokens
    public Button transferERC20Button; // Reference to the button for transferring ERC20 tokens

    // Start is called before the first frame update
    void Start()
    {
        erc1155Contract = ThirdwebManager.Instance.SDK.GetContract("0x73EfadA5C7B523CFAF187dB4CF23cB5c6f63c5EE");
        erc20Contract = ThirdwebManager.Instance.SDK.GetContract("0x7980602A62D0E133A318D193Ce495A55128a130A");

        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
        StartGameState.SetActive(false);
        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(false);

        // Add listeners to the buttons to call the corresponding functions when clicked
        transferNFTButton.onClick.AddListener(TransferERC1155OnButtonClick);
        transferERC20Button.onClick.AddListener(TransferERC20OnButtonClick);
    }

    async public void ConnectWallet()
    {
        address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();

        ConnectedState.SetActive(true);
        DisconnectedState.SetActive(false);

        WalletNFTBalance();
        WalletERC20Balance();
    }

    public void DisconnectWallet()
    {
        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
    }

    async public void WalletNFTBalance()
    {
        var shrumezBalance = await erc1155Contract.ERC1155.BalanceOf(address, "0");
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

    async public void TransferERC1155ToAddress(string toAddress, string tokenId, int amount)
    {
        await erc1155Contract.ERC1155.Transfer(toAddress, tokenId, amount);

        WalletNFTBalance(); // Refresh the balance after the transfer
    }

    async public void TransferERC20ToAddress(string toAddress, string amount)
    {
        await erc20Contract.ERC20.Transfer(toAddress, amount);

        WalletERC20Balance(); // Refresh the balance after the transfer
    }

    async public void WalletERC20Balance()
    {
        var balance = await erc20Contract.ERC20.Balance();

        // Process the balance result and update the UI as needed
        // For example, you can display the balance in a text element
        // Example: balanceText.text = "Your ERC20 Balance: " + balance.ToString();
    }

    public void TransferERC1155OnButtonClick()
    {
        string toAddress = toAddressInputField.text;
        string tokenId = tokenIdInputField.text;
        int amount = int.Parse(amountInputField.text);

        TransferERC1155ToAddress(toAddress, tokenId, amount);
    }

    public void TransferERC20OnButtonClick()
    {
        string toAddress = toAddressInputField.text;
        string amount = amountInputField.text;

        TransferERC20ToAddress(toAddress, amount);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Boston");
    }
}
