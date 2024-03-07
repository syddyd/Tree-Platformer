using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Callbacks;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Rigidbody2D rb; 
    Boolean canDash;
    [SerializeField]float dashingPower = 15.0f;
    float dashingTime = 0.75f;
    float dashingCooldown = 3.0f; 
    public PlayMakerFSM fsm;


    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        fsm = GetComponent<PlayMakerFSM>();
    }

    public IEnumerator DashMove(){
        print("dashing");
        //rb.GetComponent<Rigidbody2D>();
        canDash = false;
        float OgGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = OgGravity;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        fsm.SendEvent("Dash Exit");
    }
}
