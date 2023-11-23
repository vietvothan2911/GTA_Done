using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPooling : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> SkeletonPool = new List<GameObject>();
    public GameObject GetSkeletonPool(Vector3 pos)
    {
        return GetPool( pos, gameObjects[0], SkeletonPool);
    }
    public GameObject GetPool(Vector3 pos, GameObject poolObj, List<GameObject> Pool)
    {
        foreach (var pool in Pool)
        {

            if (!pool.activeInHierarchy)
            {
                pool.SetActive(true);
                pool.transform.position = pos;
                pool.transform.rotation = Quaternion.identity;
                return pool;
            }
        }
        GameObject newpoolObj = Instantiate(poolObj, pos, Quaternion.identity);
        Pool.Add(newpoolObj);
        return newpoolObj;
    }
}
