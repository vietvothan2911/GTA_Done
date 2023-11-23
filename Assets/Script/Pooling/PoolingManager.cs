using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager ins;
    public FxPooling fxPooling;
    public BulletPooling bulletPooling;
    public FxShotPooling fxShotPooling;
    public QuestPooling questPooling;
    public CivilianPooling civilianPooling;
    public VehiclesPolling vehiclesPolling;
    public VehiclesDetroyPooling vehiclesDetroyPooling;
    public PolicePooling policePooling;
    public VehiclesPolicePooling vehiclesPolicePooling;
    public GameObjectPooling gameObjectPooling;
    public PickUpPooling pickUpPooling;
    void Awake()
    {
        ins = this;
    }
}
