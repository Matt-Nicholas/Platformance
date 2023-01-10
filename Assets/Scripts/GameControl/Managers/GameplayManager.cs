using System.Collections.Generic;
using UnityEngine;


public class GameplayManager : MonoBehaviour
{
    public static Color startColor = Color.white;

    private Block[] blocks;
    private Dictionary<Color /*player color*/, int /*number of blocks player owns*/> score = new Dictionary<Color, int>();
    private bool winnerDeclared = false;

    void Start()
    {
        blocks = GetAllBlocks();
    }

    private Block[] GetAllBlocks()
    {
        return FindObjectsOfType(typeof(Block)) as Block[];
    }
    
    void Update()
    {
        if (blocks.Length == 0) return;

        // Set all blocks colored to true...
        bool levelComplete = AllBlocksColored();


        if (levelComplete && !winnerDeclared)
        {

            winnerDeclared = true;

            // count blocks of each color
            for (int i = 0; i < blocks.Length; i++)
            {
                if (!score.ContainsKey(blocks[i].Color))
                {
                    score[blocks[i].Color] = 0;
                }

                score[blocks[i].Color] += 1;
            }

            Color winner = startColor;

            int highestCount = 0;

            foreach (Color cColor in score.Keys)
            {
                if (score[cColor] > highestCount)
                {
                    highestCount = score[cColor];
                    winner = cColor;
                }
            }

            Game.Instance.ReportStageCompleted();
            Debug.Log(winner + "has won the game with a score of " + score[winner] + " blocks colored!");

        }
    }

    private bool AllBlocksColored()
    {

        if (blocks.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].Color == startColor || blocks[i].Color == Color.black)
            {
                return false;
            }
        }

        return true;
    }
}