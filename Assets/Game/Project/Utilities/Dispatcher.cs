using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [DefaultExecutionOrder(-100)]
    public class Dispatcher : SingletonPersistent<Dispatcher> 
    {
        private readonly Dictionary<EVENT_ID, Action> _listenerEventDictionary = new Dictionary<EVENT_ID, Action>();
        private readonly Dictionary<EVENT_ID, Action<object>> _listenerParamEventDictionary = new Dictionary<EVENT_ID, Action<object>>();
        private readonly Dictionary<EVENT_ID, object> _lastParam = new Dictionary<EVENT_ID, object>();
        public void RegisterListenerEvent(EVENT_ID eventID, Action callback)
        {
            if (_listenerEventDictionary.ContainsKey(eventID))
            {
                _listenerEventDictionary[eventID] += callback;
            }
            else
            {
                _listenerEventDictionary.Add(eventID, callback);
            }
        }
        public void RegisterListenerEvent(EVENT_ID eventID, Action<object> callback)
        {
            if (_listenerParamEventDictionary.ContainsKey(eventID))
            {
                _listenerParamEventDictionary[eventID] += callback;
            }
            else
            {
                _listenerParamEventDictionary.Add(eventID, callback);
                _lastParam.Add(eventID, null);
            }
        }

        public void UnregisterListenerEvent(EVENT_ID eventID, Action callback)
        {
            if (_listenerEventDictionary.ContainsKey(eventID))
            {
                _listenerEventDictionary[eventID] -= callback;
            }
            else
            {
                Debug.LogWarning("EventID " + eventID + " not found");
            }
        }
        public void UnregisterListenerEvent(EVENT_ID eventID, Action<object> callback)
        {
            if (_listenerParamEventDictionary.ContainsKey(eventID))
            {
                _listenerParamEventDictionary[eventID] -= callback;
            }
            else
            {
                Debug.LogWarning("EventID " + eventID + " not found");
            }
        }
        public void PostEvent(EVENT_ID eventID)
        {
            if (_listenerEventDictionary.TryGetValue(eventID, out Action value))
            {
                value?.Invoke();
            }
            else
            {
                Debug.LogWarning("EventID " + eventID + " not found");
            }
        }

        public object GetLastParamEvent(EVENT_ID eventID)
        {
            if (_lastParam.TryGetValue(eventID, out object value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning("EventID " + eventID + " not found");
            }
            return null;
        }
        public void PostEvent(EVENT_ID eventID, object data)
        {
            if (_listenerParamEventDictionary.TryGetValue(eventID, out Action<object> value))
            {
                value?.Invoke(data);
                _lastParam[eventID] = data;
            }
            else
            {
                Debug.LogWarning("EventID " + eventID + " not found");
            }
        }

        public void ClearAllListenerEvent()
        {
            _listenerEventDictionary.Clear();
        }
    }

    public enum EVENT_ID
    {
        MAP_UPDATE = 0,
        PLAYER_GRID_POS_UPDATE = 1,     
        ENEMY_COUNT_CHANGE = 2,
    }

}