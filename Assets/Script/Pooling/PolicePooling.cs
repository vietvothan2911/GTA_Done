using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePooling : CivilianPooling
{
    public override GameObject GetPoolMove(Vector3 pos, Transform nextpoint, Transform lastpoint)
    {
        GameObject newNpc = GetPool(pos, NPCPoolMove, maxNpcMove);
        if (newNpc == null) return null;
        NPCControl npcControl = newNpc.GetComponent<NPCControl>();
        npcControl.pointtarget = nextpoint;
        if (PoliceStarManager.ins.indexWanter > 0)
        {
            npcControl.npcState.ChangeState(SelectState.Attack);
        }
        else
        {
            npcControl.npcState.ChangeState(SelectState.Move);
        }
        newNpc.transform.LookAt(nextpoint);
        npcControl.lastpoint = lastpoint;
        return newNpc;
    }

    public override GameObject GetPoolDriver(Vector3 pos, Transform nextpoint, Transform lastpoint)
    {

        GameObject newNpc = GetPool(pos, NPCPoolDriver, maxNpcDriver);
        if (newNpc == null) return null;
        NPCControl npcControl = newNpc.GetComponent<NPCControl>();
        npcControl.pointtarget = nextpoint;
        npcControl.npcState.ChangeState(SelectState.Driver);
        if (PoliceStarManager.ins.indexWanter > 0)
        {
            npcControl.npcDriver.driverType = DriverType.Pursue;
        }
        newNpc.transform.LookAt(nextpoint);
        npcControl.lastpoint = lastpoint;
        return newNpc;
    }
    public override GameObject GetPool(Vector3 pos, List<GameObject> NPCPool, float maxNpc)
    {
        foreach (var npc in NPCPool)
        {
            if (!npc.activeSelf)
            {
                npc.SetActive(true);
                npc.transform.position = pos;

                NPCControl NPC = npc.GetComponent<NPCControl>();
                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                return npc;
            }
        }
        RandomNPC();
        if (NPCPool.Count >= maxNpc)
        {
            return null;
        }

        GameObject newnpc = Instantiate(Npc[index], pos, Quaternion.identity);
        PoliceStarManager.ins.AddPolice(newnpc);
        newnpc.GetComponent<NPCControl>().isPolice = true;
        NPCPool.Add(newnpc);
        return newnpc;
    }
    public void StartPoliceWanter()
    {

        if (NPCPoolMove.Count != 0)
        {
            for (int i = 0; i < NPCPoolMove.Count; i++)
            {
                if (NPCPoolMove[i].activeSelf && NPCPoolMove[i].GetComponent<NPCState>().currentState != SelectState.Attack)
                {
                    NPCPoolMove[i].GetComponent<NPCState>().ChangeState(SelectState.Attack);
                }

            }

            if (NPCPoolDriver.Count != 0)
            {
                for (int i = 0; i < NPCPoolDriver.Count; i++)
                {
                    if (NPCPoolDriver[i].activeSelf  && NPCPoolDriver[i].GetComponent<NPCState>().currentState == SelectState.Driver)
                    {
                        NPCPoolDriver[i].GetComponent<NPCControl>().npcDriver.driverType = DriverType.Pursue;
                    }

                }
            }
        }
    }
}
