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
    [SerializeField]
    GameObject levelPage = null;
    [SerializeField]
    GameObject endPage = null;
    [SerializeField]
    List<GameObject> levelObjList = null;
    #endregion

    #region private member
    int currentLevel = 0;
    #endregion
    #region UnityCallback
    void Awake()
    {
        //AppState registration
        EventManager.Instance.Registration(AppState.GameEnter, OnGameEnter);
        EventManager.Instance.Registration(AppState.GameExit, OnGameExit);
        EventManager.Instance.Registration(AppState.MenuEnter, OnMenuEnter);
        EventManager.Instance.Registration(AppState.MenuExit, OnMenuExit);
        EventManager.Instance.Registration(AppState.SelectLevel, OnSelectLevel);

        //GameState registration
        EventManager.Instance.Registration(State.GameInit, OnGameInit);
        EventManager.Instance.Registration(State.GameReady, OnGameReady);
        EventManager.Instance.Registration(State.GameStart, OnGameStart);
        EventManager.Instance.Registration(State.GamePause, OnGamePause);
        EventManager.Instance.Registration(State.GameResume, OnGameResume);
        EventManager.Instance.Registration(State.GameEnd, OnGameEnd);
        EventManager.Instance.Registration(State.GameRestart, OnGameRestart);
    }
    void OnDestroy()
    {
        //AppState cancellation
        EventManager.Instance.Cancellation(AppState.GameEnter,OnGameEnter);
        EventManager.Instance.Cancellation(AppState.GameExit, OnGameExit);
        EventManager.Instance.Cancellation(AppState.MenuEnter, OnMenuEnter);
        EventManager.Instance.Cancellation(AppState.MenuExit, OnMenuExit);
        EventManager.Instance.Cancellation(AppState.SelectLevel, OnSelectLevel);

        //GameState cancellation
        EventManager.Instance.Cancellation(State.GameInit, OnGameInit);
        EventManager.Instance.Cancellation(State.GameReady, OnGameReady);
        EventManager.Instance.Cancellation(State.GameStart, OnGameStart);
        EventManager.Instance.Cancellation(State.GamePause, OnGamePause);
        EventManager.Instance.Cancellation(State.GameResume, OnGameResume);
        EventManager.Instance.Cancellation(State.GameEnd, OnGameEnd);
        EventManager.Instance.Registration(State.GameRestart, OnGameRestart);
    }
    void Start()
    {
        endPage.SetActive(false);
        levelPage.SetActive(false);
        EventManager.Instance.SendEvent(AppState.MenuEnter);      
    }
    void Update()
    {

    }
    #endregion

    #region Event
    void OnGameEnter(EventBase e)
    {
        Debug.Log("[Game]: Enter");
        levelPage.SetActive(true);
    }
    void OnGameExit(EventBase e)
    {
        Debug.Log("[Game]: Exit");
        var eventData = e as EventData;
        int level = (int)eventData.args[0];
        levelObjList[level].SetActive(false);
    }
    void OnMenuEnter(EventBase e)
    {
        Debug.Log("[Game]: Menu Enter");
        menu.SetActive(true);
    }
    void OnMenuExit(EventBase e)
    {
        Debug.Log("[Game]: Menu Exit");
        menu.SetActive(false);
    }
    void OnSelectLevel(EventBase e)
    {
        var eventData = e as EventData;
        int level = (int)eventData.args[0];
        Debug.Log("[Game]: Select level "+ level.ToString());
        EventManager.Instance.SendEvent(State.GameInit, level);
        EventManager.Instance.SendEvent(State.GameReady);
        levelPage.SetActive(false);
        EventManager.Instance.SendEvent(State.GameStart);
    }
    void OnGameInit(EventBase e)
    {
        Debug.Log("[Game]: Init");
        var eventData = e as EventData;
        int level = (int)eventData.args[0];
        levelObjList[level].SetActive(true);
    }
    void OnGameReady(EventBase e)
    {
        Debug.Log("[Game]: Ready");
    }
    void OnGameStart(EventBase e)
    {
        Debug.Log("[Game]: Start");
    }
    void OnGamePause(EventBase e)
    {

    }
    void OnGameResume(EventBase e)
    {

    }
    void OnGameEnd(EventBase e)
    {
        Debug.Log("[Game]: End");
        endPage.SetActive(true);
    }
    void OnGameRestart(EventBase e)
    {
        Debug.Log("[Game]: Restart");
        EventManager.Instance.SendEvent(State.GameInit, currentLevel);
        EventManager.Instance.SendEvent(State.GameReady);
        EventManager.Instance.SendEvent(State.GameStart);
    }
    #endregion

    #region member function
    public void OnClick_Start()
    {
        EventManager.Instance.SendEvent(AppState.MenuExit);
        EventManager.Instance.SendEvent(AppState.GameEnter);
    }
    public void OnClick_Exit()
    {
        //EventManager.Instance.SendEvent(AppState.MenuExit);
        Application.Quit();
    }
    public void OnClick_Level(int i)
    {
        currentLevel = i;
        EventManager.Instance.SendEvent(AppState.SelectLevel, i);
    }
    public void OnClick_Back2Menu()
    {
        endPage.SetActive(false);
        EventManager.Instance.SendEvent(AppState.GameExit, currentLevel);
        EventManager.Instance.SendEvent(AppState.MenuEnter);
    }
    #endregion
}
