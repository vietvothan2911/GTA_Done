using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHP : MonoBehaviour, IDameExplosion,IDameLaser
{
    public NPCState npcState;
    public GameObject NPC;
    public NPCControl npcControl;

    public float hp = 100;

  

    public bool isNpcQuest;

    public bool isdead;

    public int bonus, wanterPoint;

    [SerializeField]
    private GameObject ragdollNPC, ragdollNPCNow;

    [SerializeField]
    private Rigidbody rb;
    public void DameLaser(float dame)
    {
        if (npcControl.npcState.currentState == SelectState.Driver) return;
        hp -= dame;
        if (hp > 0)
        {

            if (npcState.currentState != SelectState.Attack)
            {
                if (npcState.currentState == SelectState.Driver)
                {
                    npcControl.animator.Play("Grounder");
                    npcControl.npcDriver.EndDriver();
                    npcControl.npcDriver.Vehicle = null;
                }
                npcState.ChangeState(SelectState.Attack);

            }


        }
        else
        {
            PoolingManager.ins.gameObjectPooling.GetSkeletonPool(transform.position);
            PoolingManager.ins.pickUpPooling.GetPickUp(transform.position);
            PoliceStarManager.ins.ChangeWanterPoint(wanterPoint);
            NPC.SetActive(false);
            
        }
    }
    public void DameExplosion(float dame,Vector3 direction)
    {
        if (npcControl.npcState.currentState == SelectState.Driver) return;
        hp -= dame;
        if (hp > 0)
        {

            if (npcState.currentState != SelectState.Attack)
            {
                if (npcState.currentState == SelectState.Driver)
                {
                    npcControl.animator.Play("Grounder");
                    npcControl.npcDriver.EndDriver();
                    npcControl.npcDriver.Vehicle = null;
                }
                npcState.ChangeState(SelectState.Attack);

            }


        }
        else
        {
            PoolingManager.ins.gameObjectPooling.GetSkeletonPool(transform.position);
            PoolingManager.ins.pickUpPooling.GetPickUp(transform.position);
            PoliceStarManager.ins.ChangeWanterPoint(wanterPoint);
            NPC.SetActive(false);
        }
    }
    public void HitDame(float dame, Vector3 pos, float powerRagdoll = 0, bool isRagdoll = false)
    {
        hp -= dame;

        if (hp >= 0)
        {

            if (npcState.currentState != SelectState.Attack)
            {
                if (npcState.currentState == SelectState.Driver)
                {
                    npcControl.animator.Play("Grounder");
                    npcControl.npcDriver.EndDriver();
                    //npcControl.npcDriver.Vehiclel.GetComponent<Car>().npcDrive = null;
                    npcControl.npcDriver.Vehicle = null;
                    //npcControl.npcDriver.runaway = false;
                }
                npcState.ChangeState(SelectState.Attack);

            }


        }
        else if (isdead == false && hp < 0)
        {
            npcControl.animator.enabled = false;

            npcControl.npcDriver.Vehicle = null;
            PoliceStarManager.ins.ChangeWanterPoint(wanterPoint);


            OnRagdoll(new Vector3(4, -4, 4), powerRagdoll, true);
            Invoke("NPCDead", 2.5f);
            isdead = true;
        }
        if (isRagdoll && isdead == false)
        {
            OnRagdoll(pos, powerRagdoll);
        }
    }
    public void NPCDead()
    {
        //if (npcControl.isPolice)
        //{
        //    NPCManager.ins.npcPooling.ReturnPolicePool(NPC,2f);
        //}
        //else
        //{
        //    NPCManager.ins.npcPooling.ReturnPool(NPC, 2f);
        //}

        //float x = Random.Range(transform.position.x - 1, transform.position.x + 1);
        //float z = Random.Range(transform.position.z - 1, transform.position.z + 1);

        //Vector3 pos = new Vector3(x, transform.position.y, z);

        //NPCManager.ins.npcPooling.SpawnPickUp(pos);
        //NPCManager.ins.npcPooling.SpawnPickUp(pos, true);
    }


    public void OnRagdoll(Vector3 pos, float power = 0, bool isDie = false)
    {

        NPC.SetActive(false);


        ragdollNPCNow = Instantiate(ragdollNPC, transform.parent.position, transform.parent.rotation);


        ragdollNPCNow.GetComponent<RagDoll>().OnRagDoll(transform.parent.gameObject, pos * power, isDie);
    }


}
