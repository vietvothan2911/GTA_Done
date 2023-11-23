using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{

  
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ItemManager.ins.AddMoney(Random.Range(15,36));


            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(CouroutineReturn());
    }
    IEnumerator CouroutineReturn()
    {
        yield return new WaitForSeconds(30);
        gameObject.SetActive(false);
        
    }
}
