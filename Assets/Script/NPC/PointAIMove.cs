using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAIMove : MonoBehaviour
{

    public SelectState currentState;
    public List<Transform> nextpoint;
    private float time;
    public Transform _nextpoint;
    public bool canSpawn;

    private void OnEnable()
    {
      
        time = Random.Range(1, 3);
        Invoke("SpawnNpc", time);
      
    }
   
    public void SpawnNpc()
    {
        if (!canSpawn) return;
        Transform nextpoint = RandomNextPoint();
        if (gameObject.activeSelf)
        {
            StartCoroutine(ContinueSpawn(SpawnNpcManager.ins.timedelay));
        }
       
        float dis = Vector3.Distance(transform.position, Player.ins.transform.position);
        if (CheckObstruction()) return;
        if (dis < SpawnNpcManager.ins.mindistance || dis > SpawnNpcManager.ins.maxdistance) return;
        
        if (currentState == SelectState.Move)
        {
           
            if(PoolingManager.ins.policePooling.GetPoolMove(transform.position, nextpoint, transform) == null)
            {
                PoolingManager.ins.civilianPooling.GetPoolMove(transform.position, nextpoint, transform);
            }




        }
        else if (currentState == SelectState.Driver)
        {
            
            if (PoolingManager.ins.policePooling.GetPoolDriver(transform.position, nextpoint, transform) == null)
            {
                PoolingManager.ins.civilianPooling.GetPoolDriver(transform.position, nextpoint, transform);
            }
        }
    }
    IEnumerator ContinueSpawn(float time )
    {
        yield return new WaitForSeconds(time);
            SpawnNpc();
       
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public bool CheckObstruction()
    {
        bool isObstruction = Physics.CheckSphere(transform.position, 3f, GameManager.ins.layerData.VehiclesLayer);
        return isObstruction;
    }

    public Transform RandomNextPoint(Transform lastpoint=null)
    {
       
        if (nextpoint.Count > 1)
        {
            int randomIndex = Random.Range(0, nextpoint.Count);
            if (nextpoint[randomIndex] == lastpoint)
            {
                List<Transform> newnextpoint = nextpoint;
                newnextpoint.RemoveAt(randomIndex);
                return newnextpoint[0];
            }
           
            return nextpoint[randomIndex];
        }
        else
        {

            return nextpoint[0];
        }
    }
  
}
public enum SelectPoint
{
    Walk,
    Drive
}