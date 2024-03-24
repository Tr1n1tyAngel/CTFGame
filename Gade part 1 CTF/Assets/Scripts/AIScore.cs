using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIScore : MonoBehaviour
{
    public FlagPickup flagPickup;
    public Scoreboard scoreboard;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (flagPickup.aiFlag == true && other.tag == "AI")
        {
            scoreboard.aiScore++;
            flagPickup.aiFlag = false;
            flagPickup.pFlag = false;
            scoreboard.RoundSwitch();
            scoreboard.Scoring();
        }
    }
}
