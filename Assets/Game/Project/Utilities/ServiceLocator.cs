using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public abstract class ServiceLocator<T> : Singleton<ServiceLocator<T>>
    {
        protected List<T> service;
        public abstract void InitService();
        public abstract T GetService(int id);
    }
}