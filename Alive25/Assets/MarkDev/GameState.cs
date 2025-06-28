using System.Collections;
using System.Collections.Generic;
using MarkFramework;
using UnityEngine;

public class GameState : SingletonMono<GameState>
{
    public E_GameStateType currentGameType;
    public float Player1HP = 100;
    public float Player2HP = 100;
    public float Player1Energy = 10;
    public float Player2Energy = 10;
    
    private float PlayerMaxHP = 100;
    private float PlayerMaxEnergy = 10;
    
    
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
