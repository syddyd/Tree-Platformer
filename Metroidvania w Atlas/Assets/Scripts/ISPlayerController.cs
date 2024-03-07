using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;
using HutongGames.PlayMaker;

public class ISPlayerController : MonoBehaviour
{
    private InputActions inputActions;
    public Rigidbody2D rb;
    public float maxSpeed = 8;
    private float actSpeed;
    public float acceleration = 0.5f;
    public float slowDown = 0.8f;
    float moveDirection = 0;
    Transform trans;
    private InputAction move;
    public PlayMakerFSM fsm;
    [Range(0.01f, 60f)]public float jumpPower = 15;
    public float fallMultiplier = 2.5f;
    [Range(0.0f, 1.0f)]public float airMobility = 0.8f;

    [SerializeField] Camera mainCamera;

    void Awake(){
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        fsm = GetComponent<PlayMakerFSM>();
        trans = rb.transform;
        actSpeed = maxSpeed;
    } 

    void OnEnable(){
        move = inputActions.Player.Move;
        move.Enable();
        move.performed += Move;
    }

    void OnDisable(){
        move.Disable();
    }

    public void Move(InputAction.CallbackContext context){
        fsm.SendEvent("Attempt Move");
        if (isGrounded()){
            moveDirection = context.ReadValue<Vector2>().x;
        } else {
            moveDirection = context.ReadValue<Vector2>().x * airMobility;
        }
        if (context.phase == InputActionPhase.Started){
            actSpeed = 0.0f;
        }
        if (context.canceled){
            fsm.SendEvent("Move Canceled");
        }
    }

    public void Jump(InputAction.CallbackContext context){
        bool ground = isGrounded();
        print(ground);
        if (ground){
            if (!context.canceled){
                print("big jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            } else {
                print("little jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower/2);
            }
        }

    }

    public void Dash(InputAction.CallbackContext context){
        print("dashing in playcontroller");
        fsm.SendEvent("Dash Start");        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainCamera.transform.position = new Vector3(trans.position.x, trans.position.y, mainCamera.transform.position.z);
        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up *Physics2D.gravity.y *(fallMultiplier -1) *Time.deltaTime;
        }
        if (actSpeed < maxSpeed){
            actSpeed += acceleration;
        }
        rb.velocity = new Vector2(moveDirection * actSpeed , rb.velocity.y);
    }

    private Boolean isGrounded(){
        //Debug.DrawLine(trans.position, new Vector3(trans.position.x, trans.position.y -10f, trans.position.z), Color.red);
        return Physics2D.Raycast(trans.position, Vector2.down, 1.1f, 8);
    }
}
