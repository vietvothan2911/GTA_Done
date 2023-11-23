using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZoneCanSpawn : MonoBehaviour
{
    public List<GameObject> Zones= new List<GameObject>();
    void OnBecameVisible()
    {
       foreach(var zone in Zones)
        {
            zone.SetActive(true);
        }
       
    }

    void OnBecameInvisible()
    {

        foreach (var zone in Zones)
        {
            zone.SetActive(false);
        }
    
    }
}
