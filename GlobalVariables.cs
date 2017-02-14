using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

    public int gameType; // 1 = Addition, 2 = Multiplication, 3 = Subtraction, 4 = Division
    public int score = 0;
    public int numOfRounds;
    int numbersOnScreen;
    GameObject slider;

    // Use this for initialization
    void Awake()
    {
        gameType = 1;
        DontDestroyOnLoad(gameObject);
        slider = GameObject.Find("RoundsSlider");
    }

    void Update()
    {
        if (slider != null)
        {
            setNumRounds();
        }
    }

    

    public void selectGameType(int selection)
    {
        if (selection < 5 && selection >= 0)
        {
            this.gameType = selection;
        }
        else
        {
            this.gameType = 0;
        }
        
    }

    public void setScore(int scoreToAdd)
    {
        this.score = this.score + scoreToAdd;
    }

    public void setNumRounds()
    {
        int roundsToPlay;
        roundsToPlay = (int) slider.GetComponent<Slider>().value;
        if (roundsToPlay > 0 && roundsToPlay <= 100)
        {
            this.numOfRounds = roundsToPlay;
        }
        else
        {
            this.numOfRounds = 10;
        }
    }


}
