using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    GameObject SettingPage = null;
    [SerializeField]
    Text endPage_time = null;
    [SerializeField]
    Button settingBtn = null;
    [SerializeField]
    Text time = null;
    [SerializeField]
    GameObject[] controlBtns = null;
    #endregion

    #region private member
    Timer timer = null;
    bool isGame = false;
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
        EventManager.Instance.Registration(State.GameRestart, OnGameRestart);

        EventManager.Instance.Registration(State.EnableSettingBtn, OnSetting);
    }
    void OnDestroy()
    {
        EventManager.Instance.Cancellation(State.GameInit, OnGameInit);
        EventManager.Instance.Cancellation(State.GameReady, OnGameReady);
        EventManager.Instance.Cancellation(State.GameStart, OnGameStart);
        EventManager.Instance.Cancellation(State.GamePause, OnGamePause);
        EventManager.Instance.Cancellation(State.GameResume, OnGameResume);
        EventManager.Instance.Cancellation(State.GameEnd, OnGameEnd);
        EventManager.Instance.Cancellation(State.GameRestart, OnGameRestart);

        EventManager.Instance.Cancellation(State.EnableSettingBtn, OnSetting);
    }
    void Update()
    {
        if(isGame)
            time.text = timer.time.ToString("0.00");
    }
    #endregion

    #region Event
    void OnGameInit(EventBase e)
    {
        Debug.Log("[UI]: Init");
        SettingPage.SetActive(false);
        timer = new Timer();
        timer.Init();
    }
    void OnGameReady(EventBase e)
    {
        Debug.Log("[UI]: Ready");
    }
    void OnGameStart(EventBase e)
    {
        Debug.Log("[UI]: Start");
        isGame = true;
        timer.Start();
        EventManager.Instance.SendEvent(State.EnableControlBtns, true);
        EventManager.Instance.SendEvent(State.EnableSettingBtn, true);
    }
    void OnGamePause(EventBase e)
    {
        Debug.Log("[UI]: Pause");
        timer.Pause();
        EventManager.Instance.SendEvent(State.EnableControlBtns, false);
    }
    void OnGameResume(EventBase e)
    {
        Debug.Log("[UI]: Resume");
        timer.Resume();
        EventManager.Instance.SendEvent(State.EnableControlBtns, true);
        EventManager.Instance.SendEvent(State.EnableSettingBtn, true);
    }
    void OnGameEnd(EventBase e)
    {
        Debug.Log("[UI]: End");
        isGame = false;
        endPage_time.text = timer.time.ToString("0.00");
        EventManager.Instance.SendEvent(State.EnableControlBtns, false);
        EventManager.Instance.SendEvent(State.EnableSettingBtn, false);
    }
    void OnGameRestart(EventBase e)
    {
        EventManager.Instance.SendEvent(State.EnableControlBtns, false);
        EventManager.Instance.SendEvent(State.EnableSettingBtn, false);
    }
    void OnSetting(EventBase e)
    {
        Debug.Log("[UI]: Setting");
        var eventData = e as EventData;
        bool enable = (bool)eventData.args[0];
        settingBtn.enabled = enable;
    }
    #endregion

    #region member function
    public void OnClick_Setting()
    {
        EventManager.Instance.SendEvent(State.GamePause);
        EventManager.Instance.SendEvent(State.EnableSettingBtn, false);
        SettingPage.SetActive(true);
    }
    public void OnClick_SettingPage_X()
    {
        EventManager.Instance.SendEvent(State.GameResume);
        SettingPage.SetActive(false);
    }
    public void OnClick_SettingPage_Resume()
    {
        Debug.Log("[UI]: click resume");
        EventManager.Instance.SendEvent(State.GameResume);
        SettingPage.SetActive(false);
    }
    public void OnClick_SettingPage_Restart()
    {
        Debug.Log("[UI]: click restart");
        EventManager.Instance.SendEvent(State.GameRestart);
        SettingPage.SetActive(false);
    }
    public void OnClick_SettingPage_Back2Menu()
    {
        Debug.Log("[UI]: click back2menu");
        EventManager.Instance.SendEvent(AppState.MenuEnter);
        SettingPage.SetActive(false);
    }

    #endregion
}

public class Timer
{
    float pauseTime = 0;
    float startTime = 0;
    bool isPause = false;
    public void Init()
    {
        pauseTime = 0;
        startTime = 0;
        isPause = false;
    }
    public void Start() 
    {
        startTime = Time.time;
        pauseTime = 0;
    }
    public void Pause()
    {
        isPause = true;
        pauseTime += Time.time - startTime;
    }
    public void Resume()
    {
        isPause = false;
        startTime = Time.time;
    }
    public float time
    {
        get
        {
             return (isPause)? pauseTime : Time.time - startTime + pauseTime;
        }
    }
}
