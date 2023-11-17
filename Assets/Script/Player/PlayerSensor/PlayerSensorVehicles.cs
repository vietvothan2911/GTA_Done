using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorVehicles : IHumanSensor
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.ins.layerData.VehiclesLayer == (GameManager.ins.layerData.VehiclesLayer | (1 << other.gameObject.layer)))
        {
            if (other.GetComponent<VehicleSensor>() != null)
            {
                ControlsManager.ins.Control[0].GetComponent<CharacterControl>().getInVehicles.SetActive(true);
                ObjectCollision = other.gameObject.transform.parent;

            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.parent = null)
        {
            return;
        }
        if (ObjectCollision == other.gameObject.transform.parent)
        {
            ObjectCollision = null;
            ControlsManager.ins.Control[0].GetComponent<CharacterControl>().getInVehicles.SetActive(false);
        }

    }

}
