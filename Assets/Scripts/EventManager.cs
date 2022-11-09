using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class EventManager
{
    internal static Action<int> eventOnCoinsCollect;

    public static void CallOnCoinsUpdate(int coins_num)
    {
        eventOnCoinsCollect?.Invoke(coins_num);
    }
}
