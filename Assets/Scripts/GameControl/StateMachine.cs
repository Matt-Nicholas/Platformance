using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dafunk;

public class StateMachine {

  private IState currentlyRunningState;
  private IState previousState;

  public IState GetCurrentState() {
    return currentlyRunningState;
  }

  public void ChangeState(IState newState) {
    if(this.currentlyRunningState != null) {
      this.currentlyRunningState.Exit();
    }
    this.previousState = this.currentlyRunningState;

    this.currentlyRunningState = newState;
    this.currentlyRunningState.Enter();
  }

  public void ExecuteStateUpdate() {
    var runningState = this.currentlyRunningState;

    if(runningState != null)
      runningState.Execute();
  }

  public void SwitchToPreviousState() {
    this.currentlyRunningState.Exit();
    this.currentlyRunningState = this.previousState;
    this.currentlyRunningState.Enter();
  }
}

