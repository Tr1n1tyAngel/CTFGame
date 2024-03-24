using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIFSM : MonoBehaviour
{
    public FlagPickup flagPickup;
    public FlagPickup pFlagPickup;
    public FlagReturn flagReturn;
    public float tempSpeed;
    public NavMeshAgent agent;
    public Scoreboard scoreboard;
    public GameObject bullet;
    public GameObject bulletRadius;
    public GameObject player;
    public float shotForce = 10f;
    private bool canShoot = true;
    public float dangerDistance = 5.0f;
    private Vector3 evadeDirection;
    public float distanceToBullet;
    public bool bulletRange=false;
    public enum AIState
    {
        Capturing,
        Returning,
        GetPoint,
        EvadeP,
        EvadeB,
        Chase
        
    }

    
    private AIState currentState;

    void Start()
    {
        tempSpeed = agent.speed;
        SetState(AIState.Capturing);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (pFlagPickup.pFlag==true)
        {
            SetState(AIState.Chase);
        }
        else if (flagReturn.pDrop == true)
        {
            SetState(AIState.Returning);
        }
        else if(distanceToPlayer < dangerDistance)
        {
            SetState(AIState.EvadeP);
        }
        else if(bulletRange==true && bullet!=null && bulletRadius!=null)
        {
            SetState(AIState.EvadeB);
        }
        else if (flagPickup.aiFlag==false)
        {
            SetState(AIState.Capturing);
        }
        else if (flagPickup.aiFlag == true)
        {
            SetState(AIState.GetPoint);
        }

        

        
        PerformStateBehavior();
    }

    
    private void SetState(AIState newState)
    {
        currentState = newState;
        EnterState();
    }

    private void PerformStateBehavior()
    {
        
        switch (currentState)
        {
            case AIState.Capturing:
                agent.speed = tempSpeed;
                agent.destination = scoreboard.RFlag.transform.position;
                StartCoroutine(Shoot());
                break;
            case AIState.Returning:
                if(flagPickup.aiFlag==true)
                {
                    agent.speed = 1.5f;
                }
                else
                {
                    agent.speed = tempSpeed;
                }
                agent.destination = scoreboard.BFlag.transform.position;
                StartCoroutine(Shoot());
                break;
            case AIState.GetPoint:
                agent.speed = 1.5f;           
                    agent.destination = new Vector3(-7.55f, 0, 0);
                StartCoroutine(Shoot());
                break;
            case AIState.EvadeP:
                evadeDirection = transform.position - player.transform.position;
                evadeDirection.Normalize();
                Vector3 newTarget = transform.position + evadeDirection * dangerDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(newTarget, out hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
                StartCoroutine(Shoot());
                break;
            case AIState.EvadeB:
                evadeDirection = transform.position - bulletRadius.transform.position;
                evadeDirection.Normalize();
                Vector3 newBTarget = transform.position + evadeDirection * dangerDistance;
                NavMeshHit bHit;
                if (NavMesh.SamplePosition(newBTarget, out bHit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(bHit.position);
                }
                StartCoroutine(Shoot());
                break;
            case AIState.Chase:
                if (flagPickup.aiFlag == true)
                {
                    agent.speed = 1.5f;
                }
                else
                {
                    agent.speed = tempSpeed;
                }
                agent.destination = player.transform.position;
                StartCoroutine(Shoot());
                break;
        }
    }

    private void EnterState()
    {
        Debug.Log("Entering State: " + currentState);
    }
    public IEnumerator Shoot()
    {
        if (!canShoot)
            yield break; 

        canShoot = false; 
        
        GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);

        Vector2 playerPosition = player.transform.position;
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);
        canShoot=true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="PBullet")
        {
            bulletRange = true;
            Debug.Log("In Bullet");
            bulletRadius = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PBullet")
        {
            bulletRange = false;
            Debug.Log("Out Bullet");
        }
    }

}
