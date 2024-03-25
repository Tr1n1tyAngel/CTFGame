using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{
    // Flags to indicate if the player or AI has picked up their respective flag.
    [SerializeField] public bool pFlag = false;
    [SerializeField] public bool aiFlag = false;

    // Reference to the FlagReturn script.
    public FlagReturn flagReturn;

    // This method is called when the object enters a 2D collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the blue flag.
        if (other.tag == "BFlag" && this.tag == "Player")
        {
            pFlag = true; // Set the player flag to true.
            other.transform.parent = this.transform; // Attach the flag to the player.
        }
        // Check if the AI has collided with the red flag.
        else if (other.tag == "RFlag" && this.tag == "AI")
        {
            aiFlag = true; // Set the AI flag to true.
            other.transform.parent = this.transform; // Attach the flag to the AI.
        }
    }

    // This method is called when the object exits a 2D collider.
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player has dropped the blue flag.
        if (other.tag == "BFlag" && this.tag == "Player")
        {
            flagReturn.pDrop = true; // Set the player drop flag to true.
        }
        // Check if the AI has dropped the red flag.
        else if (other.tag == "RFlag" && this.tag == "AI")
        {
            flagReturn.aiDrop = true; // Set the AI drop flag to true.
        }
    }
}