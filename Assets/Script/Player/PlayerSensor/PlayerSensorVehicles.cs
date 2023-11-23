using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorVehicles : IHumanSensor
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SensorVehicles"))
        {
            

                ControlsManager.ins.Control[0].GetComponent<CharacterControl>().getInVehicles.SetActive(true);
                ObjectCollision = other.gameObject.transform.parent;

            


        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (ObjectCollision == other.gameObject.transform.parent)
        {
            ObjectCollision = null;
            ControlsManager.ins.Control[0].GetComponent<CharacterControl>().getInVehicles.SetActive(false);
        }

    }

}
