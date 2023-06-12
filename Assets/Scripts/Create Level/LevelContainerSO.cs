using UnityEngine;

[CreateAssetMenu(fileName = "New Level Container", menuName = "Level/Create new LevelContainer")]
public class LevelContainerSO : ScriptableObject
{
    [SerializeField] private LevelSettingSO[] _levels;
    public LevelSettingSO[] Levels => _levels;

}
