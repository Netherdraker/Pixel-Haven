using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
 
    private PlayerControls playerControls;
 
    private Rigidbody2D rb;
 
    private Vector2 movement;
 
    private void Awake() {
 
        playerControls = new PlayerControls();
 
        rb = GetComponent<Rigidbody2D>();
 
 
 
    }
 
    private void OnEnable() {
 
        playerControls.Enable();
 
    }
 
    private void Update() {
 
        PlayerInput();
 
    }
 
    private void FixedUpdate() {
 
        Move();
 
    }
 
    private void PlayerInput() {
 
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
 
    }
 
    private void Move() {
 
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
 
    }
    
}
