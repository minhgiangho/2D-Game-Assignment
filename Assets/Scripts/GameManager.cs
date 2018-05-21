using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        /// <summary>
        /// Initialize any systems that need to be reset.
        /// </summary>
        Pregame,

        /// <summary>
        /// Unlock player, enemies and input in other system
        /// </summary>
        Running,

        /// <summary>
        /// Pause player, enemies etc, Lock other input in other systems
        /// </summary>
        Paused
    }

    /// <summary>
    /// GameManager keep check of other manager.
    /// </summary>
    public GameObject[] ManagerPrefabs;

    private List<GameObject> managerPrefabsList;

    private string currentLevelName = String.Empty;
    private List<AsyncOperation> loadOperations;
    private GameState currentGameState = GameState.Pregame;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }

    /// <summary>
    /// Event for game state transition
    /// </summary>
    public Events.EventGameState OnGameStateChanaged;

    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        managerPrefabsList = new List<GameObject>();

        loadOperations = new List<AsyncOperation>();
        InstantiateSystemPrefabs();
//        OnGameStateChanaged.Invoke(GameState.Pregame, currentGameState);
        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

   

    private void Update()
    {
        // TODO: may change to another script to manage global key (KeyManager maybe?
        // if we are in pregame mode 
        if (currentGameState == GameState.Pregame)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Load level according to scene name. like "Main"
    /// </summary>
    /// <param name="levelName">scene namne</param>
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);

        currentLevelName = levelName;
    }

    /// <summary>
    /// When scene load complete, it means GameState change from Pregame to Running
    /// </summary>
    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            if (loadOperations.Count == 0)
            {
                UpdateState(GameState.Running);
            }
        }

//        Debug.Log("Load Completed.");
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Completed.");
    }
    
    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(currentLevelName);
        }
        
    }

    /// <summary>
    /// Unload level according to scene name. like "Main"
    /// </summary>
    /// <param name="levelName"></param>
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < ManagerPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(ManagerPrefabs[i]);
            managerPrefabsList.Add(prefabInstance);
        }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;
        switch (CurrentGameState)
        {
            case GameState.Pregame:
                Time.timeScale = 1.0f;
                break;
            case GameState.Running:
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            default:
                break;
        }

        OnGameStateChanaged.Invoke(currentGameState, previousGameState);
    }

    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.Running ? GameState.Paused : GameState.Running);
    }

    public void RestartGame()
    {
        UpdateState(GameState.Pregame);
    }

    /// <summary>
    /// Load Level 1 scene
    /// </summary>
    public void StartGame()
    {
        LoadLevel("Main");
    }

    public void QuitGame()
    {
        Debug.Log("[GameManager] Quit Game.");
        Application.Quit();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < managerPrefabsList.Count; i++)
        {
            Destroy(managerPrefabsList[i]);
        }

        managerPrefabsList.Clear();
    }
}