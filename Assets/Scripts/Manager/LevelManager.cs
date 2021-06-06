using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class LevelManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    EnemyShooter enemyShooter = null;
    #endregion
    #region UnityCallback
    void Awake()
    {
        EventManager.Instance.Registration(State.GameInit, OnGameInit);
        EventManager.Instance.Registration(State.GameReady, OnGameReady);
        EventManager.Instance.Registration(State.GameStart, OnGameStart);
        EventManager.Instance.Registration(State.GamePause, OnGamePause);
        EventManager.Instance.Registration(State.GameResume, OnGameResume);
        EventManager.Instance.Registration(State.GameEnd, OnGameEnd);
    }
    void OnDestroy()
    {
        EventManager.Instance.Cancellation(State.GameInit, OnGameInit);
        EventManager.Instance.Cancellation(State.GameReady, OnGameReady);
        EventManager.Instance.Cancellation(State.GameStart, OnGameStart);
        EventManager.Instance.Cancellation(State.GamePause, OnGamePause);
        EventManager.Instance.Cancellation(State.GameResume, OnGameResume);
        EventManager.Instance.Cancellation(State.GameEnd, OnGameEnd);
    }
    #endregion
    #region Event
    void OnGameInit(EventBase e)
    {
        Debug.Log("[Level]: Init");
    }
    void OnGameReady(EventBase e)
    {
        Debug.Log("[Level]: Ready");
    }
    void OnGameStart(EventBase e)
    {
        Debug.Log("[Level]: Start");
        enemyShooter.GameStart();
    }
    void OnGamePause(EventBase e)
    {
        Debug.Log("[Level]: Pause");
        enemyShooter.GamePause();
    }
    void OnGameResume(EventBase e)
    {
        Debug.Log("[Level]: Resume");
        enemyShooter.GameStart();
    }
    void OnGameEnd(EventBase e)
    {
        Debug.Log("[Level]: End");
        enemyShooter.GameEnd();
    }
    #endregion
}
