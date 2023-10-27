using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState state { get; private set; }

    private void Start() => UpdateGameState(GameState.Starting);

    public void UpdateGameState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        state = newState;

        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Active:
                HandleActive();
                break;
            case GameState.MoveBack:
                HandleMoveBack();
                break;
            case GameState.Draging:
                HandleDraging();
                break;
            default: 
                break;
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting()
    {
        /// TODO:
        /// When a new window starts, it should 
    }

    private void HandleActive()
    {
        /// TODO: 
        /// When window active, move window in front of the screen

        UpdateGameState(GameState.Draging);
    }

    private void HandleMoveBack()
    {
        /// TODO:
        /// When a new window is set active, move every other window one position back
    }

    private void HandleDraging()
    {
        /// TODO: 
        /// When the mouse is holding the collider in a screen, let it drag the object
    }
}

public enum GameState
{
    Starting = 0,
    Active = 1,
    MoveBack = 2,
    Draging = 3,
}