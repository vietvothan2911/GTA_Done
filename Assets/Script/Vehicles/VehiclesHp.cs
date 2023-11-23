using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VehiclesHp : MonoBehaviour, IDameExplosion,IDameLaser
{
    public IDriverVehicles driverVehicles;
    [SerializeField]
    private float hp;
    private float maxHP;
    public int index;
    private Transform damePoint;
    private ParticleSystem smokeFx;
    private ParticleSystem fireFx;


    public void Start()
    {
        driverVehicles = gameObject.transform.parent.GetComponent<IDriverVehicles>();
        SelectVehiclesDetroy _selectVehiclesDetroy = driverVehicles._vehiclesData.selectVehiclesDetroy;
        index = (int)_selectVehiclesDetroy;
        maxHP= driverVehicles._vehiclesData.HP;
        damePoint = driverVehicles._damepoint;
        Init();
    }
   
    public void Init()
    {
        hp = driverVehicles._vehiclesData.HP;
    }
    public void DameLaser(float dame)
    {
        if (DameVehicles(dame))
        {
            ReturnDriver();
        }
    }
    public void DameExplosion(float dame,Vector3 direction)
    {
        if (DameVehicles(dame))
        {
            ReturnDriver();
        }
    }
    public bool DameVehicles(float dame)
    {
        hp -= dame;
        if (hp <= maxHP * VehiclesHPData.ins.rateSmoke)
        {
           
            if (hp <= maxHP * VehiclesHPData.ins.rateFire)
            {
                if (hp <= 0)
                {
                    PoolingManager.ins.vehiclesDetroyPooling.GetVehiclesDetroyedPool(transform.parent.position, transform.forward, index);
                    FxPooling.ins.GetrocketHitPool(transform.position);
                    PoliceStarManager.ins.ChangeWanterPoint(driverVehicles._vehiclesData.wantedPoint);
                    transform.parent.gameObject.SetActive(false);
                    return true;
                }
                if (fireFx != null) return false;
                fireFx = PoolingManager.ins.fxPooling.GetFirePool(damePoint.position);
                fireFx.gameObject.transform.parent = damePoint;
                return false;
            }
            if (smokeFx != null) return false;
            smokeFx=PoolingManager.ins.fxPooling.GetSmokePool(damePoint.position);
            smokeFx.gameObject.transform.parent = damePoint;

        }
        if (driverVehicles._driver != null)
        {
            if (driverVehicles._driver.CompareTag("Player")) return false;
            driverVehicles._driver.GetComponent<NPCDriver>().IsTakeDame();
        }
        return false;



    }
    public void ReturnDriver()
    {
        if (driverVehicles._driver != null)
        {
            if (driverVehicles._driver.CompareTag("Player")) return;
            PoolingManager.ins.pickUpPooling.GetPickUp(transform.position);
            PoolingManager.ins.gameObjectPooling.GetSkeletonPool(transform.position);
            driverVehicles._driver.GetComponent<NPCDriver>().Return();
        }
    }
    public void OnDisable()
    {
        Init();
    }


}
