using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using UnityEngine;
//using static UnityEditor.PlayerSettings;


public class NPCPooling : MonoBehaviour
{
    public static NPCPooling ins;

    [SerializeField]
    private List<GameObject> NPC = new List<GameObject>();

    [SerializeField]
    private List<GameObject> NPCPool = new List<GameObject>();

    [SerializeField]
    private List<PointAIMove> NPCAIMove = new List<PointAIMove>();

    [SerializeField]
    private List<GameObject> NPCPolice;

    [SerializeField]
    private List<GameObject> NPCPolicePool;

    [SerializeField]
    private List<Transform> transformSpawnPoint;

    [SerializeField]
    private int maxPoolNpcStar;

    [SerializeField]
    private int indexNpcStar;

    [SerializeField]
    private List<GameObject> NPCPickup;

    [SerializeField]
    private List<GameObject> NPCPickupPool;

    [SerializeField]
    private int maxNpcPickUp;

    [SerializeField]
    private List<GameObject> NPCQuest;

    [SerializeField]
    private List<GameObject> NPCQuestPool;

    [SerializeField]
    private int maxNpcQuest;

    [SerializeField]
    private int indexNpcQuest;

    [SerializeField]
    private List<GameObject> QuestCar;

    [SerializeField]
    private List<GameObject> QuestCarPool;

    [SerializeField]
    private int maxCarQuest;

    [SerializeField]
    private int indexCarQuest;

    public List<GameObject> Car;

    public List<GameObject> CarPool;

    public void Awake()
    {
        ins = this;
    }
    public void ReStartGame()
    {
        for (int i = 0; i < NPCAIMove.Count; i++)
        {
            NPCAIMove[i].Init();
        }
    }


    public GameObject GetPool(Vector3 pos)
    {
        NPCManager.ins.totalnpc++;

       
        for (int i = 0;i< NPCPool.Count;i++)
        {
            if (!NPCPool[i].activeSelf)
            {
                NPCPool[i].SetActive(true);
                NPCPool[i].transform.position = pos;

                NPCControl NPC = NPCPool[i].GetComponent<NPCControl>();
                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                return NPCPool[i];
            }
        }
        int total = Random.Range(0, NPC.Count);
        if (NPCPool.Count >= NPCManager.ins.maxOnNPC)
        {
            return null;
        }
        GameObject newnpc = Instantiate(NPC[total], pos, Quaternion.identity);

        newnpc.GetComponent<NPCControl>().npcHp.isPolice = false;
        NPCPool.Add(newnpc);
        return newnpc;

    }

    public void ReturnPool(GameObject npc, float time)
    {
       
        StartCoroutine(CouroutineReturnPool(npc, time));
    }
    IEnumerator CouroutineReturnPool(GameObject npc, float time)
    {
        yield return new WaitForSeconds(time);
        NPCManager.ins.totalnpc--;
        npc.transform.parent = null;

        npc.gameObject.GetComponent<Animator>().enabled = true;
        if (npc.GetComponent<NPCDriver>().Vehiclel != null)
        {      
            ReturnCar(npc.GetComponent<NPCDriver>().Vehiclel);
            npc.GetComponent<NPCDriver>().candriver = false;
        }

        npc.SetActive(false);
    }
    //NPCPolice

    public GameObject GetPolice(Vector3 pos)
    {
        SpawnNPCInit.ins.onPolice++;


        for (int i = 0; i < NPCPolicePool.Count; i++)
        {
            if (!NPCPolicePool[i].activeSelf)
            {
                NPCPolicePool[i].SetActive(true);
                NPCPolicePool[i].transform.position = pos;

                NPCControl NPC = NPCPolicePool[i].GetComponent<NPCControl>();
                NPC.npcHp.hp = 100;
                NPC.npcHp.isdead = false;
                
                return NPCPolicePool[i];
                
            }
        }
        int total = Random.Range(0, NPCPolice.Count);
        //if (NPCPolicePool.Count >= NPCManager.ins.maxOnNPC)
        //{
        //    return null;
        //}
        GameObject newnpc = Instantiate(NPCPolice[total], pos, Quaternion.identity);

        newnpc.GetComponent<NPCControl>().npcHp.isPolice = true;
        NPCPool.Add(newnpc);
        return newnpc;

    }

    public void ReturnPolicePool(GameObject npc, float time)
    {
        StartCoroutine(CouroutineReturnPolicePool(npc, time));
    }
    IEnumerator CouroutineReturnPolicePool(GameObject npc, float time)
    {
        yield return new WaitForSeconds(time);
        SpawnNPCInit.ins.onPolice++;
        npc.transform.parent = null;

        npc.gameObject.GetComponent<Animator>().enabled = true;
        NPCControl NPC = npc.GetComponent<NPCControl>();
        NPC.npcState.ChangeState(SelectState.Move);
        NPC.pointtarget = NPC.lastpoint;
        npc.SetActive(false);
    }

    public void StartPoliceWanter()
    {
        if (NPCPolicePool.Count != 0)
        {
            for (int i = 0; i < NPCPolicePool.Count; i++)
            {
                if (NPCPolicePool[i].activeSelf == true && NPCPolicePool[i].GetComponent<NPCState>().currentState != SelectState.Attack)
                {
                    NPCPolicePool[i].GetComponent<NPCState>().ChangeState(SelectState.Attack);
                }

            }
        }
    }

    public void EndStarPolice()
    {
        if (NPCPolicePool.Count != 0)
        {
            for (int i = 0; i < NPCPolicePool.Count; i++)
            {
                NPCControl NPC = NPCPolicePool[i].GetComponent<NPCControl>();
                NPC.npcState.ChangeState(SelectState.Move);
                NPC.pointtarget = NPC.lastpoint;

            }
        }
    }


    // Pickup
    public void SpawnPickUp(Vector3 posPickup, bool _isMoney = false)
    {
        if (_isMoney == false)
        {
            if (NPCPickupPool.Count >= maxNpcPickUp)
            {
                for (int i = 0; i < NPCPickupPool.Count; i++)
                {
                    if (NPCPickupPool[i].activeSelf == false)
                    {
                        NPCPickupPool[i].SetActive(true);

                        NPCPickupPool[i].transform.position = posPickup;
                        return;
                    }
                }
            }
            else
            {
                int index = Random.Range(1, 3);
                GameObject pickupNew = Instantiate(NPCPickup[index], posPickup, Quaternion.identity);

                NPCPickupPool.Add(pickupNew);
            }
        }

        else
        {
            if (NPCPickupPool.Count >= maxNpcPickUp)
            {
                for (int i = 0; i < NPCPickupPool.Count; i++)
                {
                    if (NPCPickupPool[i].activeSelf == false && NPCPickupPool[i].GetComponent<MoneyPickup>() != null)
                    {
                        NPCPickupPool[i].SetActive(true);

                        NPCPickupPool[i].transform.position = posPickup;
                        return;
                    }
                }
            }
            else
            {
                GameObject pickupNew = Instantiate(NPCPickup[0], posPickup, Quaternion.identity);

                NPCPickupPool.Add(pickupNew);
            }
        }
    }

    //NPCQuest
    

    //CarQuest


    //Car
    public GameObject SpawnCar(Vector3 pos)
    {
     
        if (SpawnNPCInit.ins.isSpawnVehicles)
        {
            GameObject newnpc = Instantiate(Car[0], pos, Quaternion.identity);
            Debug.LogError("Check");
            SpawnNPCInit.ins.Vehicles(1);
            CarPool.Add(newnpc);
        
            SpawnNPCInit.ins.onVehicles += 1;

            return newnpc;
        }
        else
        {    
            return ResetCar(pos);
        }
    }

    public void ReturnCar(GameObject Car)
    {
     
        Car.SetActive(false);
        SpawnNPCInit.ins.onVehicles -= 1;
        Car.GetComponent<Car>().npcDrive = null;

    }

    public GameObject ResetCar(Vector3 pos)
    {
     
        for (int i = 0; i < CarPool.Count; i++)
        {
          
            if (CarPool[i].GetComponent<Car>().npcDrive == null)
            {           
                CarPool[i].SetActive(true);
                CarPool[i].transform.position = pos;
                CarPool[i].GetComponent<Car>().vehiclesHp.Init();
                SpawnNPCInit.ins.onVehicles += 1;
                return CarPool[i];
            }
        }
        

        return null;
    }
    //Check

    public void CheckPlayerDead()
    {
        for (int i = 0; i < NPCPool.Count; i++)
        {
            if (NPCPool[i].GetComponent<NPCControl>().npcState.currentState == SelectState.Attack)
            {
                NPCPool[i].GetComponent<NPCControl>().npcState.ChangeState(SelectState.Move);
                NPCPool[i].GetComponent<NPCControl>().pointtarget = NPCPool[i].GetComponent<NPCControl>().lastpoint;
            }
            
        }
        if (NPCPolicePool.Count != 0)
        {
            PoliceStarManager.ins.LoseWanterPoint();
            for (int i = 0; i < NPCPolicePool.Count; i++)
            {
                ReturnPolicePool(NPCPolicePool[i],0);
            }
        }


    }


    

}
