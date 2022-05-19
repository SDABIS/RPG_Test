using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    #region Declarations

    public enum GameState {
        PREGAME,
        LOADING,
        RUNNING,
        PAUSED
    }

    public enum GamePhase {
        START,
        WORLD,
        BATTLE
    }

    [SerializeField] GameObject[] SystemPrefabs;
    
    private List<GameObject> _instantiatedSystemPrefabs;
    
    private GameState _currentGameState = GameState.PREGAME;
    private GamePhase _currentGamePhase = GamePhase.START;

    public GameState CurrentGameState {
        get {
            return _currentGameState;
        }

        private set {
            _currentGameState = value;
        }
    }

    public GamePhase CurrentGamePhase {
        get {
            return _currentGamePhase;
        }

        private set {
            _currentGamePhase = value;
        }
    }

    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
    [System.Serializable] public class EventGamePhase : UnityEvent<GamePhase, GamePhase> { }
    public EventGameState OnGameStateChanged;
    public EventGamePhase OnGamePhaseChanged;

    #endregion
    
    #region Start Update

    private void Start() {
        
        _instantiatedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();

        EventBroker.Instance.OnBattleStart.AddListener(HandleBattleStart);
        EventBroker.Instance.OnBattleEnd.AddListener(HandleBattleEnd);

        UIManager.Instance.OnTransitionPaneFade.AddListener(HandleTransitionPaneFade);
        LoadManager.Instance.OnLoadComplete.AddListener(HandleLoadComplete);
    }

    private void HandleBattleStart() {
        UpdateState(GameState.LOADING);
    }

    public void HandleBattleEnd() {
        UpdateState(GameState.LOADING);
    }

    private void HandleTransitionPaneFade(bool isFadeIn) {
        if(!isFadeIn) return;
        switch(CurrentGamePhase) {

            case GamePhase.START: 
                LoadZone(PersistentParty.Instance.CurrentZone);
                UpdatePhase(GamePhase.WORLD);
                break;

            case GamePhase.WORLD:

                LoadManager.Instance.UnloadLevel("World");
                LoadManager.Instance.LoadLevel("Battle");
                UpdatePhase(GamePhase.BATTLE);

                break;
            case GamePhase.BATTLE:
                LoadManager.Instance.UnloadLevel("Battle");
                LoadZone(PersistentParty.Instance.CurrentZone);
                UpdatePhase(GamePhase.WORLD);
                break;
        }
        
    }

    private void LoadZone(Zone currentZone)
    {
        LoadManager.Instance.LoadLevel(currentZone.Scene.name);
    }

    private void HandleLoadComplete() {
        UpdateState(GameState.RUNNING);
    }

    #endregion

    private void InstantiateSystemPrefabs() {

        GameObject prefabInstance;
        for(int i = 0; i<SystemPrefabs.Length; i++) {
            prefabInstance = Instantiate(SystemPrefabs[i]);

            _instantiatedSystemPrefabs.Add(prefabInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i<SystemPrefabs.Length; i++) {

            Destroy(_instantiatedSystemPrefabs[i]);
        }

        _instantiatedSystemPrefabs.Clear();

    }

    #region State

    private void UpdateState(GameState state) { 
        GameState previousGameState = _currentGameState;
        _currentGameState = state;
        switch(_currentGameState) {
            case GameState.PREGAME:
                Time.timeScale = 1;
            break;
            case GameState.LOADING:
                Time.timeScale = 1;
            break;
            case GameState.RUNNING:
                Time.timeScale = 1;
            break;
            case GameState.PAUSED:
                Time.timeScale = 0;
            break;
            default:
            break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    private void UpdatePhase(GamePhase phase) { 
        GamePhase previousGamePhase = _currentGamePhase;
        _currentGamePhase = phase;
        
        switch(_currentGamePhase) {
            case GamePhase.START:
            
            break;
            case GamePhase.WORLD:
            
            break;
            case GamePhase.BATTLE:
            
            break;
            default:
            break;
        }

        OnGamePhaseChanged.Invoke(_currentGamePhase, previousGamePhase);
    }

    public void StartGame() {
        UpdateState(GameState.RUNNING);
        UpdatePhase(GamePhase.WORLD);
    }

    public void TogglePause(){
        if(_currentGameState == GameState.RUNNING) {
            UpdateState(GameState.PAUSED);
        }
        else {
            UpdateState(GameState.RUNNING);
        }
    }

    public void RestartGame() {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame() {
        Application.Quit();
    }
    
    #endregion
}
