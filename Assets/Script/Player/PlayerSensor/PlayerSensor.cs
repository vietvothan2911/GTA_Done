using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] PlayerSensorHuman human;
    [SerializeField] PlayerSensorVehicles vehicle;

    public Transform ReturnHuman()
    {
        if (human.ObjectCollision != null)
        {
            return human.ObjectCollision;
        }
        return null;
    }
    public Transform ReturnVehicle()
    {
        if (vehicle.ObjectCollision != null)
        {
            return vehicle.ObjectCollision;
        }
        return null;
    }

}

