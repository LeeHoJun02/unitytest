using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab;
    private int poolSize = 10;
    // public GameObject[] pool; >> 여러개의 프리팹을 담을 수 있음
    List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(enemyPrefab);
            newObj.SetActive(false); // 
            pool.Add(newObj); // 
        }
    }

    public GameObject Get()
    {
        GameObject Select = null;
        foreach (GameObject item in pool)
        {
            if (!item.activeInHierarchy)
            {
                Select = item;
                item.SetActive(false);
                return item;
            }
        }
        if (!Select)
        {
            Select = Instantiate(enemyPrefab);
            pool.Add(Select);
        }
        return Select;
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
    }
}
