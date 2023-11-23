using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesHPData : MonoBehaviour
{
    public static VehiclesHPData ins;
    public float rateSmoke;
    public float rateFire;
    public float rateExplosion;
    public void Awake()
    {
        ins = this;
    }

}
