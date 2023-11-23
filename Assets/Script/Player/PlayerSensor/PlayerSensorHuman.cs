using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorHuman : IHumanSensor
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.transform.parent==null) return;
            if (other.gameObject.transform.parent.gameObject == transform.parent.gameObject) return;

            if (other.gameObject.transform.parent == transform) return;

            ObjectCollision = other.gameObject.transform.parent;

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent == ObjectCollision)
        {

            ObjectCollision = null;
        }
    }
}
