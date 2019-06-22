using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor { PURPLE, RED, BLUE, YELLOW }

public class PlayerColorManager : MonoBehaviour
{
    [SerializeField]
    private bool autoAssignColorsToPlayers;

    [SerializeField]
    private Color purpleColorField;
    [SerializeField]
    private Color redColorField;
    [SerializeField]
    private Color blueColorField;
    [SerializeField]
    private Color yellowColorField;

    private static Color purpleColor;
    private static Color redColor;
    private static Color blueColor;
    private static Color yellowColor;

    private static Dictionary<int, PlayerColor> IDToColorMap;

    private void Awake()
    {
        // Cannot serialize static fields, but need fields to be static for static method
        // So assign static fields to be equal to serialized fields
        purpleColor = purpleColorField;
        redColor = redColorField;
        blueColor = blueColorField;
        yellowColor = yellowColorField;

        IDToColorMap = new Dictionary<int, PlayerColor>(4);

        if (autoAssignColorsToPlayers)
        {
            AssignColorToPlayer(0, PlayerColor.PURPLE);
            AssignColorToPlayer(1, PlayerColor.RED);
            AssignColorToPlayer(2, PlayerColor.BLUE);
            AssignColorToPlayer(3, PlayerColor.YELLOW);
        }
    }

    public static void AssignColorToPlayer(int playerID, PlayerColor color)
    {
        IDToColorMap.Add(playerID, color);
    }

    public static PlayerColor GetPlayerColor(int playerID)
    {
        return IDToColorMap[playerID];
    }

    public static Color GetRGBAColor(int playerID)
    {
        return GetRGBAColor(GetPlayerColor(playerID));
    }

    public static Color GetRGBAColor(PlayerColor playerColor)
    {
        switch (playerColor)
        {
            case PlayerColor.PURPLE:
                return purpleColor;
            case PlayerColor.RED:
                return redColor;
            case PlayerColor.BLUE:
                return blueColor;
            case PlayerColor.YELLOW:
                return yellowColor;
            default:
                return Color.black;
        }
    }
}
