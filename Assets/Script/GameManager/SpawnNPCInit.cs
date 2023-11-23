using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCInit : MonoBehaviour
{
    public static SpawnNPCInit ins;

    [SerializeField]
    private List<PointAIMove> aIMoveCar, aIMoveNPC;

    [SerializeField]
    private List<PointAiPolice> aIPolice;

    [SerializeField]
    private int maxVehicles, vehiclesIndex;

    public bool isSpawnVehicles;

    public int maxOnVehicles, onVehicles;

    public int onPolice, maxPolice;

    public void Awake()
    {
        ins = this;
    }
    public bool CheckCanSpawnPolice()
    {
        if(onPolice >= maxPolice)
        {
            return false;
        }
        return true;
    }

    public void Start()
    {
        isSpawnVehicles = true;
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < aIMoveCar.Count; i++)
        {
            //aIMoveCar[i].Init();
        }

        for (int i = 0; i < aIMoveNPC.Count; i++)
        {
            //aIMoveNPC[i].Init();
        }
        for(int i = 0;i < aIPolice.Count; i++) 
        {
            //aIPolice[i].Init();
        }
    }

    public void Vehicles(int _vehicles)
    {
        vehiclesIndex += _vehicles;
        if (vehiclesIndex >= maxVehicles)
        {
            isSpawnVehicles = false;
        }
    }


}
