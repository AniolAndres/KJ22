
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelList", menuName = "ScritpableObject/LevelList", order = 100)]
public class LevelProvider : ScriptableObject
{
    [SerializeField]
    private List<LevelController> levels;

    public LevelController GetLevel(int index) {
        if (index >= levels.Count) {
            return null;
        }

        return levels[index];
    }
}
