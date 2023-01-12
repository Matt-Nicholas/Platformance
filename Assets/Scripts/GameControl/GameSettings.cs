using UnityEngine;

public class GameSettings : MonoBehaviour
{
    
    public string[] Stages = {};
    public GameType[] GameTypes;

    public Color ColorBlockStartColor;

    public enum GameType
    {
        ColorBlocks,
        Sprint,
        
    }

    
}
