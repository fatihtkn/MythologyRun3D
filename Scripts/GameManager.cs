using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>   
{

    public static event Action onCollectingPhaseEnded;
    public static event Action onGameStateChanged;
    public static event Action onGameFinished;
    //public static GameStates currentState;
    public bool Running_Phase;
    private   GameStates currentState;
    private void Start()
    {
        
    }
    public  GameStates CurrentState
    {
        get { return currentState; }
        set 
        { 
            currentState = value; 
            onGameStateChanged?.Invoke();
        }
    }

    public void EndCollectingPhase()
    {
        onCollectingPhaseEnded?.Invoke();
    }
    public static void EndGame()
    {
        onGameFinished?.Invoke();
    }
        
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
