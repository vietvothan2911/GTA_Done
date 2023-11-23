using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{

    [Header("Control")] 
    public NPCControl npcControl;
    public float speedRotate; 
    public float forward;
    public bool oncollision;
    public bool ismove;
    public bool canmove;
    [Header("OnGround")]
    [SerializeField] Transform surfaceCheck;
    [SerializeField] float surfaceDistance;
    public bool onSurface; 
    public LayerMask surfaceMask;
  
   

    public void Move(float _forward)
    {
        forward = _forward;
        npcControl.animator.SetFloat("Forward", forward);
        npcControl.animator.Play("Grounded");
        if (gameObject.activeSelf)
        {
            StartCoroutine(MoveCourotine());
        }
        
    }
    public void Idle()
    {

        forward = 0;
        npcControl.animator.SetFloat("Forward", forward);
        npcControl.navMeshAgent.speed = 0;
        StartCoroutine(IdleCourotine());
    }
    public void CheckOnGround()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
    }
    IEnumerator IdleCourotine()
    {
        yield return new WaitForSeconds(5f);
      
        NextState(100);
    }
  
    IEnumerator MoveCourotine()
    {
       
        while (canmove)
        {
           
            npcControl.navMeshAgent.enabled = true;
           
            npcControl.animator.applyRootMotion = false;
         
             bool isGrounded= npcControl.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded") ;
            if (isGrounded&&!CheckCollision())
            {
                Quaternion targetRotation = Quaternion.LookRotation(npcControl.navMeshAgent.velocity.normalized);
                transform.rotation = targetRotation;
                ismove = true;
                npcControl.animator.SetFloat("Forward", forward);
                if (forward == 0.5)
                {
                    npcControl.navMeshAgent.speed = 1.5f;
                }
                else
                {
                    npcControl.navMeshAgent.speed = 4f;
                }
               
            }
            else
            {
                ismove = false;
                npcControl.animator.SetFloat("Forward", 0);
                npcControl.navMeshAgent.speed = 0;
            }
            if (npcControl.pointtarget != null)
            {
                npcControl.navMeshAgent.SetDestination(npcControl.pointtarget.position);
            }
          
            if (Vector3.Distance(transform.position, npcControl.pointtarget.position) < 1f)
            {
                NextState(75);
            }
           
            yield return new WaitForSeconds(npcControl.timedelay);
        }
        ismove = false;
        npcControl.animator.SetFloat("Forward", 0);
        npcControl.navMeshAgent.speed = 0;
    }


    public void NextState(int rate)
    {
        if (Vector3.Distance(transform.position, Player.ins.transform.position) >= 150)
        {
            gameObject.SetActive(false);
           

            return;
        }
        if (npcControl.pointtarget.gameObject.GetComponent<PointAIMove>() != null)
        {
            int i = Random.Range(0, 101);
    
            if ( i <= rate)
            {
                Transform currentpoint = npcControl.pointtarget;
                npcControl.pointtarget = npcControl.pointtarget.gameObject.GetComponent<PointAIMove>().RandomNextPoint(npcControl.lastpoint);
                npcControl.lastpoint = currentpoint;
                npcControl.npcState.ChangeState(SelectState.Move);
            }
            else
            {
                npcControl.npcState.ChangeState(SelectState.Idle);
            }
        }
    }
    public bool CheckCollision()
    {
        if (npcControl.npcSensor.collisionwithplayer)
        {
          
            return true;
        }
        if (npcControl.npcSensor.collisionwithtrafficlight)
        {
           
            return true;
        }
        return false;

        
    }
 
}
