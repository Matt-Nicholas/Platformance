using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Tournament
{
    private readonly int _totalGames;
    private readonly string[] _stageNames;
    private int _stageNumber;

    public Tournament(int lenght)
    {
        _totalGames = lenght;
        _stageNames = new string[_totalGames];
    }
    public void StartTournament()
    {
        PopulateStageList();
        LoadNextStage();
        
        Game.Instance.OnStageCompleted += HandleStageCompleted;
    }
    

    private void PopulateStageList()
    {
        var tempStages = new List<string>(Game.Instance.GameSettings.Stages);

        for (int i = 0; i < _totalGames; i++)
        {
            System.Random random = new System.Random();
            var index = random.Next(0, _stageNames.Length);
            _stageNames[i] = tempStages[index];

            tempStages.RemoveAt(i);
        }
    }

    private void LoadNextStage()
    {
        if(_stageNumber >= _stageNames.Length)
        {
            Game.Instance.LoadMainMenu();
        }
        SceneManager.LoadScene(_stageNames[_stageNumber]);
        _stageNumber++;
    }

    private void HandleStageCompleted(int obj)
    {
        //TODO show tournament standings
        
        LoadNextStage();
    }
}
