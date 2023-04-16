using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kernel.Data
{
    public class StaticData : IStaticData
    {
        private readonly Dictionary<Type, ScriptableObject> _cached;

        public StaticData()
        {
            _cached = new Dictionary<Type, ScriptableObject>();
        }

        public T Get<T>() where T : ScriptableObject
        {
            if (_cached.TryGetValue(typeof(T), out ScriptableObject value))
                return value as T;
            
            T newValue = Resources.LoadAll<T>("")[0];
            _cached.Add(typeof(T), newValue);
            
            return newValue;
        }
    }
}