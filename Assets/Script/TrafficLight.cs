using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject redLight;
    public GameObject greenLight;
    public GameObject yellowLight;
    public GameObject CarCollider;
    public List<GameObject> PedestrianCollider=new List<GameObject>();

    public void Red()
    {
        redLight.SetActive(true);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
       
    }
    public void Green()
    {
        greenLight.SetActive(true);
        redLight.SetActive(false);
        yellowLight.SetActive(false);
    }
    public void Yellow()
    {
        yellowLight.SetActive(true);
        greenLight.SetActive(false);
        redLight.SetActive(false);
       

    }
    public void Carcollider()
    {
        CarCollider.SetActive(true);
        foreach (var pedestrian in PedestrianCollider)
        {
            pedestrian.SetActive(false);
        }
    }
    public void Pedestriancollider()
    {
        CarCollider.SetActive(false);
        foreach (var pedestrian in PedestrianCollider)
        {
            pedestrian.SetActive(true);
        }
    }


}
