using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;

    [SerializeField] int currentHitPoints = 0;
    Enemy enemy;
    [SerializeField] AudioSource audioSorce;
    [SerializeField] AudioClip destroy;
    [SerializeField] AudioClip arrow;


    void OnEnable()
    {
        currentHitPoints = maxHitPoints;        
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        audioSorce = GetComponent<AudioSource>();
    }

    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

    void ProcessHit() 
    {

        audioSorce.PlayOneShot(arrow, 0.1f);
        currentHitPoints--;

        //This is just a quick fix for the sfx while I look on why
        //the sound does not play when healt is 0 before "destroy"
        if(currentHitPoints == 1)
        {
            audioSorce.PlayOneShot(destroy, 0.3f);
        }

        if(currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
