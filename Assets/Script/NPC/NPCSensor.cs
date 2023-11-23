using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSensor : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    public NPCControl npcControl;
    public bool collisionwithplayer;
    public bool collisionwithobject;
    public bool collisionwithtrafficlight;
    public GameObject objcollisionwithhuman;
    public GameObject objcollisionwithobject;
    public GameObject objcollisionwithtrafficlight;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
           
            collisionwithplayer = true;
            ExitCollisionWithPlayer();

        }
        if (GameManager.ins.layerData.Trafficlight == (GameManager.ins.layerData.Trafficlight | (1 << other.gameObject.layer)))
        {
            collisionwithtrafficlight = true;
            objcollisionwithtrafficlight = other.gameObject;
            Invoke("CheckExitTrafficlight", 2);
        }
        if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << other.gameObject.layer)))
        {
            collisionwithobject = true;
            objcollisionwithobject = other.gameObject.transform.parent.gameObject;
            CreatObstacle(other.gameObject);
        }
    }
    public void ExitCollisionWithPlayer()
    {
        StartCoroutine(CouroutineExitCollisionWithPlayer(5));
    }
    IEnumerator CouroutineExitCollisionWithPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        collisionwithplayer = false;

    }
    public void CreatObstacle(GameObject other)
    {
        if (other.gameObject.GetComponent<NavMeshObstacle>() != null)
        {
            obstacle = other.gameObject.GetComponent<NavMeshObstacle>();
            obstacle.enabled = true;
        }
        else
        {
            obstacle = other.gameObject.AddComponent<NavMeshObstacle>();
            obstacle.carving = true;
            obstacle.shape = NavMeshObstacleShape.Capsule;
        }

        StartCoroutine(CouroutineTurnOffObstacle());
    }
    IEnumerator CouroutineTurnOffObstacle()
    {
        yield return new WaitForSeconds(3f);
        obstacle.enabled = false;

    }
    public void CheckExitTrafficlight()
    {
        if (objcollisionwithtrafficlight.activeSelf)
        {
            Invoke("CheckExitTrafficlight", 2);
        }
        else
        {
            collisionwithtrafficlight = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {

         if (GameManager.ins.layerData.ObstacleLayer == (GameManager.ins.layerData.ObstacleLayer | (1 << other.gameObject.layer)))
        {
            collisionwithobject = false;
        }
        else if (GameManager.ins.layerData.Trafficlight == (GameManager.ins.layerData.Trafficlight | (1 << other.gameObject.layer)))
        {
            collisionwithtrafficlight = false;
        }

    }

}
