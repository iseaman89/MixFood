using UnityEngine;

public static class DataStore
{
    #region Variables
        
    private const string SavedLevelKey = "Level";
        
    #endregion

    #region Function
        
    public static void LoadGame() => 
        GameController.Instance.CurrentLevel = PlayerPrefs.GetInt(SavedLevelKey, 1);

    public static void SaveGame()
    {
        PlayerPrefs.SetInt(SavedLevelKey, GameController.Instance.CurrentLevel);

        PlayerPrefs.Save();
    }

    #endregion
}