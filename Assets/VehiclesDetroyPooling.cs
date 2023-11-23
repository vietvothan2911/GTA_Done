using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesDetroyPooling : MonoBehaviour
{

   
    public List<GameObject> VehiclesDestroy = new List<GameObject>();
    [Header("Car")]
    [SerializeField] List<GameObject> AudiPool = new List<GameObject>();
    [SerializeField] List<GameObject> BMWPool = new List<GameObject>();
    [SerializeField] List<GameObject> ChevroletPool = new List<GameObject>();
    [SerializeField] List<GameObject> PickUpPool = new List<GameObject>();
    [SerializeField] List<GameObject> JevroletPool = new List<GameObject>();
    [SerializeField] List<GameObject> JeepPool = new List<GameObject>();
    [SerializeField] List<GameObject> AmbulancePool = new List<GameObject>();
    [Header("Motor")]
    [SerializeField] List<GameObject> ChopperPool = new List<GameObject>();
    [SerializeField] List<GameObject> MoutainPool = new List<GameObject>();
    [SerializeField] List<GameObject> SportPool = new List<GameObject>();
    [SerializeField] List<GameObject> DirtPool = new List<GameObject>();
    [Header("Heli")]
    [SerializeField] List<GameObject> HeliPool = new List<GameObject>();
    [Header("Tank")]
    [SerializeField] List<GameObject> TankPool = new List<GameObject>();
    public GameObject GetVehiclesDetroyedPool(Vector3 pos, Vector3 forward,int index)
    {
        GameObject pool = null;
        switch (index)
        {
            case 0:
                pool = GetPool(0, AudiPool);
                break;
            case 1:
                pool = GetPool(1, BMWPool);
                break;
            case 2:
                pool = GetPool(2, ChevroletPool);
                break;
            case 3:
                pool = GetPool(3, PickUpPool);
                break;
            case 4:
                pool = GetPool(4, JevroletPool);
                break;
            case 5:
                pool = GetPool(5, JeepPool);
                break;
            case 6:
                pool = GetPool(6, AmbulancePool);
                break;
            case 7:
                pool = GetPool(7, ChopperPool);
                break;
            case 8:
                pool = GetPool(8, MoutainPool);
                break;
            case 9:
                pool = GetPool(9, SportPool);
                break;
            case 10:
                pool = GetPool(10, DirtPool);
                break;
            case 11:
                pool = GetPool(11, HeliPool);
                break;
            case 12:
                pool = GetPool(12, TankPool);
                break;
            default:
                break;
        }
        pool.transform.rotation = Quaternion.LookRotation(forward);
        pool.transform.position = pos;
        return pool;
    }
    public GameObject GetPool(int index, List<GameObject> VehiclesPool)
    {
        foreach (var vehicle in VehiclesPool)
        {
            if (!vehicle.activeSelf)
            {
                vehicle.SetActive(true);
                return vehicle;
            }
        }
        GameObject newVehicle = Instantiate(VehiclesDestroy[index]);
        newVehicle.SetActive(true);
        VehiclesPool.Add(newVehicle);
        return newVehicle;
    }

    public void ReturnPool()
    {
        // Triển khai logic ReturnPool cho CivilianPooling
    }
}
public enum SelectVehiclesDetroy
{
    Audi,
    BMW,
    Chevrolet,
    PickUp,
    Jevrolet,
    Jeep,
    Ambulance,
    Chopper,
    Moutain,
    Sport,
    Dirt,
    Heli,
    Tank

}