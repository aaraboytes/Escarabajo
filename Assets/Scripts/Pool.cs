using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject[] pooledObj;
    public int amount;
    private List<GameObject> pool;
    void Awake()
    {
        GameObject Bucket = new GameObject(pooledObj[0].name + "_bucket");
        pool = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject aux = (GameObject)Instantiate(pooledObj[Random.Range(0,pooledObj.Length)]);
            aux.SetActive(false);
            aux.transform.SetParent(Bucket.transform);
            pool.Add(aux);
        }
    }

    public GameObject Recycle(Vector3 position)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = position;
                if (pool[i].GetComponent<ParticleSystem>())
                {
                    ParticleSystem ps = pool[i].GetComponent<ParticleSystem>();
                    ps.time = 0;
                    ps.Play();
                }
                if (pool[i].GetComponent<Rigidbody>())
                {
                    Rigidbody rb = pool[i].GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                }
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }
}