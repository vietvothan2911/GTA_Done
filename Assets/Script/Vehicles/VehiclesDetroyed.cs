using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesDetroyed : MonoBehaviour
{
    public List<Rigidbody> rigidbodiesComponent = new List<Rigidbody>();
    public List<Vector3> originalTransform = new List<Vector3>();
    public float force=10;
    public float timereturn;
    private void Start()
    {
        foreach (var rb in rigidbodiesComponent)
        {
            originalTransform.Add(rb.gameObject.transform.position);

        }
    }
    private void OnEnable()
    {
       
        foreach ( var rb in rigidbodiesComponent)
        {
            rb.AddForce((rb.gameObject.transform.position - transform.position).normalized *rb.mass* force);
           
        }
        Return();
    }

    public void Return()
    {
        StartCoroutine(CouroutineReturn());
    }
    IEnumerator CouroutineReturn()
    {
        yield return new WaitForSeconds(timereturn);
        SetPosition();
        gameObject.SetActive(false);
       
    }
    public void SetPosition()
    {
        for(int i=0; i < rigidbodiesComponent.Count; i++)
        {
            rigidbodiesComponent[i].velocity = Vector3.zero;
            rigidbodiesComponent[i].gameObject.transform.position = originalTransform[i];

        }
    }
}
