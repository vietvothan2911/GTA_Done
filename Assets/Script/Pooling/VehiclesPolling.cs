using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesPolling : MonoBehaviour
{
    [Header("Car")]
    public List<GameObject> Car = new List<GameObject>();
    [SerializeField] List<GameObject> CarPool = new List<GameObject>();
    [Header("Motor")]
    public List<GameObject> Motor = new List<GameObject>();
    [SerializeField] List<GameObject> MotorPool = new List<GameObject>();
    [Header("Heli")]
    public List<GameObject> Heli = new List<GameObject>();
    [SerializeField] List<GameObject> HeliPool = new List<GameObject>();
    [Header("Tank")]
    public List<GameObject> Tank = new List<GameObject>();
    [SerializeField] List<GameObject> TankPool = new List<GameObject>();
    private int index;


    public void RandomVehicles(List<GameObject> vehicles)
    {
        index = Random.Range(0, vehicles.Count);

    }
    public GameObject GetPoolCar(Vector3 pos)
    {
        return GetPool(pos, Car, CarPool);
    }

    public GameObject GetPoolMotor(Vector3 pos)
    {

        return GetPool(pos, Motor, MotorPool);
    }
    public GameObject GetPoolHeli(Vector3 pos)
    {

        return GetPool(pos, Heli, HeliPool);
    }
    public GameObject GetPoolTank(Vector3 pos)
    {

        return GetPool(pos, Tank, TankPool);
    }
    public GameObject GetPool(Vector3 pos, List<GameObject> Vehicles, List<GameObject> VehiclesPool)
    {
        foreach (var vehicle in VehiclesPool)
        {
            if (!vehicle.activeSelf)
            {
                vehicle.SetActive(true);
                vehicle.transform.position = pos;
                return vehicle;
            }
        }
        RandomVehicles(VehiclesPool);
        GameObject newVehicle = Instantiate(Vehicles[index], pos, Quaternion.identity);
        newVehicle.SetActive(true);
        VehiclesPool.Add(newVehicle);
        return newVehicle;
    }

    public void ReturnPool()
    {
        // Triển khai logic ReturnPool cho CivilianPooling
    }
}
