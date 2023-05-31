using UnityEngine;

public class LevelsContainer : MonoBehaviour
{
    [SerializeField] 
    private LevelSettings[] _levels;
    public LevelSettings[] Levels => _levels;
}