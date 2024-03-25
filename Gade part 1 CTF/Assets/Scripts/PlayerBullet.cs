using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // This method is called when the bullet enters a 2D collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has hit the AI.
        if (other.tag == "AI")
        {
            // Generate a random integer between 0 and 2.
            int randomInt = Random.Range(0, 3);

            // Detach all children of the AI (used to remove the flag if the AI is carrying it).
            other.transform.DetachChildren();

            // Teleport the AI to a random spawn point based on the random integer generated earlier.
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

            // Destroy the bullet after it hits the AI.
            Destroy(this.gameObject);
        }
        // Check if the bullet has hit an obstacle.
        else if (other.tag == "Obstacle")
        {
            // Destroy the bullet upon collision with an obstacle.
            Destroy(this.gameObject);
        }
    }
}
