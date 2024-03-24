using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerScore : MonoBehaviour
{
    public FlagPickup flagPickup;
    public Scoreboard scoreboard;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(flagPickup.pFlag==true && other.tag=="Player")
        {
            scoreboard.pScore++;
            flagPickup.aiFlag = false;
            flagPickup.pFlag = false;
            scoreboard.RoundSwitch();
            scoreboard.Scoring();

        }
    }
}
