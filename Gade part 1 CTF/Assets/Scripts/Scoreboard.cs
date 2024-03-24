using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    public int pScore;
    public int aiScore;
    public int round;
    public GameObject Player;
    public GameObject AI;
    public GameObject RFlag;
    public GameObject BFlag;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;
    public TextMeshProUGUI gameOverText;
    public bool restart = false;
    public FlagReturn flagReturnAI;
    public FlagReturn flagReturnPlayer;
    public FlagPickup flagPickupPlayer;
    public FlagPickup flagPickupAI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && restart == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            restart = false;
        }
    }

    public void RoundSwitch()
    {
        round++;
        Player.transform.position = new Vector3(4f, 0, 0);
        AI.transform.position = new Vector3(-4f, 0, 0);
        RFlag.transform.parent = null;
        BFlag.transform.parent = null;
        RFlag.transform.position = new Vector3(7.55f, 0, 0);
        BFlag.transform.position = new Vector3(-7.55f, 0, 0);
        flagReturnAI.pDrop = false;
        flagReturnPlayer.pDrop = false;
        flagReturnAI.aiDrop = false;
        flagReturnPlayer.aiDrop = false;
        flagPickupPlayer.pFlag=false; 
        flagPickupPlayer.aiFlag=false;
        flagPickupAI.pFlag = false;
        flagPickupAI.aiFlag = false;
        if (round==5)
        {
            Time.timeScale = 0;
            if(pScore>aiScore)
            {
                gameOverText.text = "Blue Wins press R to restart";
                restart = true;
            }
            else
            {
                restart = true; ;
                gameOverText.text = "Red Wins press R to restart";
            }
        }
    }
    public void Scoring()
    {
        blueScoreText.text = "Score: " +pScore;
        redScoreText.text = "Score: " + aiScore;
    }
}
