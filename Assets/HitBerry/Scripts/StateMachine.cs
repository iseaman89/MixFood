public class StateMachine
{
    #region Variables

    private State _currentState;

    #endregion

    #region Functions

    public void Initialize(State startState)
    {
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
    #endregion
}