using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCDriver : MonoBehaviour
{

    [Header("Control")]
    public DriverType driverType;
    public NPCControl npcControl;
    public FindThePath findThePath;
    public bool ischangedirection;
    [Header("State")]
    public bool candriver;
    float forward = 1;
    [Header("Vehicle")]
    public GameObject Vehicle;
    private IDriverVehicles driverVehicles;
    public float currentspeed;
    public float steeringAngle;
    public VehicleSensor sensor;
    private float timedelay;
    public bool isDriver;
    public float font;
    private Transform pointTarget;
    public void DriverVehicle()
    {
        GetInVehicles();
        Driver();
    }
    public void IsTakeDame()
    {
        if (npcControl.isPolice)
        {
            ChangeDriverState(DriverType.Pursue);
        }
        else
        {
            ChangeDriverState(DriverType.Runaway);

        }
    }
    public void ChangeDriverState(DriverType _driverType)
    {
        driverType = _driverType;
        if (driverType == DriverType.Pursue)
        {
            StartCoroutine(CouroutineReturn());
        }


    }
    IEnumerator CouroutineReturn()
    {
        yield return new WaitForSeconds(5f);
        if (driverType != DriverType.Pursue)
        {
            yield break;
        }
        if (Vector3.Distance(transform.position, Player.ins.transform.position) > 100)
        {
            Return();
            yield break;
        }
        else
        {
            StartCoroutine(CouroutineReturn());
        }

    }
    public void GetInVehicles()
    {

       
        if (npcControl.chacractorData.vehicle == SelectVehicles.Car)
        {
            if (npcControl.isPolice)
            {
                Vehicle = PoolingManager.ins.vehiclesPolicePooling.GetPoolCar(transform.position);
            }
            else
            {
                Vehicle = PoolingManager.ins.vehiclesPolling.GetPoolCar(transform.position);
            }
            npcControl.animator.Play("SitInCar");
            npcControl.animator.SetFloat("CarType", ((int)Vehicle.GetComponent<Car>().carData.type));

        }
        else if (npcControl.chacractorData.vehicle == SelectVehicles.Motor)
        {
            Vehicle = PoolingManager.ins.vehiclesPolling.GetPoolMotor(transform.position);
            npcControl.animator.Play("SitInMotor");
            npcControl.animator.SetFloat("MotorType", ((int)Vehicle.GetComponent<Motor>().motorData.type));

        }
        Vehicle.transform.LookAt(npcControl.pointtarget);
        driverVehicles = Vehicle.GetComponent<IDriverVehicles>();
        sensor = driverVehicles._sensor;
        driverVehicles._driver = gameObject;
        transform.parent = driverVehicles._driverSit;
        driverVehicles._driver = gameObject;
    }
    public void Driver()
    {

        StartCoroutine(DriverCourotine());
    }

    public void EndDriver()
    {
        StopCoroutine(DriverCourotine());
    }
    IEnumerator DriverCourotine()
    {

        while (candriver)
        {
            if (Vehicle == null)
            {
                yield break;
            }
            DriverManager();
            driverVehicles.DriverVehicles(forward, 0, steeringAngle * npcControl.chacractorData.speedRotate * forward, currentspeed);
           
            yield return null;


        }
        yield break;

    }
    public void DriverManager()
    {

        switch (driverType)
        {
            case DriverType.Normal:
                NormalDriver();
                break;
            case DriverType.Runaway:
                RunAwayDriver();
                break;
            case DriverType.Pursue:
                PursueDriver();
                break;
            default:
                break;
        }
    }
    public void NormalDriver()
    {

        if (sensor.collisionwithhuman )
        {
            driverVehicles._rb.velocity = Vector3.zero;
            currentspeed = 0;
            forward = 0;
        }
        else if ( sensor.collisionwithobject || sensor.collisionwithtrafficlight|| sensor.collisionwithvehicles)
        {
            bool isFront = false;
            if (sensor.collisionwithvehicles)
            {
                float angle = Vector3.Angle(sensor.objcollisionwithvehicles.transform.position-Vehicle.transform.position, Vehicle.transform.forward);
                isFront = angle >45;
                font = angle;


            }
            else if (sensor.collisionwithobject)
            {
                float angle = Vector3.Angle(sensor.objcollisionwithobject.transform.position - Vehicle.transform.position, Vehicle.transform.forward);
                isFront = angle > 45;
                font = angle;
            }
           
            if (isFront)
            {

                currentspeed = npcControl.chacractorData.speed;
                forward = 1;
                return;
            }
            currentspeed = 0;
            forward = 0;
            driverVehicles.ApplyBreaks(1000);
        }
        
        else
        {
            currentspeed = npcControl.chacractorData.speed;
            forward = 1;
            driverVehicles._rb.isKinematic = false;
        }
        Vector3 relativeVector = Vehicle.transform.InverseTransformPoint(npcControl.pointtarget.position);
        steeringAngle = (relativeVector.normalized.x / relativeVector.normalized.magnitude);
        currentspeed -= currentspeed * Mathf.Clamp(Mathf.Abs(steeringAngle), 0, 0.5f);
        forward -= forward * Mathf.Clamp(Mathf.Abs(steeringAngle), 0, 0.5f);
        if (Vector3.Distance(transform.position, npcControl.pointtarget.position) < 5f)
        {
            NextPoint();
        }

    }
    public void RunAwayDriver()
    {
        findThePath.PathProgress(npcControl.pointtarget, Vehicle.transform);
        Vector3 relativeVector = Vehicle.transform.InverseTransformPoint(findThePath.PostionToFollow);
        if (sensor.collisionwithhuman)
        {
            //    if (sensor.objcollisionwithhuman.CompareTag("Player"))
            //    {
            //        GetOutVehicle();
            //    }
        }
        else if (sensor.collisionwithobject || sensor.collisionwithvehicles)
        {

            timedelay += Time.deltaTime;
            if (timedelay > 2 && !ischangedirection)
            {
                steeringAngle = (relativeVector.x > 1) ? 1 : -1;
                currentspeed = npcControl.chacractorData.maxspeed;
                timedelay = 0;
                StartCoroutine(ChangeDirection());
            }

        }


        else
        {
            currentspeed = npcControl.chacractorData.maxspeed;
            driverVehicles._rb.isKinematic = false;
            forward = 1;
        }
        if (!ischangedirection)
        {
            steeringAngle = (relativeVector.normalized.x / relativeVector.normalized.magnitude);
            currentspeed -= currentspeed * Mathf.Clamp(Mathf.Abs(steeringAngle), 0, 0.5f);
        }
        //else
        //{
        //    steeringAngle = (relativeVector.x > 1) ? 1 : -1;
        //    currentspeed = npcControl.chacractorData.maxspeed;
        //}

        if (Vector3.Distance(transform.position, npcControl.pointtarget.position) < 5f)
        {
            NextPoint();
        }
    }
    public void PursueDriver()
    {
        findThePath.PathProgress(Player.ins.transform, Vehicle.transform);
        Vector3 relativeVector = Vehicle.transform.InverseTransformPoint(findThePath.PostionToFollow);
        if (sensor.collisionwithhuman)
        {
            if (sensor.objcollisionwithhuman.CompareTag("Player"))
            {
                GetOutVehicle();
            }
            //driverVehicles._rb.velocity = Vector3.zero;
            //currentspeed = 0;
            //forward = 0;
        }
        else if (sensor.collisionwithobject || sensor.collisionwithtrafficlight || sensor.collisionwithvehicles)
        {
            if (Vector3.Distance(transform.position, Player.ins.transform.position) < 10)
            {
                GetOutVehicle();
            }
            timedelay += Time.deltaTime;
            if (timedelay > 2 && !ischangedirection)
            {
                steeringAngle = (relativeVector.x > 1) ? 1 : -1;
                currentspeed = npcControl.chacractorData.maxspeed;
                timedelay = 0;
                StartCoroutine(ChangeDirection());
            }

        }
       

        else
        {
            currentspeed = npcControl.chacractorData.maxspeed;
            driverVehicles._rb.isKinematic = false;
            forward = 1;
        }
       
        if (!ischangedirection)
        {
            steeringAngle = (relativeVector.normalized.x / relativeVector.normalized.magnitude);
            currentspeed -= currentspeed * Mathf.Clamp(Mathf.Abs(steeringAngle), 0, 0.5f);
        }
        //else
        //{
        //    steeringAngle = (relativeVector.x > 1) ? 1 : -1;
        //    currentspeed = npcControl.chacractorData.maxspeed;
        //}
       
    }
    public void Return()
    {
        
        gameObject.transform.parent = null;
        driverVehicles._driver = null;
        Vehicle.SetActive(false);
        gameObject.SetActive(false);
    }

    public void NextPoint()
    {
        if (Vector3.Distance(transform.position, Player.ins.transform.position) >= 125)
        {
            Return();

            return;
        }
        else if (driverType == DriverType.Pursue)
        {
            GetOutVehicle(false);
            return;
        }
        else if (npcControl.pointtarget.gameObject.GetComponent<PointAIMove>() != null)
        {
            npcControl.pointtarget = npcControl.pointtarget.gameObject.GetComponent<PointAIMove>().RandomNextPoint();
            if (driverType == DriverType.Runaway)
            {
                driverType = DriverType.Normal;
            }
            return;
        }
    }
    IEnumerator ChangeDirection()
    {
        ischangedirection = true;
        findThePath.ClearWayPoint();
        forward = -1;
        yield return new WaitForSeconds(2f);
        forward = 1;

        ischangedirection = false;

    }
    public void GetOutVehicle(bool isEject=false,float side=0)
    {
      
        candriver = false;
        transform.parent = null;
        Vehicle = null;
        driverVehicles._rb.velocity = Vector3.zero;
        driverVehicles.Return();
        if (npcControl.chacractorData.vehicle == SelectVehicles.Car)
        {

            GetOutCar(isEject,side);

        }
        else if (npcControl.chacractorData.vehicle == SelectVehicles.Motor)
        {
            GetOutMotor(isEject, side);

        }
        npcControl.npcState.ChangeState(SelectState.Attack);
     
    }
    public void GetOutCar(bool isEject, float side )
    {
       
        if (isEject)
        {
            transform.position = driverVehicles._enterFormPos[0].position;
            npcControl.animator.Play("exit_force3");
        }
        else
        {
            npcControl.animator.Play("GetOutCar");
            npcControl.animator.applyRootMotion = true;
        }
        driverVehicles = null;
    }
    public void GetOutMotor(bool isEject, float side)
    {
        if (isEject)
        {
            npcControl.animator.SetTrigger("GetOutVehicles");
        }
        else
        {
            npcControl.animator.SetTrigger("GetOutVehicles");
        }
        driverVehicles = null;

    }
 
    public void EndDeadVehicle()
    {
        npcControl.DoFallAction();
    }


}

