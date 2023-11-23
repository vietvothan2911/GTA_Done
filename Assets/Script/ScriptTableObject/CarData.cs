using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CarData", menuName = "Vehicles/CarData")]
public class CarData : VehiclesData
{
    public CarType carType;
    public CarData()
    {
        type = (int)carType;
    }
   
}
