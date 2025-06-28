using System.Collections;
using System.Collections.Generic;
using MarkFramework;
using UnityEngine;

public class GameState : SingletonMono<GameState>
{
    public E_GameStateType currentGameType;
    
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}

public enum E_GameStateType
{
    E_BeforeStart,
    E_InGame,
    E_GameEnd
}
