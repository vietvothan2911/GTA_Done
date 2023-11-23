using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MotorData", menuName = "Vehicles/MotorData")]
public class MotorData : VehiclesData
{
    public MotorType motorType;
    public MotorData()
    {
        type = (int)motorType;
    }
}
