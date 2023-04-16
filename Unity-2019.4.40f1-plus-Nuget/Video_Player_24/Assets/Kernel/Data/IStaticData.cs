using UnityEngine;

namespace Kernel.Data
{
    public interface IStaticData
    {
        T Get<T>() where T : ScriptableObject;
    }
}