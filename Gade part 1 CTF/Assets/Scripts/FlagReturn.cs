using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class FlagReturn : MonoBehaviour
{
    // References to the FlagPickup and Scoreboard scripts.
    public FlagPickup flagPickup;
    public Scoreboard scoreboard;

    // Flags to indicate if the player or AI has dropped their respective flag.
    public bool pDrop = false;
    public bool aiDrop = false;

    // Update method called once per frame.
    public void Update()
    {
        // Check if the blue flag has no parent, meaning it has been dropped.
        if (scoreboard.BFlag.transform.parent == null)
        {
            flagPickup.pFlag = false; // Reset the player flag.
        }
        // Check if the red flag has no parent, meaning it has been dropped.
        if (scoreboard.RFlag.transform.parent == null)
        {
            flagPickup.aiFlag = false; // Reset the AI flag.
        }
    }

    // This method is called when the object enters a 2D collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the red flag has collided with the player.
        if (this.tag == "RFlag" && other.tag == "Player")
        {
            this.transform.position = new Vector3(7.55f, 0, 0); // Reset the red flag's position.
            aiDrop = false; // Reset the AI drop flag.
            flagPickup.aiFlag = false; // Reset the AI flag.
        }
        // Check if the blue flag has collided with the AI.
        else if (this.tag == "BFlag" && other.tag == "AI")
        {
            this.transform.position = new Vector3(-7.55f, 0, 0); // Reset the blue flag's position.
            pDrop = false; // Reset the player drop flag.
            flagPickup.pFlag = false; // Reset the player flag.
        }
    }
}
