using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{

    [SerializeField] int startingBalance = 100;
    [SerializeField] int currentBalance;
    public int CurrentBalance{get {return currentBalance;} }

    [SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {
        currentBalance = startingBalance;
        updateDisplay();
    }

    public void Deposit(int amount)
    {
        //Make every amount into positive
        currentBalance += Mathf.Abs(amount);
        updateDisplay();
    }

    public void Withdraw(int amount)
    {
        //Guard from adding more negative
        currentBalance -= Mathf.Abs(amount);
        updateDisplay();

        if (currentBalance < 0)
        {
            //Loose the game
            //For prototype just reload the scene
            RelaodScene();
        }
    }

    void RelaodScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void updateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }

}
