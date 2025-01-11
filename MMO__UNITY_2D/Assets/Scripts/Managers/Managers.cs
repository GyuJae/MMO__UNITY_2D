using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _sInstance;
    static Managers Instance { get { Init(); return _sInstance; } }

    #region Contents
    readonly MapManager _mapManager = new();
    public static MapManager Map { get { return Instance._mapManager; } }
    #endregion

    #region Core
    readonly ResourceManager _resource = new();
    readonly PoolManager _pool = new();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    #endregion

    static void Init()
    {
        if (_sInstance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _sInstance = go.GetComponent<Managers>();
        }
    }

    public static void Clear()
    {
        Pool.Clear();
    }
}
