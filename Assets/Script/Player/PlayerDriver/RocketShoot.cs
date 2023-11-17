using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShoot : MonoBehaviour
{
    public float dame;
    public TrailRenderer trailRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("HitSensor"))
        {
            Debug.LogError(other.gameObject.tag);
            ParticleSystem hit = FxPooling.ins.GetrocketHitPool(transform.position);
            gameObject.SetActive(false);
        }


    }

}
