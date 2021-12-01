using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    bool playing = true;

    #region score & time
    [SerializeField]
    Text scoreText; //the text for the score
    [SerializeField]
    Text timeText; //the text for the time

    int score = 0; //the score number
    int min = 5; //the minute number
    int sec = 0; //the second number


    string timing; //where the time text goes


    bool canCount = true; //stops counting down more than once a frame
    #endregion

    #region number of fish
    [SerializeField]
    Transform topLeft; //min x, max y
    [SerializeField]
    Transform bottomRight; //max x, min y

    int numBadFish = 1;
    int numGoodFish = 1;

    int maxBad = 10;
    int maxGood = 15;
    [SerializeField]
    GameObject goodFishPrefab;
    [SerializeField]
    GameObject badFishPrefab;

    #endregion

    static GameManager _instance = null; //gamemanager instance

    public static GameManager instance
    {
        get { return _instance; }
        set { instance = value; }
    }


    void Start()
    {
        //gamemanager stuff
        if (_instance) 
            Destroy(gameObject);
        else
        {
            _instance = this;

        }

        //end of game manager stuff

        while (numGoodFish < maxGood)
        {
            SpawnGoodFish();
        }

        while (numBadFish < maxBad)
        {
            SpawnBadFish();
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (canCount)
            StartCoroutine(waitASecond()); //count down a second
        
        if (sec < 10)
        {
            string secString = "0" + sec.ToString(); //adds a 0 before the single digit time

            timing = min.ToString() + ":" + secString; //set the time string variable
        } else
        {
            timing = min.ToString() + ":" + sec.ToString(); //set the time string variable
        }

        timeText.text = timing; // set the text box to this value



        if (playing)
        {
            if (numGoodFish < maxGood)
            {
                SpawnGoodFish();
            }

            if (numBadFish < maxBad)
            {
                SpawnBadFish();
            }
        }

        

    }



    public void SpawnGoodFish()
    {
        float maxX = bottomRight.position.x;
        float maxY = topLeft.position.y;
        float minX = topLeft.position.x;
        float minY = bottomRight.position.y;


        float tempX = Random.Range(minX, maxX);
        float tempY = Random.Range(minY, maxY);

        Vector3 randLocation = new Vector3(tempX, tempY, 0);

        Instantiate(goodFishPrefab, randLocation, Quaternion.identity);
        numGoodFish++;

    }

    public void SpawnBadFish()
    {
        float maxX = bottomRight.position.x;
        float maxY = topLeft.position.y;
        float minX = topLeft.position.x;
        float minY = bottomRight.position.y;


        float tempX = Random.Range(minX, maxX);
        float tempY = Random.Range(minY, maxY);

        Vector3 randLocation = new Vector3(tempX, tempY, 0);

        Instantiate(badFishPrefab, randLocation, Quaternion.identity);
        numBadFish++;

    }



    IEnumerator waitASecond()
    {
        canCount = false; //don't allow counting each frame
        yield return new WaitForSeconds(1);


        if (min == 0 && sec == 0)
        {
            //end game code
        }
        else
        {
            if (min == 0)
            {
                sec--; //take a second away
            }
            else
            {
                if (sec == 0)
                {
                    min--; //take a minute away
                    sec = 59; //set the seconds up to 59 after taking a minute away
                } else
                {
                    sec--; // take a second away
                }



            }
        }
        canCount = true; //reset the loop
    }


    public void goodFish()
    {
        score += 10; //add 10 points for hitting a good fish
        scoreText.text = score.ToString(); //set the score box

        numGoodFish--; //take away one good fish from count
    }    

    public void badFish()
    {
        score -= 10; //take 10 points away for hitting a bad fish
        score = Mathf.Clamp(score, 0, 1000000); //clamp the value to min 0
        scoreText.text = score.ToString(); //set the score box
        numBadFish--; //take away one bad fish from count
    }


    
}
