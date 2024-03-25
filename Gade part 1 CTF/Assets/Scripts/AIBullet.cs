using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBullet : MonoBehaviour
{
    // This method is called when the bullet enters a 2D collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Generate a random integer between 0 and 2.
        int randomInt = Random.Range(0, 3);

        // Check if the bullet has hit the player.
        if (other.tag == "Player")
        {
            // Detach all children of the player (used to remove the flag if the player is carrying it).
            other.transform.DetachChildren();

            // Teleport the player to a random spawn point based on the random integer generated earlier.
            switch (randomInt)
            {
                case 0:
                    other.transform.position = new Vector3(8.5F, 4.5f, 0);
                    break;
                case 1:
                    other.transform.position = new Vector3(8.5F, 0, 0);
                    break;
                case 2:
                    other.transform.position = new Vector3(8.5F, -4.5f, 0);
                    break;
            }

            // Destroy the bullet after it hits the player.
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
