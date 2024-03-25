using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIScore : MonoBehaviour
{
    // References to other scripts.
    public FlagPickup flagPickup;
    public Scoreboard scoreboard;

    // This method is called when the AI enters a 2D collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the AI is carrying the flag and has collided with its own base.
        if (flagPickup.aiFlag == true && other.tag == "AI")
        {
            // Increment the AI's score.
            scoreboard.aiScore++;

            // Reset the flags.
            flagPickup.aiFlag = false;
            flagPickup.pFlag = false;

            // Switch the round and update the scoring.
            scoreboard.RoundSwitch();
            scoreboard.Scoring();
        }
    }
}
