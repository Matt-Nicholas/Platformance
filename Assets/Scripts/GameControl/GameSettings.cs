using UnityEngine;

public class GameSettings : MonoBehaviour
{
    
    public string[] Stages = {};
    public GameType[] GameTypes;


    public enum GameType
    {
        ColorBlocks,
        Sprint,
        
    }
}
