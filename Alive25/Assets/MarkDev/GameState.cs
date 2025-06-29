using System.Collections;
using System.Collections.Generic;
using MarkFramework;
using UnityEngine;

public class GameState : SingletonMono<GameState>
{
    public E_GameStateType currentGameType;
    public float Player1HP = 100;
    public float Player2HP = 100;
    public int Player1Energy = 5;
    public int Player2Energy = 5;
    
    private float PlayerMaxHP = 100;
    private int PlayerMaxEnergy = 5;
    
    public float GetPlayerMaxHP()
    {
        return PlayerMaxHP;
    }
    
    public int GetPlayerMaxEnergy()
    {
        return PlayerMaxEnergy;
    }
}

public enum E_GameStateType
{
    E_BeforeStart,
    E_InGame,
    E_GameEnd
}
