using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Game Preferences")]
public class GamePreferences : ScriptableObject
{
    public Color PassiveTileColor;
    public Color BorderCubeColor;
    public List<Color> TileColors;
}
