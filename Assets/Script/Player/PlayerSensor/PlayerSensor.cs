using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] PlayerSensorHuman human;
    [SerializeField] PlayerSensorVehicles vehicle;

    public Transform ReturnHuman()
    {
       
            return human.ObjectCollision;
       
    }
    public Transform ReturnVehicle()
    {
        
            return vehicle.ObjectCollision;
       
    }

}

