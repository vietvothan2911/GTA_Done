using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPooling : GameObjectPooling
{
  
    [Header("Armor")]
    public List<GameObject> ArmorPool= new List<GameObject>();
    public int maxAromor=10;
    public float rateArmor = 25;
    [Header("Health")]
    public List<GameObject> HealthPool = new List<GameObject>();
    public int maxHealth=10;
    public float rateHealth = 25;
    [Header("Money")]
    public List<GameObject> MoneyPool = new List<GameObject>();
    public int maxMoney=10;
    public float rateMoney = 100;
    public GameObject GetPoolArmor(Vector3 pos)
    {
        if (Random.Range(0,101)>rateArmor) return null;
        return GetPool(pos, gameObjects[0], ArmorPool);
    }
    public GameObject GetPoolHealth(Vector3 pos)
    {
        if (Random.Range(0, 101) > rateHealth) return null;
        return GetPool(pos, gameObjects[1], HealthPool);
    }
    public GameObject GetPoolMoney(Vector3 pos)
    {
        if ( Random.Range(0, 101) > rateMoney) return null;
        return GetPool(pos, gameObjects[2], MoneyPool);
    }
    public void GetPickUp(Vector3 pos)
    {
        GetPoolArmor(pos + new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)));
        GetPoolHealth(pos + new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)));
        GetPoolMoney(pos + new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)));

    }
}
