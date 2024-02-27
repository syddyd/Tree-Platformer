using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemController : MonoBehaviour
{
    public void HandleMove(InputAction.CallbackContext context){
        if (context.performed)
            print("performed");
        if (context.started)
            print("started");
        if (context.canceled)
            print("canceled");
    }
}
