using UnityEngine;

public class GameStateManager : Manager
{
    [SerializeField] private GameState _gameState = GameState.Default;

    private GameState _currentState = GameState.Default;

    private void OnEnable()
    {
        
    }

    private void OnValidate()
    {
        if(_gameState != _currentState)
            game.Events.Trigger(new ChangeGameStateEvent { newState = _gameState });
    }

    private void Start()
    {
        game.Events.On<ChangeGameStateEvent>(handleChangeGameStateEvent);

        if (_gameState != _currentState && _currentState != GameState.Default)
            game.Events.Trigger(new ChangeGameStateEvent { newState = _gameState });
        else
            game.Events.Trigger(new ChangeGameStateEvent { newState = GameState.Init });
    }

    private void handleChangeGameStateEvent(ChangeGameStateEvent e)
    {
        if (e.newState == _currentState)
        {
            Debug.LogError($"Attempted to transition state to active state ({_currentState})");
            return;
        }

        Log($"Transitioning from {_currentState} to {e.newState}...");

        GameState oldState = _currentState;

        game.Events.Trigger(new GameStateChangingEvent { newState = e.newState, oldState = oldState });

        _currentState = e.newState;

        Log($"Transitioned from {oldState} to {_currentState}.");

        game.Events.Trigger(new GameStateChangedEvent { newState = _currentState, oldState = oldState });

        _gameState = _currentState;
    }
}
