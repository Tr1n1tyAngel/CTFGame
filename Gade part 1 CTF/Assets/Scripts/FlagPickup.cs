using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{
    [SerializeField] public bool pFlag = false;
    [SerializeField] public bool aiFlag = false;
    public FlagReturn flagReturn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="BFlag" && this.tag == "Player")
        {
            pFlag = true;
            other.transform.parent = this.transform;
           
        }
        else if(other.tag=="RFlag" && this.tag == "AI")
        {
            aiFlag = true;
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BFlag" && this.tag == "Player")
        {
            
            flagReturn.pDrop = true;
            

        }
        else if (other.tag == "RFlag" && this.tag == "AI")
        {
            
            flagReturn.aiDrop = true;
            
        }
    }
}
