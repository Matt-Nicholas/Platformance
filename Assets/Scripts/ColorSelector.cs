using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorSelector : MonoBehaviour
{
    private Color[] colors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.magenta,
        Color.cyan,
        Color.white,
        Color.black,
        Color.green,
        Color.green,
        Color.green
    };


    public ColorSelector otherSelector;
    public Text instructions;
    public GameObject buttonTrans;
    [SerializeField] private int playerNumber;
    [SerializeField] private Button[] buttons;
    private string chooseColorTxt = "Choose Your Color";
    private string joinTxt = "Press Start!";
    private int lockIndex = 2;
    private int cButtonIndex;
    private bool justMoved = false;
    private bool ready;
    private bool joined;
    private TheGameManager gameManager;

    private void Start()
    {
        gameManager = TheGameManager.Instance;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i > lockIndex) break;

            buttons[i].GetComponent<Image>().color = colors[i];
        }

        cButtonIndex = playerNumber - 1;
        buttons[cButtonIndex].Select();
        buttons[cButtonIndex].transform.localScale = new Vector3(1.35f, 1.35f, 1);

        if (playerNumber == 2)
            instructions.text = joinTxt;

        joined = (playerNumber == 1) ? true : false;
    }

    private void Update()
    {
        int startingIndex = cButtonIndex;
        int tempIndex = cButtonIndex;

        if (joined && !justMoved && !ready)
        {
            // Horizontal
            if (InputManager.MainHorizontal(gameManager.Players[playerNumber].controllerID) > 0.2f)
            {
                tempIndex += 1;

                if (tempIndex > lockIndex) tempIndex = lockIndex;
                else if (tempIndex >= buttons.Length) tempIndex = buttons.Length - 1;

                justMoved = true;
            }
            else if (InputManager.MainHorizontal(gameManager.Players[playerNumber].controllerID) < -0.2f)
            {
                tempIndex -= 1;

                if (tempIndex < 0) tempIndex = 0;
                justMoved = true;

            }

            // Vertical
            else if (InputManager.MainVertical(gameManager.Players[playerNumber].controllerID) < -0.2f)
            {
                int temp = tempIndex + 5;

                if (temp > lockIndex) temp = lockIndex;
                if (temp < buttons.Length)
                {
                    tempIndex = temp;
                    justMoved = true;
                }
            }
            else if (InputManager.MainVertical(gameManager.Players[playerNumber].controllerID) > 0.2f)
            {
                int temp = tempIndex - 5;

                if (temp >= 0)
                    tempIndex = temp;
                justMoved = true;
            }

            if (justMoved)
            {
                buttons[cButtonIndex].transform.localScale = new Vector3(1, 1, 1);

                if (tempIndex == otherSelector.cButtonIndex)
                {
                    if (tempIndex > startingIndex)
                    {
                        tempIndex = AdjustIndex(tempIndex, true);
                    }
                    else
                    {
                        tempIndex = AdjustIndex(tempIndex, false);
                    }
                }

                cButtonIndex = tempIndex;

                buttons[cButtonIndex].Select();
                buttons[cButtonIndex].transform.localScale = new Vector3(1.35f, 1.35f, 1);

                Invoke("ResetJustMoved", 0.15f);
            }
        }

        if (InputManager.AButtonDown(gameManager.Players[playerNumber].controllerID))
        {
            if (!joined)
            {
                buttonTrans.SetActive(true);
                instructions.text = chooseColorTxt;
                joined = true;
            }
            else
            {

                gameManager.Players[playerNumber].Color = colors[cButtonIndex];

                // tell game manager all players are ready
                ready = true;

                if (playerNumber == 1)
                {
                    SceneManager.LoadScene("Main");
                }
            }
        }
    }

    int AdjustIndex(int tempIndex, bool movingUp)
    {
        int i = tempIndex;
        if (movingUp)
        {
            if (i + 1 < buttons.Length && i + 1 <= lockIndex)
                i++;
            else
                i--;
        }
        else
        {
            if (i - 1 >= 0)
                i--;
            else
                i++;
        }

        return i;
    }

    void ResetJustMoved()
    {
        justMoved = false;
    }

    public void SetColor(string color)
    {
        Color temp = Color.white;

        switch (color)
        {

            case "red":
                temp = Color.red;
                break;

            case "blue":
                temp = Color.blue;
                break;

            case "green":
                temp = Color.green;
                break;

            case "yellow":
                temp = Color.yellow;
                break;

            case "magenta":
                temp = Color.magenta;
                break;
        }

        gameManager.Players[playerNumber].Color = temp;

    }

    public int CButtonIndex
    {
        get { return cButtonIndex; }
    }
}