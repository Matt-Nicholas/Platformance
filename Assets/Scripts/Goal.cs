
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

  [SerializeField]
  private string level = "";

	// Use this for initialization
	void Start () {

	}

  public void OnTriggerEnter2D(Collider2D collision) {
   
    if(collision.tag == "Player") {
      SceneManager.LoadScene(level);

    }

  }
  public void OnTriggerExit2D(Collider2D collision) {

  }

}
