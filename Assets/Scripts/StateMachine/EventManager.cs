using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameObject("EventManager").AddComponent<EventManager>();
                }
                return m_instance;
            }
        }
        static EventManager m_instance = null;

        readonly Dictionary<Enum, Action<EventBase>> handlerDictionary = new Dictionary<Enum, Action<EventBase>>();

        void Awake()
        {
            if (m_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                m_instance = this;
            }
        }
        public void SendEvent(Enum eid)
        {
            SendEvent(eid, null);
        }
        public void SendEvent(Enum eid, params object[] args)
        {
            try
            {
                if (handlerDictionary.ContainsKey(eid))
                {
                    handlerDictionary[eid].Invoke(args == null ? null : new EventData(args));
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }
        public void Registration(Enum e, Action<EventBase> action)
        {
            try
            {
                if (!handlerDictionary.ContainsKey(e))
                {
                    handlerDictionary.Add(e, action);
                }
                else
                {
                    handlerDictionary[e] += action;
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }
        public void Cancellation(Enum e, Action<EventBase> action)
        {
            try
            {
                if (handlerDictionary.ContainsKey(e))
                {
                    handlerDictionary[e] -= action;
                    if (handlerDictionary[e] == null)
                    {
                        handlerDictionary.Remove(e);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }
    }

    public abstract class EventBase
    {
    }

    public class EventData : EventBase
    {
        public object[] args;

        public EventData(params object[] args)
        {
            this.args = args;
        }
    }
}