
abstract public class BaseState<T> {

  public StateMachine<T> stateMachine;

  public virtual void Enter(T entity) {}
  public virtual void Execute (T entity) {}
  public virtual void Exit(T entity) {}
}