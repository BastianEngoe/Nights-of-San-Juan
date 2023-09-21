using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public virtual void Tick(){

    }

    public virtual void OnStateEnter(){

    }

    public virtual void OnStateExit(){

    }

}
