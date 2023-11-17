using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGun : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public WeaponData WeaponData;
    public Transform muzzle;
    private ParticleSystem flash;
    public float fireRate = 0;
    public float startTime;
    public float offset;
    Ray ray;
    [SerializeField]
    private NPCControl npc;

    private void Start()
    {

        startTime = WeaponData._startTime;
    }
    public void StartShooting(NPCControl _npc)
    {
        npc = _npc;

        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            return;
        }
        else
        {
            //Invoke("Shooting", 0.1f);
            Shooting();
        }


    }
    public void Shooting()
    {

        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
            Player.ins.animator.SetBool("RangedWeaponShoot", false);
            return;
        }
        if (fireRate <= 0)
        {
            
            fireRate = 1f / WeaponData._fireRate;
            flash = FxPooling.ins.GetmuzzleFlashPool(muzzle.position);
            flash.gameObject.transform.parent = muzzle;
            flash.gameObject.transform.localRotation = Quaternion.identity;
            flash.gameObject.transform.localScale = Vector3.one;
            FxPooling.ins.ReturnPool(flash, 0.5f);


            //var bullet = Instantiate(WeaponData._bullet, pos, Quaternion.identity);
            Vector3 pos = muzzle.position;
            var bullet = FxShotPooling.ins.GetGunPool(pos);
            Vector3 randomOffset = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0f);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 direction = (Player.ins.transform.position - pos + Vector3.up * 1.25f).normalized + randomOffset;
            //HitCheck(direction);

            bullet.GetComponent<Rigidbody>().AddForce(direction * WeaponData._power);

            Player.ins.animator.SetBool("RangedWeaponShoot", true);
            //for (int i = 0; i < 6; i++) 
            //{
            //    Invoke("MaChineGun", i * 0.1f);
            //}
           

        }
    }

    public void MaChineGun()
    {
       
    } 
    public void HitCheck(Vector3 direction)
    {
        ray.origin = muzzle.transform.position;
        ray.direction = direction;

        LayerData layerData = GameManager.ins.layerData;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f))
        {
          
            if (layerData.HumanLayer == (layerData.HumanLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetbloodEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                Player.ins.playerHP.OnHit(HitDameState.Weapon, false, npc.chacractorData.rangeDame, Vector3.zero);

            }

            if (layerData.MetalLayer == (layerData.MetalLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetmetallHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
               
            }

            if (layerData.StoneLayer == (layerData.StoneLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetstoneHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
               
             
            }

             if (layerData.WoodLayer == (layerData.WoodLayer | (1 << raycastHit.collider.gameObject.layer)))
            {
                hitEffect = FxPooling.ins.GetwoodHitEffectPool(raycastHit.point);
                hitEffect.gameObject.transform.LookAt(muzzle);
                
              
            }

        }
    }
    public void FinishShooting()
    {
        startTime = WeaponData._startTime;
        fireRate -= Time.deltaTime;
    }
}
