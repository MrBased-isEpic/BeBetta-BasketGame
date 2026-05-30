using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<GameStateType, IGameState> _states;
    private IGameState _currentState;
    private GameStateType _currentStateType;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializeStates();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        TransitionTo(GameStateType.Setup);
    }

    private void Update()
    {
        _currentState?.Update();
    }

    private void InitializeStates()
    {
        _states = new Dictionary<GameStateType, IGameState>
        {
            { GameStateType.Setup,   new SetupState()   },
            { GameStateType.Playing, new PlayingState() },
            { GameStateType.Paused,  new PausedState()  },
            { GameStateType.Ended,   new EndedState()   }
        };
    }

    public void TransitionTo(GameStateType stateType)
    {
        if (!_states.TryGetValue(stateType, out IGameState nextState))
        {
            //Debug.LogWarning($"[GameManager] State '{stateType}' not found.");
            return;
        }

        _currentStateType = stateType;
        _currentState = nextState;
        _currentState.Enter();

        //Debug.Log($"[GameManager] Entered state: {stateType}");
    }

    public GameStateType GetCurrentStateType() => _currentStateType;
}
