using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dafunk;

// It is not the state machines job to decide when to go into what state
// Owner MonoBehaviour will request state change by providing IState object, can also request to return to previous state
// The state machine takes an IState object and provides a way to execute whats in them, and change and replace functionality
// It will it will take the currently running IState and execute its functionality each frame
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

