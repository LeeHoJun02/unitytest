using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public float spawnInterval = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameManager.instance.pool.Get();
        }

    }
}