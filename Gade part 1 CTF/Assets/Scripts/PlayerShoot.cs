using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Reference to the bullet prefab.
    public GameObject bullet;

    // The force with which the bullet will be shot.
    public float shotForce = 10f;

    // Reference to the projectile's child (if any).
    public Transform projectileChild;

    // Variable to hold the instantiated bullet GameObject.
    GameObject projectile;

    // Update method called once per frame.
    void Update()
    {
        // Check if the left mouse button is pressed.
        if (Input.GetMouseButtonDown(0))
        {
            // Call the Shoot method to shoot a bullet.
            Shoot();
        }
    }

    // Method to shoot a bullet.
    void Shoot()
    {
        // Instantiate a new bullet at the player's position with no rotation.
        projectile = Instantiate(bullet, transform.position, Quaternion.identity);

        // Get the first child of the projectile (if it exists).
        projectileChild = projectile.transform.GetChild(0);

        // Get the mouse position in the world and calculate the direction from the player to the mouse.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Calculate the angle between the player and the mouse position.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the projectile to face towards the mouse position.
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Get the Rigidbody2D component of the projectile and add force to it in the direction of the mouse position.
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce, ForceMode2D.Impulse);
    }
}
