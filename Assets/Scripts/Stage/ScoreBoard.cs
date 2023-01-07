using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    public void StartTimer()
    {
        _timer.StartTimer();
    }
}
