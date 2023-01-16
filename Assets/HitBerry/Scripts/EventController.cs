using System;
using UnityEngine;

public class EventController : MonoBehaviour
{
    #region Variables

    public static EventController Instance { get; private set; }

    public static event Action OnLevelRestarted;
    public static event Action OnNextLevel;
    public static event Action OnCoverOpened;
    public static event Action OnCoverClosed;
    public static event Action OnLevelDone;
    public static event Action<int, float> OnMixerShake;

    #endregion

    #region Functions

    private void Awake() => Instance = this;

    public void RestartLevel() => OnLevelRestarted?.Invoke();
    public void NextLevel() => OnNextLevel?.Invoke();
    public void OpenCover() => OnCoverOpened?.Invoke();
    public void CloseCover() => OnCoverClosed?.Invoke();
    public void LevelDone() => OnLevelDone?.Invoke();
    public void ShakeMixer(int loops, float frequence) => OnMixerShake?.Invoke(loops, frequence);

    #endregion
}