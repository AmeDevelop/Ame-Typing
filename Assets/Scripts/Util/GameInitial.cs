using UnityEngine;

public class GameInitia
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(900, 720, false);

    }
}