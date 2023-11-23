using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesData : ScriptableObject
{
    public SelectVehiclesDetroy selectVehiclesDetroy;
    public float HP;
    public float wantedPoint;
    [Header("Vehicle Force")]
    public float maxspeed;
    public float accelerationForce;
    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    [Header("Vehicle breaking")]
    public float breakingForce = 500f;
    public float breakingForceMax = 1000f;
    public int type ;
   
}