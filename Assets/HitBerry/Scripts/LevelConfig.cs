using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "Levels", order = 0)]
public class LevelConfig : ScriptableObject
{
    public Color OrderColor;
    public int LevelNumber;
    public Food[] FoodPrefabs;
    public Vector3[] FoodPositions;
    public GameObject ClientPrefab;
}