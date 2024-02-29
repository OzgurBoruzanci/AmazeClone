using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PuzzleCreator : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private List<TileController> puzzleCubes;
    [SerializeField] private List<Border> borderCubes;
    private GamePreferences gamePreferences;

    private void OnEnable()
    {
        EventManager.GetPuzzlePercentage += GetPuzzlePercentage;
        EventManager.CreateLevel += CreateLevelPuzzle;
        EventManager.PuzzleCubeFilled += RemoveLevelElements;
    }

    private void OnDisable()
    {
        EventManager.GetPuzzlePercentage -= GetPuzzlePercentage;
        EventManager.CreateLevel -= CreateLevelPuzzle;
        EventManager.PuzzleCubeFilled-= RemoveLevelElements;
    }
    private float GetPuzzlePercentage()
    {
        var puzzleCubeAmount = puzzleCubes.Count;
        var filledAmount = 0;

        foreach (var cube in puzzleCubes)
        {
            if (cube.state == TileCubeStates.Filled) filledAmount++;
        }
        var percentage = (100 * filledAmount) / puzzleCubeAmount;
        return percentage;

    }

    private void Start()
    {
        gamePreferences = EventManager.GetGamePreferencesScriptable();
    }

    public void CreateLevelPuzzle()
    {
        var tilePassiveColor = gamePreferences.PassiveTileColor;
        var tileColorsCount = gamePreferences.TileColors.Count - 1;
        var tileColor = gamePreferences.TileColors[((int)Random.Range(0, tileColorsCount))];
        foreach (var item in puzzleCubes)
        {
            DestroyImmediate(item);
        }

        puzzleCubes.Clear();

        levelIndex = EventManager.GetLevelIndex().GetLevelIndex();
        var levelSprite = EventManager.GetLevelDataScriptable().GetLevelData()[levelIndex];
        EventManager.LevelSprites(levelSprite); // buras� setative false ile hata al�yor.counterde
        var borderCubeColor = gamePreferences.BorderCubeColor;
        var width = levelSprite.width;
        var height = levelSprite.height;

        var puzzleParent = transform.GetChild(0);
        var borderParent = transform.GetChild(1);

        var border = Vector3.zero;
        border.x = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f) / 2;
        border.z = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).z - .5f) / 2;
        border *= .9f;
        var xScale = 1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var color = levelSprite.GetPixel(x, y);
                
                if (color.a == 0)
                {
                    var cube = Instantiate(borderPrefab, new Vector3((x * xScale) - ((width * xScale) / 2) + xScale / 2, xScale, (y * xScale) - ((width * xScale) / 2)), Quaternion.identity, borderParent).AddComponent<Border>();

                    cube.transform.localScale = new Vector3(xScale, xScale, xScale);
                    cube.SetCube(borderCubeColor,tileColor);
                    borderCubes.Add(cube);
                }
                else
                {
                    var cube = Instantiate(tilePrefab, new Vector3((x * xScale) - ((width * xScale) / 2) + xScale / 2, 0, (y * xScale) - ((width * xScale) / 2)), Quaternion.identity, puzzleParent).AddComponent<TileController>();
                    cube.transform.localScale = new Vector3(xScale, xScale, xScale);
                    
                    cube.SetCube(tilePassiveColor,tileColor);
                    puzzleCubes.Add(cube);
                }
            }

        }
        EventManager.BallStartPos(puzzleCubes[0].transform.position);
    }

    private void RemoveLevelElements()
    {
        foreach (Transform puzzle in transform.GetChild(0))
        {
            Destroy(puzzle.gameObject);
        }
        foreach (Transform border in transform.GetChild(1))
        {
            Destroy(border.gameObject);
        }
        borderCubes.Clear();
        //puzlecubes de silinebilir
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(PuzzleCreator))]
public class PlayerDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PuzzleCreator playerData = (PuzzleCreator)target;
        if (GUILayout.Button("Create Puzzle"))
        {
            playerData.CreateLevelPuzzle();
        }
    }
}
#endif