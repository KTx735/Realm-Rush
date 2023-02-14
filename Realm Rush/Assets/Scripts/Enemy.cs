using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 100;

    //Reference to access bank
    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        //Guard Statement: Make sure Bank exist on the scene
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }

        public void StealGold()
    {
        //Guard Statement: Make sure Bank exist on the scene
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
    }

}
