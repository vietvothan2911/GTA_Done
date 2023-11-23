using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPooling : MonoBehaviour
{
    public static FxPooling ins;
    public ParticleSystem _muzzleFlash;
    [SerializeField]
    private List<ParticleSystem> muzzleFlashPool = new List<ParticleSystem>();
    public ParticleSystem _muzzleFlash_2;
    [SerializeField]
    private List<ParticleSystem> muzzleFlashPool_2 = new List<ParticleSystem>();
    public ParticleSystem _metallHitEffect;
    [SerializeField]
    private List<ParticleSystem> metallHitEffectPool = new List<ParticleSystem>();
    public ParticleSystem _stoneHitEffect;
    [SerializeField]
    private List<ParticleSystem> stoneHitEffectPool = new List<ParticleSystem>();
    public ParticleSystem _woodHitEffect;
    [SerializeField]
    private List<ParticleSystem> woodHitEffectPool = new List<ParticleSystem>();
    public ParticleSystem _bloodEffect;
    [SerializeField]
    private List<ParticleSystem> bloodEffectPool = new List<ParticleSystem>();
    public ParticleSystem _rocketHit;
    [SerializeField]
    private List<ParticleSystem> rocketHitPool = new List<ParticleSystem>();
    public ParticleSystem _smoke;
    [SerializeField]
    private List<ParticleSystem> smokePool = new List<ParticleSystem>();
    public ParticleSystem _fire;
    [SerializeField]
    private List<ParticleSystem> firePool = new List<ParticleSystem>();
    private void Start()
    {
        ins = this;
    }
    public ParticleSystem GetSmokePool(Vector3 pos)
    {
        return GetPool(pos, _smoke, smokePool);
    }
    public ParticleSystem GetFirePool(Vector3 pos)
    {
        return GetPool(pos, _fire, firePool);
    }
    public ParticleSystem GetrocketHitPool(Vector3 pos)
    {
        ParticleSystem newparticle = GetPool(pos, _rocketHit, rocketHitPool);
        ReturnPool(newparticle, 4f);
        return newparticle;
    }
    public ParticleSystem GetwoodHitEffectPool(Vector3 pos)
    {
        ParticleSystem newparticle = GetPool(pos, _woodHitEffect, woodHitEffectPool);
        ReturnPool(newparticle, 4f);
        return newparticle;
        
    }
    public ParticleSystem GetbloodEffectPool(Vector3 pos)
    {
        ParticleSystem newparticle = GetPool(pos, _bloodEffect, bloodEffectPool);
        ReturnPool(newparticle, 4f);
        return newparticle;
   
    }
    public ParticleSystem GetmuzzleFlashPool(Vector3 pos)
    {
        ParticleSystem newparticle = GetPool(pos, _muzzleFlash, muzzleFlashPool);
        ReturnPool(newparticle, 4f);
        return newparticle;
      
    }
    public ParticleSystem GetmuzzleFlashPool_2(Vector3 pos)
    {

        ParticleSystem newparticle = GetPool(pos, _muzzleFlash_2, muzzleFlashPool_2);
        ReturnPool(newparticle, 4f);
        return newparticle;
       
    }
    public ParticleSystem GetmetallHitEffectPool(Vector3 pos)
    {
        ParticleSystem newparticle = GetPool(pos, _metallHitEffect, metallHitEffectPool);
        ReturnPool(newparticle, 4f);
        return newparticle;
       
    }
    public ParticleSystem GetstoneHitEffectPool(Vector3 pos)
    {
        ParticleSystem newparticle = GetPool(pos, _stoneHitEffect, stoneHitEffectPool);
        ReturnPool(newparticle, 4f);
        return newparticle;

    }
    public ParticleSystem GetPool(Vector3 pos, ParticleSystem particleSystem, List<ParticleSystem> Pool)
    {
        foreach (var particle in Pool)
        {

            if (!particle.gameObject.activeInHierarchy)
            {
                particle.gameObject.SetActive(true);
                particle.gameObject.transform.position = pos;
                particle.gameObject.transform.rotation = Quaternion.identity;
                return particle;
            }
        }
        ParticleSystem newparticle = Instantiate(particleSystem, pos, Quaternion.identity);
        Pool.Add(newparticle);
      
        return newparticle;
    }

    public void ReturnPool(ParticleSystem particleSystem, float time)
    {
        StartCoroutine(CouroutineReturnPool(particleSystem, time));
    }
    IEnumerator CouroutineReturnPool(ParticleSystem particleSystem, float time)
    {
        yield return new WaitForSeconds(time);
        particleSystem.gameObject.SetActive(false);

    }
}
