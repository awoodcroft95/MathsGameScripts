using UnityEngine;
using System.Collections;

public class mainSetupScript : MonoBehaviour {
    public int firstNumber;
    public int secondNumber;
    public int thirdNumber;
    public int fourthNumber;
    public int gameMode;
    public int score;
    public int answer;
    public bool newRound;

    // Use this for initialization
    void Start () {
        gameMode = GameObject.Find("GlobalVarsObj").GetComponent<GlobalVariables>().gameType;
        if (gameMode == 1)
        {
            gameSetup();
            setScore(GameObject.Find("GlobalVarsObj").GetComponent<GlobalVariables>().score);
            newRound = false;
        }
        else if (gameMode == 2)
        {
            gameSetup();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (newRound == true)
        {
            newRound = false;
            gameSetup();
            for (int i = 0; i < 4; i++)
            {
                string numSpawnLoc = "numberSpawn" + i;
                GameObject.Find(numSpawnLoc).GetComponent<spawnInteraction>().resetSpriteCall();
            }
        }
	}

    void gameSetup()
    {
        thirdNumber = Random.Range(0, 9);
        fourthNumber = Random.Range(0, 9);
        if (gameMode == 1)
        {
            generateNumbersUnder10();
        }
        else
        {
            generateMultNums();
        }
        
    }

    void generateNumbersUnder10()
    {
        firstNumber = Random.Range(0, 10);
        answer = Random.Range(0, 10) + firstNumber;
        secondNumber = answer - firstNumber;
        genRandNums();
    }

    void generateMultNums()
    {
        answer = Random.Range(2, 20);
        int try1 = Random.Range(1, 10);
        if ((answer % try1) == 0)
        {
            firstNumber = try1;
            secondNumber = answer / firstNumber;
        }
        else
        {
            generateMultNums();
        }
    }

    void genRandNums()//genrates the numbers the sprites will use
    {
        
        if ((thirdNumber + fourthNumber) == answer)
        {
            thirdNumber = Random.Range(0, 9);
            genRandNums();
        }
        if (thirdNumber == firstNumber || thirdNumber == secondNumber || thirdNumber == fourthNumber)
        {
            thirdNumber = Random.Range(0, 9);
            genRandNums();
        }
        if (fourthNumber == firstNumber || fourthNumber == secondNumber)
        {
            fourthNumber = Random.Range(0, 9);
            genRandNums();
        }
    }

    void genRandNumsMult()
    {
        if ((thirdNumber * fourthNumber) == answer)
        {
            thirdNumber = Random.Range(0, 9);
            genRandNums();
        }
        if (thirdNumber == firstNumber || thirdNumber == secondNumber || thirdNumber == fourthNumber)
        {
            thirdNumber = Random.Range(0, 9);
            genRandNums();
        }
        if (fourthNumber == firstNumber || fourthNumber == secondNumber)
        {
            fourthNumber = Random.Range(0, 9);
            genRandNums();
        }
    }

    public void increaseScore(int scoreToAdd)
    {
        this.score = score + scoreToAdd;
    }

    public void setNewRound()
    {
        this.newRound = true;
    }

    void setScore(int scoreFromGlobal)
    {
        this.score = scoreFromGlobal;
    }
}
