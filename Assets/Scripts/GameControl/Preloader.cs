using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<Game>() == null) SceneManager.LoadScene("_preload");
    }
}