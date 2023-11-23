using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using UnityEngine;
//using static UnityEditor.PlayerSettings;


public class QuestPooling : MonoBehaviour
{

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


}
