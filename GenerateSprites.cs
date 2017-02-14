using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateSprites : MonoBehaviour {

    public GameObject[] NumberSprite;
    public List<GameObject> SpriteClones = new List<GameObject>();
    public Transform[] numberSpawnLoc;
    public Transform answerNumberLoc;
    public Transform scoreNumberLoc;
    public int answerIndex;
    public int firstNumber;
    public int secondNumber;
    public int firstSpawnLoc;
    public int secondSpawnLoc;
    public int objectIndex;
    public int randOne;
    public int randTwo;
    string spawnLocString1;
    string spawnLocString2;
    string spawnLocString3;
    string spawnLocString4;
    public bool isSelectedCheck1;
    public bool isSelectedCheck2;
    public int currentRound;
    public int numOfRounds;

    // Use this for initialization
    void Start () {
        setupScene();
        numOfRounds = GameObject.Find("GlobalVarsObj").GetComponent<GlobalVariables>().numOfRounds;
        currentRound = 1;
    }

    void Update ()
    {
        if (currentRound <= numOfRounds)
        {
            isSelectedCheck1 = GameObject.Find(spawnLocString1).GetComponent<spawnInteraction>().isSelected;
            isSelectedCheck2 = GameObject.Find(spawnLocString2).GetComponent<spawnInteraction>().isSelected;

            if (isSelectedCheck1 && isSelectedCheck2)
            {
                GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().increaseScore(1);
                GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().setNewRound();
                int score = GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().score;
                GameObject.Find(spawnLocString1).GetComponent<spawnInteraction>().resetIsSelected();
                GameObject.Find(spawnLocString2).GetComponent<spawnInteraction>().resetIsSelected();
                GameObject.Find(spawnLocString3).GetComponent<spawnInteraction>().resetIsSelected();//may not be needed if selection is limited to two
                GameObject.Find(spawnLocString4).GetComponent<spawnInteraction>().resetIsSelected();
                destroyGameSprites();
                if (score >= 10 && score < 20)//generate the target number in the top right - if it is >=10 it will split the sprite generation into two calls
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[score - 10], scoreNumberLoc.position, scoreNumberLoc.rotation)); //first sprite generation, single digit number < 10
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[1], scoreNumberLoc.position + Vector3.left, scoreNumberLoc.rotation));//second sprite generation, single digit number which is 1 to represent the tens column
                }
                else
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[score], scoreNumberLoc.position, scoreNumberLoc.rotation));// generate the sprite if the score number is < 10
                }
                currentRound = currentRound + 1;
                setupScene();
            }
        }
        else
        {
            destroyGameSprites();
            //send score data to global
        }
    }

    void SpawnNumbers ()
    {
        int i = 0;
        for (int spawnIndex = 0; spawnIndex < 4; spawnIndex++)//cycle through spawn locations to generate number sprites
        {
            if (spawnIndex == firstSpawnLoc)//check if the current index is equal to the randomly generated spawn location for the first number
            {
                if (firstNumber > 9)
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[firstNumber - 10], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));//generate sprite for first number
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[1], numberSpawnLoc[spawnIndex].position + Vector3.left, numberSpawnLoc[spawnIndex].rotation));
                }
                else
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[firstNumber], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));
                }
            }
            else if(spawnIndex == secondSpawnLoc)//check if the current index is equal to the randomly generated spawn location for the second number
            {
                if (secondNumber > 19)
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[secondNumber - 20], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));//generate sprite for first number
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[2], numberSpawnLoc[spawnIndex].position + Vector3.left, numberSpawnLoc[spawnIndex].rotation));
                }
                else if (secondNumber > 9 && secondNumber < 20)
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[secondNumber - 10], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));//generate sprite for first number
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[1], numberSpawnLoc[spawnIndex].position + Vector3.left, numberSpawnLoc[spawnIndex].rotation));
                }
                else
                {
                    SpriteClones.Add((GameObject)Instantiate(NumberSprite[secondNumber], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));
                }
            }
            else//used for the numbers that dont make the answer - randOne and randTwo
            {
                if (i == 0)
                {
                    SpriteClones.Add((GameObject) Instantiate(NumberSprite[randOne], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));//generate the sprite using the index to select spawn point
                    i++;
                    spawnLocString3 = "numberSpawn" + spawnIndex;
                    
                }
                else
                {
                    SpriteClones.Add((GameObject) Instantiate(NumberSprite[randTwo], numberSpawnLoc[spawnIndex].position, numberSpawnLoc[spawnIndex].rotation));//generate the sprite using the index to select spawn point
                    spawnLocString4 = "numberSpawn" + spawnIndex;
                }
           }
        }



        if (answerIndex >= 10 && answerIndex <20)//generate the target number in the top right - if it is >=10 it will split the sprite generation into two calls
        {
            SpriteClones.Add((GameObject) Instantiate(NumberSprite[answerIndex - 10], answerNumberLoc.position, answerNumberLoc.rotation));//first sprite generation, single digit number < 10
            SpriteClones.Add((GameObject) Instantiate(NumberSprite[1], answerNumberLoc.position + Vector3.left, answerNumberLoc.rotation));//second sprite generation, single digit number which is 1 to represent the tens column
        }
        else
        {
            SpriteClones.Add((GameObject) Instantiate(NumberSprite[answerIndex], answerNumberLoc.position, answerNumberLoc.rotation));// generate the sprite if the target number is < 10
        }
    }

    void checkRandom()
    {
        if (secondSpawnLoc == firstSpawnLoc)
        {
            secondSpawnLoc = Random.Range(0, 3);
            checkRandom();
        }
    }
    
    void setupScene()
    {
        answerIndex = GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().answer;
        firstNumber = GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().firstNumber;
        secondNumber = GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().secondNumber;
        randOne = GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().thirdNumber;
        randTwo = GameObject.Find("ShipDeckPH").GetComponent<mainSetupScript>().fourthNumber;
        firstSpawnLoc = Random.Range(0, 3);
        secondSpawnLoc = Random.Range(0, 3);
        checkRandom();
        spawnLocString1 = "numberSpawn" + firstSpawnLoc;
        spawnLocString2 = "numberSpawn" + secondSpawnLoc;
        SpawnNumbers();
    }

    void destroyGameSprites()
    {
        foreach (GameObject j in SpriteClones)
        {
            Destroy(j);
        }
    }
}
