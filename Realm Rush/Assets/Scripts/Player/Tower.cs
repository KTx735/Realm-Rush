using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] int cost = 75; //Cost for tower
    [SerializeField] float buildDelay = 1.5f;
    [SerializeField] AudioSource audioSorce;
    [SerializeField] AudioClip build;

    void Start()
    {
        StartCoroutine(Build());
        audioSorce = GetComponent<AudioSource>();
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost){
        Instantiate(tower.gameObject, position, Quaternion.identity);
        bank.Withdraw(cost);
        return true;
        }
        return false;
    }

    IEnumerator Build()
    {
        //Set everything to be set as inactive in the hierarchy
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        //Set each section as active in the hierarchy
        foreach(Transform child in transform)
        {
            audioSorce.Play();
            audioSorce.PlayOneShot(build, 1f);
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(true);
            }
        }

    }

}
