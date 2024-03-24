using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class FlagReturn : MonoBehaviour
{
    public FlagPickup flagPickup;
    public Scoreboard scoreboard;
    public bool pDrop=false;
    public bool aiDrop=false;

    public void Update()
    {
        if(scoreboard.BFlag.transform.parent==null)
        {
            flagPickup.pFlag = false;
        }
        if (scoreboard.RFlag.transform.parent == null)
        {
            flagPickup.aiFlag = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.tag== "RFlag" && other.tag == "Player")
        {
            
            this.transform.position = new Vector3(7.55f, 0, 0);
            aiDrop = false;
            flagPickup.aiFlag = false;
            
        }
        else if (this.tag == "BFlag" && other.tag == "AI")
        {
            this.transform.position = new Vector3(-7.55f, 0, 0);
            
            pDrop = false;
            flagPickup.pFlag = false;
        }
    }
}
