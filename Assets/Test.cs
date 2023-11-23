using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public float dis;
    private void Update()
    {
        dis = Vector3.Distance(transform.position, Player.ins.transform.position);
    }
}
