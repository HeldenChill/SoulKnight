using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Dispatcher : Singleton<Dispatcher> 
    {
        private readonly Dictionary<EVENT_ID, Action> _listenerEventDictionary = new Dictionary<EVENT_ID, Action>();
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

        public void PostEvent(EVENT_ID eventID)
        {
            if (_listenerEventDictionary.TryGetValue(eventID, out Action value))
            {
                value.Invoke();
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
    }

}