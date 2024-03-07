using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    State currentState;

    public void ChangeState(State newState){
        if (currentState != null)
            currentState.OnExit();
        
        
    }

    public abstract class State{

        public StateController sc;

        public virtual void OnEnter(StateController controller){
            sc = controller;
        }

        public virtual void OnHurt(){

        }

        public virtual void OnUpdate(){

        }

        public virtual void OnExit(){

        }
    }
}
