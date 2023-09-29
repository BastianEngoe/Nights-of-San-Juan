using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
   protected State state;

   virtual public void Tick(){
        state.Tick();
   }

   virtual protected void OnStateEnter(){
        state.OnStateEnter();
   }

   virtual protected void OnStateExit(){
        state.OnStateExit();
   }

    virtual public void swapState(State newState){
        state.OnStateExit();
        state = newState;
        state.OnStateEnter();
    }

}
