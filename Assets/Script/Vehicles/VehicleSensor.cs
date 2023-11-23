using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleSensor : MonoBehaviour
{
    public IDriverVehicles driverVehicles;
    public bool collisionwithhuman;
    public bool collisionwithobject;
    public bool collisionwithvehicles;
    public bool collisionwithtrafficlight;
    public bool cancheckcollision;
    public GameObject objcollisionwithhuman;
    public GameObject objcollisionwithobject;
    public GameObject objcollisionwithvehicles;
    public GameObject objcollisionwithtrafficlight;

    private void Start()
    {
        driverVehicles = transform.parent.GetComponent<IDriverVehicles>();
    }
    private void OnEnable()
    {
        collisionwithhuman = false;
        collisionwithobject = false;
        collisionwithvehicles = false;
        collisionwithtrafficlight = false;

    }
    IEnumerator CheckExitTrafficlight()
    {
        yield return new WaitForSeconds(2);
        if (objcollisionwithtrafficlight.activeSelf)
        {
            StartCoroutine(CheckExitTrafficlight());
        }
        else
        {
            collisionwithtrafficlight = false;
            yield break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (driverVehicles._driver == null) return;
        if (GameManager.ins.layerData.HumanLayer == (GameManager.ins.layerData.HumanLayer | (1 << other.gameObject.layer)))
        {

            if (other.gameObject.transform.parent==null) return;
            if (other.gameObject.transform.parent.gameObject == driverVehicles._driver) return;
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerSensorVehicles>() != null)
                {
                    collisionwithhuman = true;
                    objcollisionwithhuman = Player.ins.gameObject;
                    StartCoroutine(CheckExitHuman());
                }
            }
            else
            {
                collisionwithhuman = true;
                objcollisionwithhuman = other.gameObject.transform.parent.gameObject;
                StartCoroutine(CheckExitHuman());
            }

        }
         if (GameManager.ins.layerData.VehiclesLayer == (GameManager.ins.layerData.VehiclesLayer | (1 << other.gameObject.layer)))
        {
            collisionwithvehicles = true;
            objcollisionwithvehicles = other.gameObject.transform.parent.gameObject;
            StartCoroutine(CheckExitVehicles());
        }
         if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << other.gameObject.layer)))
        {
            collisionwithobject = true;
            objcollisionwithobject = other.gameObject.transform.parent.gameObject;
            StartCoroutine(CheckExitObject());

        }
        if (GameManager.ins.layerData.Trafficlight == (GameManager.ins.layerData.Trafficlight | (1 << other.gameObject.layer)))
        {
            collisionwithtrafficlight = true;
            objcollisionwithtrafficlight = other.gameObject;
            StartCoroutine(CheckExitTrafficlight());
        }



    }
    public void Return()
    {
        StopAllCoroutines();
        driverVehicles._driver.transform.parent = null;
        driverVehicles._driver.SetActive(false);
        driverVehicles._driver = null;
        transform.parent.gameObject.SetActive(false);

    }


    IEnumerator CheckExitHuman()
    {
        yield return new WaitForSeconds(5);

        
        if (Vector3.Distance(transform.position, Player.ins.transform.position) > 100)
        {
            Return();
            yield break;

        }
        else if (!objcollisionwithhuman.activeSelf||Vector3.Distance(transform.position,objcollisionwithhuman.transform.position)>5)
        {
            collisionwithhuman = false;
            yield break;
           
        }
        else
        {
            StartCoroutine(CheckExitHuman());
        }
    }
    IEnumerator CheckExitVehicles()
    {
        yield return new WaitForSeconds(5);


        if (Vector3.Distance(transform.position, Player.ins.transform.position) > 100)
        {
            Return();
            yield break;

        }
        else if (!objcollisionwithvehicles.activeSelf || Vector3.Distance(transform.position, objcollisionwithvehicles.transform.position) >10)
        {
            collisionwithvehicles = false;
            yield break;

        }
        else
        {
            StartCoroutine(CheckExitVehicles());
        }
    }
    IEnumerator CheckExitObject()
    {
        yield return new WaitForSeconds(5);


        if (Vector3.Distance(transform.position, Player.ins.transform.position) > 100)
        {
            Return();
            yield break;

        }
        else
        {
            StartCoroutine(CheckExitObject());
        }
    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            collisionwithhuman = false;
        }
        else if (objcollisionwithhuman == other.gameObject.transform.parent.gameObject)
        {
            collisionwithhuman = false;
        }
        else if (objcollisionwithvehicles == other.gameObject.transform.parent.gameObject)
        {
            collisionwithvehicles = false;
        }
        else if (objcollisionwithobject == other.gameObject.transform.parent.gameObject)
        {
            collisionwithobject = false;
        }

    }
}
