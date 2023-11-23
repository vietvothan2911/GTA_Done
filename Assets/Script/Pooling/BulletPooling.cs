using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : GameObjectPooling
{
    public static BulletPooling ins;
    public GameObject rocket;
    public List<GameObject> rocketPool = new List<GameObject>();
    public GameObject laser;
    public List<GameObject> laserPool = new List<GameObject>();
    public GameObject rocketHeli;
    public List<GameObject> rocketHeliPool = new List<GameObject>();
   
    void Start()
    {
        ins = this;
    }
    public GameObject GetRocketHeliPool(Vector3 pos)
    {
        return GetPool(pos, rocket, rocketHeliPool);
    }
    public GameObject GetRocketPool(Vector3 pos)
    {
        return GetPool(pos, rocket, rocketPool);
    }
    public GameObject GetLaserPool(Vector3 pos)
    {
        return GetPool(pos, laser, laserPool);
    }
   
}
