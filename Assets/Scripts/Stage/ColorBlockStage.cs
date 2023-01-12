using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ColorBlockStage : MonoBehaviour
{
    private Block[] _blocks;
    private Dictionary<Player, int> _score = new Dictionary<Player, int>();
    private bool _winnerDeclared;
    private int _totalBlockCount;
    private int _claimedBlockCount;
    
    void Awake()
    {
        _blocks = GetAllBlocks();
        _totalBlockCount = _blocks.Length;
        foreach (var block in _blocks)
        {
            block.Init();
            block.OnBlockClaimed += HandleBlockClaimed;
            block.OnBlockUnClaimed += HandleBlockExited;
        }
    }
    
    private Block[] GetAllBlocks()
    {
        return FindObjectsOfType(typeof(Block)) as Block[];
    }
    
    private void HandleBlockClaimed(Player player)
    {
        if (_winnerDeclared)
            return;
        
        if (!_score.TryAdd(player, 1))
        {
            _score[player]++;
        }
        
        var allBlocksClaimed = ++_claimedBlockCount >= _totalBlockCount;

        if (allBlocksClaimed && !_winnerDeclared)
        {
            _winnerDeclared = true;

            var orderedByCount = 
                _score.OrderBy(x => x.Value).
                ToDictionary(x => x.Key, x => x.Value);
            
            foreach (var kvp in orderedByCount)
            {
                print($"player: {kvp.Key} block: {kvp.Value}");
            }
            Game.Instance.ReportStageCompleted();
        }
    }

    private void HandleBlockExited(Player obj)
    {
        _claimedBlockCount--;
    }
}