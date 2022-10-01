using UnityEngine;

public class StateMachine <T> {
  public T owner { get; private set; }
  public BaseState<T> currentState { get; private set; }
  public BaseState<T> previousState { get; private set; }
  public BaseState<T> globalState { get; private set; }

  public void Awake() {
    currentState = null;
    previousState = null;
    globalState = null;
  }

  public void Configure(T owner, BaseState<T> initialState) {
    this.owner = owner;
    ChangeState(initialState);
  }

  public void Update() {
    if (globalState != null)  globalState.Execute(owner);
    if (currentState != null) currentState.Execute(owner);
  }

  public void ChangeState(BaseState<T> newState) {
    newState.stateMachine = this;

    previousState = currentState;
    if (currentState != null) {
      currentState.Exit(owner);
    }
    currentState = newState;
    if (currentState != null) {
      currentState.Enter(owner);
    }
  }

  public void RevertToPreviousState() {
    if (previousState != null) {
      ChangeState(previousState);
    }
  }
};