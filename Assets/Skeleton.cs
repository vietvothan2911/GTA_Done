using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Rigidbody rb;
    public float foce=10;
    public float timeReturn=5;
    private void OnEnable()
    {
        rb.AddForce(transform.up * foce);
        StartCoroutine(CouroutineReturn());
    }
    IEnumerator CouroutineReturn()
    {
        yield return new WaitForSeconds(timeReturn);
        gameObject.SetActive(false);
    }
}
