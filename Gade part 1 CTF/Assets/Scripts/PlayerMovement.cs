using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for player movement speed.
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float TempSpeed;

    // Reference to the player's Rigidbody2D component.
    [SerializeField] public Rigidbody2D rigidbody;

    // Variable to store movement input.
    [SerializeField] private Vector2 movement;

    // Reference to the FlagPickup script and a flag to restrict speed.
    public FlagPickup flagPickup;
    public bool speedRestrict = false;

    // Start method called once before the first frame update.
    private void Start()
    {
        // Store the initial move speed.
        TempSpeed = moveSpeed;
    }

    // Update method called once per frame.
    void Update()
    {
        // Check if the player has picked up the flag and if speed restriction is not already applied.
        if (flagPickup.pFlag == true && speedRestrict == false)
        {
            speedRestrict = true; // Apply speed restriction.
            moveSpeed = moveSpeed * 0.5f; // Reduce the move speed by half.
        }
        // Check if the player no longer has the flag.
        else if (flagPickup.pFlag == false)
        {
            speedRestrict = false; // Remove speed restriction.
            moveSpeed = TempSpeed; // Reset the move speed to its original value.
        }

        // Get input for horizontal and vertical movement.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // FixedUpdate method called at a fixed interval for physics updates.
    void FixedUpdate()
    {
        // Move the player's Rigidbody2D based on input and move speed.
        rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
