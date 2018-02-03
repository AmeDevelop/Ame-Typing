using UnityEngine;

public class GameInitia
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1000, 800, false);

    }
}