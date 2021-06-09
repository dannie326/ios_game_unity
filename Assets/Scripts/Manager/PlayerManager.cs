using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    Player playerPref = null;
    [SerializeField]
    GameObject[] controlBtns = null;
    #endregion

    #region private member
    Vector3 spawnPos = Vector3.zero;
    Player player = null;
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

        EventManager.Instance.Registration(State.EnableControlBtns, OnControl);
    }
    void OnDestroy()
    {
        EventManager.Instance.Cancellation(State.GameInit, OnGameInit);
        EventManager.Instance.Cancellation(State.GameReady, OnGameReady);
        EventManager.Instance.Cancellation(State.GameStart, OnGameStart);
        EventManager.Instance.Cancellation(State.GamePause, OnGamePause);
        EventManager.Instance.Cancellation(State.GameResume, OnGameResume);
        EventManager.Instance.Cancellation(State.GameEnd, OnGameEnd);

        EventManager.Instance.Cancellation(State.EnableControlBtns, OnControl);
    }
    #endregion

    #region Event
    void OnGameInit(EventBase e)
    {
        Debug.Log("[Player]: Init");
       
        // spawn player prefab
        spawnPos = new Vector3(0, -2.9f, 0.0f);
        if (player != null) Destroy(player.gameObject);
        player = Instantiate(playerPref, spawnPos, Quaternion.identity);
        ClearControl();
        SetUpControl();
        EventManager.Instance.SendEvent(State.EnableControlBtns, false);
    }
    void OnGameReady(EventBase e)
    {
        Debug.Log("[Player]: Ready");
    }
    void OnGameStart(EventBase e)
    {
        Debug.Log("[Player]: Start");
        player.gameEnd += triggerResult;
    }
    void OnGamePause(EventBase e)
    {
        Debug.Log("[Player]: Pause");
    }
    void OnGameResume(EventBase e)
    {
        Debug.Log("[Player]: Resume");
    }
    void OnGameEnd(EventBase e)
    {
        Debug.Log("[Player]: End");
        Destroy(player.gameObject);
        player = null;
        ClearControl();
    }
    void OnControl(EventBase e)
    {
        Debug.Log("[Player]: Control");
        var eventData = e as EventData;
        bool enable = (bool)eventData.args[0];
        foreach (GameObject b in controlBtns)
        {
            b.GetComponent<EventTrigger>().enabled = enable;
            b.GetComponent<Button>().interactable = enable;
        }
    }
    #endregion

    #region memeber function
    void SetUpControl()
    {
        EventTrigger triggerR = controlBtns[0].GetComponent<EventTrigger>();
        triggerR.triggers[0].callback.AddListener((data) => { player.OnPress_R(); });
        triggerR.triggers[1].callback.AddListener((data) => { player.OnRelease_R(); });
        EventTrigger triggerL = controlBtns[1].GetComponent<EventTrigger>();
        triggerL.triggers[0].callback.AddListener((data) => { player.OnPress_L(); });
        triggerL.triggers[1].callback.AddListener((data) => { player.OnRelease_L(); });
        EventTrigger triggerJ = controlBtns[2].GetComponent<EventTrigger>();
        triggerJ.triggers[0].callback.AddListener((data) => { player.OnClick_J(); });
    }
    void ClearControl()
    {
        EventTrigger triggerR = controlBtns[0].GetComponent<EventTrigger>();
        triggerR.triggers[0].callback.RemoveAllListeners();
        triggerR.triggers[1].callback.RemoveAllListeners();
        EventTrigger triggerL = controlBtns[1].GetComponent<EventTrigger>();
        triggerL.triggers[0].callback.RemoveAllListeners();
        triggerL.triggers[1].callback.RemoveAllListeners();
        EventTrigger triggerJ = controlBtns[2].GetComponent<EventTrigger>();
        triggerJ.triggers[0].callback.RemoveAllListeners();
    }
    void triggerResult()
    {
        EventManager.Instance.SendEvent(State.GameEnd);
    }
    #endregion
}
