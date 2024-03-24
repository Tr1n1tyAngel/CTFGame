using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public float shotForce = 10f;
    public Transform projectileChild;
    GameObject projectile;



    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            
            Shoot();
            
        }
        
    }

    void Shoot()
    {
        
        projectile = Instantiate(bullet, transform.position, Quaternion.identity);

        projectileChild = projectile.transform.GetChild(0);
        

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce, ForceMode2D.Impulse);

        

    }
}
