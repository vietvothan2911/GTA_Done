using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.UI;

public class PoliceStarManager : MonoBehaviour
{
    public static PoliceStarManager ins;

    [SerializeField]
    private List<Image> images;

    [SerializeField]
    public int indexWanter;
    [SerializeField]
    private float wanterPoint;

    [SerializeField]
    private List<bool> isPoliceSpawns;

    public bool isQuest;

    [SerializeField]
    private List<int> timeLoseWanter;

    private float timeWanter;

    [SerializeField]
    private List<GameObject> police;

    public void Awake()
    {
        ins = this;
    }
    public void Start()
    {
        indexWanter = 0;
        wanterPoint = 0;
        ChangeStarWanter(0);
        ReStartWanter();
    }

    public void AddPolice(GameObject _police)
    {
        police.Add(_police);
    }

    public void ChangeWanterPoint(float _wanterPoint)
    {
        

        if (isQuest == false)
        {
            timeWanter = Time.time;
            wanterPoint += _wanterPoint;
            if (wanterPoint <= 400)
            {
                if (wanterPoint >= 10 && wanterPoint < 50)
                {
                   
                    if (isPoliceSpawns[0] == false)
                    {
                  
                        ChangeStarWanter(1);
                        timeWanter = timeLoseWanter[IndexWanter()];
                        isPoliceSpawns[0] = true;
                        
                    }
                    
                }
                else if (wanterPoint >= 50 && wanterPoint < 125)
                {
                    if (isPoliceSpawns[1] == false)
                    {
                        timeWanter = timeLoseWanter[IndexWanter()];
                        ChangeStarWanter(2);
                        isPoliceSpawns[1] = true;
                    }
                    
                }
                else if (wanterPoint >= 125 && wanterPoint < 300)
                {
                    if (isPoliceSpawns[2] == false)
                    {
                        timeWanter = timeLoseWanter[IndexWanter()];
                        ChangeStarWanter(3);
                        isPoliceSpawns[2] = true;
                    }
                    
                }
                else if (wanterPoint >= 300 && wanterPoint < 400)
                {
                    if (isPoliceSpawns[3] == false)
                    {
                        timeWanter = timeLoseWanter[IndexWanter()];
                        ChangeStarWanter(4);
                        isPoliceSpawns[3] = true;
                    }
                    
                }
                else if (wanterPoint >= 400)
                {
                    if (isPoliceSpawns[4] == false)
                    {
                        timeWanter = timeLoseWanter[IndexWanter()];
                        ChangeStarWanter(5);
                        isPoliceSpawns[4] = true;
                    }
                    wanterPoint = 500;
                }
            }
        }
    }

    public int IndexWanter()
    {
        return indexWanter;
    }

    public void ChangeStarWanter(int indexStar)
    {
        indexWanter = indexStar;

        for (int i = 0; i < images.Count; i++)
        {
            if (i < indexWanter)
            {
                images[i].fillAmount = 1;
            }
            else
            {
                images[i].fillAmount = 0;
            }
        }
        if (indexWanter == 0) return;
        PoolingManager.ins.policePooling.StartPoliceWanter();
        if (indexWanter == 1)
        {
            //SpawnNPCInit.ins.maxPolice = 3;
            PoolingManager.ins.policePooling.maxNpcMove = 2;

        }
        else if (indexWanter == 2)
        {
            //SpawnNPCInit.ins.maxPolice = 4;
            PoolingManager.ins.policePooling.maxNpcMove = 3;
        }
        else if (indexWanter == 3)
        {
            //SpawnNPCInit.ins.maxPolice = 5;
            PoolingManager.ins.policePooling.maxNpcMove = 3;
            PoolingManager.ins.policePooling.maxNpcDriver = 1;
        }
        else if (indexWanter == 4)
        {
            //SpawnNPCInit.ins.maxPolice = 6;
            PoolingManager.ins.policePooling.maxNpcMove = 4;
            PoolingManager.ins.policePooling.maxNpcDriver = 2;

        }
        else if (indexWanter == 5)
        {
            //SpawnNPCInit.ins.maxPolice = 7;
            PoolingManager.ins.policePooling.maxNpcMove = 5;
            PoolingManager.ins.policePooling.maxNpcDriver = 3;
        }
        else
        {
            //SpawnNPCInit.ins.maxPolice = 3;
            PoolingManager.ins.policePooling.maxNpcMove = 2;
            PoolingManager.ins.policePooling.maxNpcDriver = 0;
        }
    }

    public void LoseWanterPoint()
    {
        indexWanter = 0;
        wanterPoint = 0;
        ChangeStarWanter(0);
        //npcPooling.EndStarPolice();
        for (int i = 0; i < isPoliceSpawns.Count; i++)
        {
            isPoliceSpawns[i] = false;
        }
    }


    public void ReStartWanter()
    {
        if (IndexWanter() != 0)
        {
            bool iswanter = true;
            for(int i = 0;i < police.Count; i++)
            {
                if (Vector3.Distance(police[i].transform.position, Player.ins.transform.position) <= 40)
                {
                   iswanter = false;
                }
            }
            if (iswanter)
            {
                timeWanter -= 1f;
                if (timeWanter <= 0f)
                {
                    LoseWanterPoint();
                }
            }
           
        }
        Invoke("ReStartWanter", 1f);
    }


}
