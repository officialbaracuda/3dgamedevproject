using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum poolTag { FOOD, ENEMY };

    private Dictionary<poolTag, Queue<GameObject>> dictionary;
    
    [Serializable]
    public class Pool {
        public poolTag tag;
        public int size;
        public GameObject[] objects;
    }

    private int spawnRadius;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Transform target;

    public List<Pool> pools;

    #region SINGLETON
    public static Spawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        spawnRadius = 2;

        dictionary = new Dictionary<poolTag, Queue<GameObject>>();

        foreach (Pool pool in pools) {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                int index = UnityEngine.Random.Range(0, pool.objects.Length);
                GameObject obj = Instantiate(pool.objects[index]);
                obj.SetActive(false);
                if (pool.tag == poolTag.ENEMY) {
                    obj.GetComponent<Chaser>().SetTarget(target);
                }
                queue.Enqueue(obj);
            }
            dictionary.Add(pool.tag, queue);
        }
    }


    public void SpawnFromQueue(poolTag tag)
    {
        if (!isPoolEmpty(tag))
        {
            Vector2 circle = UnityEngine.Random.insideUnitCircle * spawnRadius;
            Vector3 pos = new Vector3(transform.position.x + circle.x, 1, transform.position.z + circle.y);
            int spawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
            
            GameObject obj = dictionary[tag].Dequeue();
            
            obj.SetActive(true);
            obj.transform.position = pos + spawnPoints[spawnPoint].position;
        }
    }

    public void Remove(poolTag tag, GameObject obj)
    {
        Debug.Log(obj.name + " added to pool");
        obj.SetActive(false);
        dictionary[tag].Enqueue(obj);
    }

    public int GetActiveEnemyCount() {
        Pool enemyPool = pools[0];
        foreach (Pool p in pools) {
            if (p.tag == poolTag.ENEMY) {
                enemyPool = p;
            }
        }

        return enemyPool.size - dictionary[poolTag.ENEMY].Count;
    }

    private bool isPoolEmpty(poolTag tag)
    {
        return dictionary[tag].Count <= 0;
    }
}
