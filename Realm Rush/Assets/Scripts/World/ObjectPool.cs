using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTime = 2f;

    GameObject[] pool; //Will hold all the instantiated objects

    void Awake()
    {
        PopulatePool();    
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            //Position and rotation is being handle by EnemyMover
            pool[i] = Instantiate(enemy, transform); 
            //Disactivate GameObject
            pool[i].SetActive(false); 
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }//End of Enable ObjectInPool

}
