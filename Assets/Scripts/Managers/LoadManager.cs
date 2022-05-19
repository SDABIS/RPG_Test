using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadManager : Singleton<LoadManager>
{
    #region Declarations

    private string _currentLevel = string.Empty;
    public string CurrentLevel => _currentLevel;
    private List<AsyncOperation> _loadOperations = new List<AsyncOperation>();

    public UnityEvent OnLoadComplete;

    #endregion
    
    #region Load

    private void OnLoadOperationComplete(AsyncOperation ao) {
        if(_loadOperations.Contains(ao)) {
            _loadOperations.Remove(ao);

            if(_loadOperations.Count == 0) OnLoadComplete.Invoke();
        }
        Debug.Log("Load Complete");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentLevel));
    }

    private void OnUnloadOperationComplete(AsyncOperation ao) {
        Debug.Log("Unload Complete");
    }

    public void LoadLevel(string levelName) {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if(ao == null) {
            Debug.LogError("Unable to load level " + levelName + ".");
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevel = levelName;
    }

    public void UnloadLevel(string levelName) {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if(ao == null) {
            Debug.LogError("Unable to unload level " + levelName + ".");
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    #endregion

}
