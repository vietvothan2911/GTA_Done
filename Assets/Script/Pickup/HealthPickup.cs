using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.ins.playerHP.PlusHp(40);

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
