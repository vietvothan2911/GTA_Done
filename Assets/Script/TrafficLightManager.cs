using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrafficLightManager : MonoBehaviour
{
    
   public List<Direction> direction;
    public float timeRed;
    public float timeGreen;
    public float timeYellow;
    private float total;
    private void Start()
    {
        total = direction.Count;
        Invoke("StartRunTrafficLight", UnityEngine.Random.Range(0, 2));
    }
   public void StartRunTrafficLight()
    {
        StartCoroutine(Red());
    }
    IEnumerator Red()
    {
        foreach(var light in direction[0].trafficLight)
        {
            light.Red();
            light.Carcollider();
            
        }
        if (total > 1)
        {
            foreach (var light in direction[1].trafficLight)
            {
                light.Green();
                light.Pedestriancollider();
            }
        }
        yield return new WaitForSeconds(timeRed);
        StartCoroutine(Yellow(false));

    }
    IEnumerator Green()
    {
        foreach (var light in direction[0].trafficLight)
        {
            light.Green();
            light.Pedestriancollider();
        }
        if (total > 1)
        {
            foreach (var light in direction[1].trafficLight)
            {
                light.Red();
                light.Carcollider();
            }
        }
        yield return new WaitForSeconds(timeGreen);
        StartCoroutine(Yellow(true));
    }
    IEnumerator Yellow(bool isred)
    {
        
        foreach (var light in direction[0].trafficLight)
        {
            light.Yellow();
        }
        if (total > 1)
        {
            foreach (var light in direction[1].trafficLight)
            {
                light.Yellow();
            }
        }
        yield return new WaitForSeconds(timeYellow);
        if (isred==true)
        {
            StartCoroutine(Red());
        }
        else
        {
            StartCoroutine(Green());
        }
    }

}




[Serializable]
public struct Direction 
{
   public List<TrafficLight> trafficLight;
}

