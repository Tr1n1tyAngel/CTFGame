using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerScore : MonoBehaviour
{
    // References to FlagPickup and Scoreboard scripts.
    public FlagPickup flagPickup;
    public Scoreboard scoreboard;

    // This method is called when the player enters a 2D collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is carrying the flag and has collided with their own base.
        if (flagPickup.pFlag == true && other.tag == "Player")
        {
            // Increment the player's score.
            scoreboard.pScore++;

            // Reset the flags.
            flagPickup.aiFlag = false;
            flagPickup.pFlag = false;

            // Switch to the next round and update the scoreboard.
            scoreboard.RoundSwitch();
            scoreboard.Scoring();
        }
    }
}