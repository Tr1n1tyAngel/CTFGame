using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "AI")
        {
            int randomInt = Random.Range(0, 3);
            other.transform.DetachChildren();
            switch (randomInt)
            {
                case 0:
                    other.transform.position = new Vector3(-8.5F, 4.5f, 0);
                    break;
                case 1:
                    other.transform.position = new Vector3(-8.5F, 0, 0);
                    break;
                case 2:
                    other.transform.position = new Vector3(-8.5F, -4.5f, 0);
                    break;
            }
            Destroy(this.gameObject);
        }
        else if (other.tag == "Obstacle")
        {
           Destroy(this.gameObject);
        }
    }
}
