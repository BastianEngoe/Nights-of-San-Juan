using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
   IState state;

   public void Tick(){
        state.Tick();
   }

   private void OnStateEnter(){
        state.OnStateEnter();
   }

   private void OnStateExit(){
        state.OnStateExit();
   }

    public void swapState(IState newState){
        state.OnStateExit();
        state = newState;
        state.OnStateEnter();
    }

}
