using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAiPolice : MonoBehaviour
{
    public List<Transform> nextpoint;
    public NPCPooling npcPooling;
    public float maxdistance;
    public float mindistance;
    public float mintime;
    public float maxtime;
    private float time;
    public Transform _nextpoint;


    public void Init()
    {
        _nextpoint = RandomNextPoint();
        time = Random.Range(mintime, maxtime);

        StartCoroutine(SpawnNPCCouroutine());
    }

    public Transform RandomNextPoint()
    {
        if (nextpoint.Count > 0)
        {

            int randomIndex = Random.Range(0, nextpoint.Count);


            return nextpoint[randomIndex];
        }
        else
        {

            return null;
        }
    }
    public IEnumerator SpawnNPCCouroutine()
    {
        yield return new WaitForSeconds(time);

        float dis = Vector3.Distance(transform.position, Player.ins.transform.position);
        time = Random.Range(mintime, maxtime);

        if (dis < mindistance || dis > maxdistance || !SpawnNPCInit.ins.CheckCanSpawnPolice())
        {
            StartCoroutine(SpawnNPCCouroutine());
            yield break;
        }
        GameObject newnpc = npcPooling.GetPolice(transform.position);

        if (newnpc == null)
        {
            StartCoroutine(SpawnNPCCouroutine());
            yield break;

        }

        NPCControl npcControl = newnpc.GetComponent<NPCControl>();

        npcControl.pointtarget = RandomNextPoint();
        newnpc.transform.LookAt(_nextpoint);
        npcControl.lastpoint = transform;
        npcControl.npcState.ChangeState(SelectState.Move);

        StartCoroutine(SpawnNPCCouroutine());
        yield break;

    }
}
