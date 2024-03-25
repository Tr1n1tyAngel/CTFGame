using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIFSM : MonoBehaviour
{
    // References to FlagPickup and FlagReturn scripts for AI and player.
    public FlagPickup flagPickup;
    public FlagPickup pFlagPickup;
    public FlagReturn flagReturn;

    // AI movement speed and reference to the NavMeshAgent component.
    public float tempSpeed;
    public NavMeshAgent agent;

    // References to the scoreboard and bullet GameObject.
    public Scoreboard scoreboard;
    public GameObject bullet;
    public GameObject bulletRadius;

    // Reference to the player GameObject and shooting related variables.
    public GameObject player;
    public float shotForce = 10f;
    private bool canShoot = true;

    // Variables for evading bullets and players.
    public float dangerDistance = 5.0f;
    private Vector3 evadeDirection;
    public bool bulletRange = false;

    // Enum to represent different AI states.
    public enum AIState
    {
        Capturing,
        Returning,
        GetPoint,
        EvadeP,
        EvadeB,
        Chase
    }

    // Variable to keep track of the current AI state.
    private AIState currentState;

    // Start method called once before the first frame update.
    void Start()
    {
        tempSpeed = agent.speed; // Store the initial speed of the AI.
        SetState(AIState.Capturing); // Set the initial state to Capturing.
    }

    // Update method called once per frame.
    void Update()
    {
        // Calculate the distance between the AI and the player.
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Reset the rotation of the AI to face upwards.
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Check various conditions to determine the AI's current state.
        if (pFlagPickup.pFlag == true)
        {
            SetState(AIState.Chase); // If the player has the flag, chase the player.
        }
        else if (flagReturn.pDrop == true)
        {
            SetState(AIState.Returning); // If the player has dropped the flag, return to base.
        }
        else if (distanceToPlayer < dangerDistance)
        {
            SetState(AIState.EvadeP); // If the player is too close, evade the player.
        }
        else if (bulletRange == true && bullet != null && bulletRadius != null)
        {
            SetState(AIState.EvadeB); // If a bullet is close, evade the bullet.
        }
        else if (flagPickup.aiFlag == false)
        {
            SetState(AIState.Capturing); // If the AI doesn't have the flag, capture it.
        }
        else if (flagPickup.aiFlag == true)
        {
            SetState(AIState.GetPoint); // If the AI has the flag, return to base to score.
        }

        // Perform the behavior based on the current state.
        PerformStateBehavior();
    }

    // Method to set the AI's current state.
    private void SetState(AIState newState)
    {
        currentState = newState; // Set the new state.
        EnterState(); // Perform actions upon entering the new state.
    }

    // Method to perform actions based on the current state.
    private void PerformStateBehavior()
    {
        switch (currentState)
        {
            case AIState.Capturing:
                agent.speed = tempSpeed; // Set the speed to the normal speed.
                agent.destination = scoreboard.RFlag.transform.position; // Set the destination to the red flag's position.
                StartCoroutine(Shoot()); // Start shooting.
                break;
            case AIState.Returning:
                if (flagPickup.aiFlag == true)
                {
                    agent.speed = 1.5f; // Slow down if carrying the flag.
                }
                else
                {
                    agent.speed = tempSpeed; // Otherwise, move at normal speed.
                }
                agent.destination = scoreboard.BFlag.transform.position; // Set the destination to the blue flag's position.
                StartCoroutine(Shoot()); // Start shooting.
                break;
            case AIState.GetPoint:
                agent.speed = 1.5f; // Slow down if carrying the flag.
                agent.destination = new Vector3(-7.55f, 0, 0); // Set the destination to the scoring point.
                StartCoroutine(Shoot()); // Start shooting.
                break;
            case AIState.EvadeP:
                evadeDirection = transform.position - player.transform.position; // Calculate the direction to evade the player.
                evadeDirection.Normalize(); // Normalize the direction.
                Vector3 newTarget = transform.position + evadeDirection * dangerDistance; // Calculate the new target position.
                // Use NavMesh sampling to find a valid position to evade to.
                NavMeshHit hit;
                if (NavMesh.SamplePosition(newTarget, out hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position); // Set the destination to the evading position.
                }
                StartCoroutine(Shoot()); // Start shooting.
                break;
            case AIState.EvadeB:
                evadeDirection = transform.position - bulletRadius.transform.position; // Calculate the direction to evade the bullet.
                evadeDirection.Normalize(); // Normalize the direction.
                Vector3 newBTarget = transform.position + evadeDirection * dangerDistance; // Calculate the new target position for bullet evasion.
                NavMeshHit bHit;
                if (NavMesh.SamplePosition(newBTarget, out bHit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(bHit.position); // Set the destination to the bullet evading position.
                }
                StartCoroutine(Shoot()); // Start shooting.
                break;
            case AIState.Chase:
                if (flagPickup.aiFlag == true)
                {
                    agent.speed = 1.5f; // Slow down if carrying the flag.
                }
                else
                {
                    agent.speed = tempSpeed; // Otherwise, move at normal speed.
                }
                agent.destination = player.transform.position; // Set the destination to the player's position.
                StartCoroutine(Shoot()); // Start shooting.
                break;
        }
    }

    // Method called when entering a new state.
    private void EnterState()
    {
        Debug.Log("Entering State: " + currentState); // Log the current state for debugging purposes.
    }

    // Coroutine for shooting.
    public IEnumerator Shoot()
    {
        if (!canShoot)
            yield break; // If can't shoot, exit the coroutine.

        canShoot = false; // Set canShoot to false to prevent continuous shooting.

        GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity); // Instantiate a new bullet.

        Vector2 playerPosition = player.transform.position; // Get the player's position.
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized; // Calculate the direction to shoot.

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate the angle for the bullet's rotation.
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle); // Set the bullet's rotation.

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component of the bullet.
        rb.AddForce(direction * shotForce, ForceMode2D.Impulse); // Add force to the bullet to shoot it.

        yield return new WaitForSeconds(1f); // Wait for 1 second before allowing the AI to shoot again.
        canShoot = true; // Set canShoot to true to allow shooting.
    }

    // Method called when a 2D collider enters the trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PBullet") // Check if the collider is a player's bullet.
        {
            bulletRange = true; // Set bulletRange to true to indicate that a bullet is within range.
            bulletRadius = other.gameObject; // Store the reference to the bullet's GameObject.
        }
    }

    // Method called when a 2D collider exits the trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PBullet") // Check if the collider is a player's bullet.
        {
            bulletRange = false; // Set bulletRange to false to indicate that the bullet is no longer within range.
        }
    }
}
