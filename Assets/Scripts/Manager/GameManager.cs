using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    GameObject menu = null;
    #endregion
    #region UnityCallback
    void Awake()
    {
        //AppState registration
        EventManager.Instance.Registration(AppState.GameEnter, OnGameEnter);
        EventManager.Instance.Registration(AppState.GameExit, OnGameExit);
        EventManager.Instance.Registration(AppState.MenuEnter, OnMenuEnter);
        EventManager.Instance.Registration(AppState.MenuExit, OnMenuExit);

        //GameState registration
        EventManager.Instance.Registration(State.GameInit, OnGameInit);
        EventManager.Instance.Registration(State.GameReady, OnGameReady);
        EventManager.Instance.Registration(State.GameStart, OnGameStart);
        EventManager.Instance.Registration(State.GamePause, OnGamePause);
        EventManager.Instance.Registration(State.GameResume, OnGameResume);
        EventManager.Instance.Registration(State.GameEnd, OnGameEnd);
    }
    void OnDestroy()
    {
        //AppState registration
        EventManager.Instance.Cancellation(AppState.GameEnter,OnGameEnter);
        EventManager.Instance.Cancellation(AppState.GameExit, OnGameExit);
        EventManager.Instance.Cancellation(AppState.MenuEnter, OnMenuEnter);
        EventManager.Instance.Cancellation(AppState.MenuExit, OnMenuExit);

        EventManager.Instance.Cancellation(State.GameInit, OnGameInit);
        EventManager.Instance.Cancellation(State.GameReady, OnGameReady);
        EventManager.Instance.Cancellation(State.GameStart, OnGameStart);
        EventManager.Instance.Cancellation(State.GamePause, OnGamePause);
        EventManager.Instance.Cancellation(State.GameResume, OnGameResume);
        EventManager.Instance.Cancellation(State.GameEnd, OnGameEnd);
    }
    void Start()
    {
            EventManager.Instance.SendEvent(AppState.MenuEnter);      
    }
    void Update()
    {

    }
    #endregion

    #region Event
    void OnGameEnter(EventBase e)
    {
        EventManager.Instance.SendEvent(State.GameInit);
        EventManager.Instance.SendEvent(State.GameReady);
        menu.SetActive(false);
        EventManager.Instance.SendEvent(State.GameStart);
    }
    void OnGameExit(EventBase e)
    {

    }
    void OnMenuEnter(EventBase e)
    {
        menu.SetActive(true);
    }
    void OnMenuExit(EventBase e)
    {

    }
    void OnGameInit(EventBase e)
    {
        
    }
    void OnGameReady(EventBase e)
    {

    }
    void OnGameStart(EventBase e)
    {

    }
    void OnGamePause(EventBase e)
    {

    }
    void OnGameResume(EventBase e)
    {

    }
    void OnGameEnd(EventBase e)
    {
        EventManager.Instance.SendEvent(AppState.GameExit);
        EventManager.Instance.SendEvent(AppState.MenuEnter);
    }
    #endregion

    #region member function
    public void OnClick_Start()
    {
        EventManager.Instance.SendEvent(AppState.GameEnter);
    }
    public void OnClick_Exit()
    {
        EventManager.Instance.SendEvent(AppState.MenuExit);
    }
    #endregion
}
