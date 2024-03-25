using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    // Variables to keep track of player and AI scores, and the current round.
    public int pScore;
    public int aiScore;
    public int round;

    // References to player, AI, and flags GameObjects.
    public GameObject Player;
    public GameObject AI;
    public GameObject RFlag;
    public GameObject BFlag;

    // References to UI elements to display scores and game over message.
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;
    public TextMeshProUGUI gameOverText;

    // Flag to indicate if the game can be restarted.
    public bool restart = false;

    // References to FlagReturn and FlagPickup scripts for both player and AI.
    public FlagReturn flagReturnAI;
    public FlagReturn flagReturnPlayer;
    public FlagPickup flagPickupPlayer;
    public FlagPickup flagPickupAI;

    // Update method called once per frame.
    private void Update()
    {
        // Check if the R key is pressed and the game can be restarted.
        if (Input.GetKeyDown(KeyCode.R) && restart == true)
        {
            // Reload the current scene to restart the game.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Set the time scale back to normal.
            Time.timeScale = 1;

            // Reset the restart flag.
            restart = false;
        }
    }

    // Method to handle the switching of rounds.
    public void RoundSwitch()
    {
        // Increment the round counter.
        round++;

        // Reset the parent of both flags to null (detach them from any player or AI).
        RFlag.transform.parent = null;
        BFlag.transform.parent = null;

        // Reset the positions of the player, AI, and flags.
        Player.transform.position = new Vector3(4f, 0, 0);
        AI.transform.position = new Vector3(-4f, 0, 0);
        RFlag.transform.position = new Vector3(7.55f, 0, 0);
        BFlag.transform.position = new Vector3(-7.55f, 0, 0);

        // Reset the drop flags for both player and AI.
        flagReturnAI.pDrop = false;
        flagReturnPlayer.pDrop = false;
        flagReturnAI.aiDrop = false;
        flagReturnPlayer.aiDrop = false;

        // Reset the flag pickup flags for both player and AI.
        flagPickupPlayer.pFlag = false;
        flagPickupPlayer.aiFlag = false;
        flagPickupAI.pFlag = false;
        flagPickupAI.aiFlag = false;

        // Check if the game has reached the final round.
        if (round == 5)
        {
            // Pause the game.
            Time.timeScale = 0;

            // Check who has the higher score and display the appropriate game over message.
            if (pScore > aiScore)
            {
                gameOverText.text = "Blue Wins press R to restart";
                restart = true;
            }
            else
            {
                gameOverText.text = "Red Wins press R to restart";
                restart = true;
            }
        }
    }

    // Method to update the score display.
    public void Scoring()
    {
        // Update the score texts with the current scores.
        blueScoreText.text = "Score: " + pScore;
        redScoreText.text = "Score: " + aiScore;
    }
}
