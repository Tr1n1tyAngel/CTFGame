using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float TempSpeed;
    [SerializeField] public Rigidbody2D rigidbody;
    [SerializeField] private Vector2 movement;
    public FlagPickup flagPickup;
    public bool speedRestrict = false;

    private void Start()
    {
        TempSpeed = moveSpeed;
    }
    void Update()
    {
        if(flagPickup.pFlag == true && speedRestrict == false)
        {
            speedRestrict = true;
            moveSpeed = moveSpeed * 0.5f;
        }
        else if(flagPickup.pFlag == false)
        {
            speedRestrict = false;
            moveSpeed = TempSpeed;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    { 
        rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
