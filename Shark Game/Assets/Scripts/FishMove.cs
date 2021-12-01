using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{


    [SerializeField]
    float moveSpeed = 2.0f; //how fast the fish moves

    [SerializeField]
    Transform topLeft; //world bounds top left
    [SerializeField]
    Transform bottomRight; //world bounds bottom right

    float randSwitch; //time before fish switch directions
    float direction; //the direction the fish is going (-1 or 1)

    [SerializeField]
    float charScale;


    bool canMove = true; //give the fish a little pause before switching direcitons

   private void Start()
   {
       randSwitch = Random.Range(0, 15.0f); //random time before direction switch
       float randDirection = Random.Range(-1, 1); //random direction
       if (randDirection >= 0)
       {
            direction = 1.0f; //backwards
       }
        else
        {
            direction = -1.0f; //forwards
        }

        StartCoroutine(directionSwitch()); //start the process
   }


    void Update()
    {


        if (canMove)
        {
            transform.position += new Vector3(direction , 0, 0) * Time.deltaTime * moveSpeed; //move fish using random values
        }


        if (direction < 0.01f) //for turing the sprite left or right
        {
            transform.localScale = Vector3.one * charScale;
        }
        else if (direction > -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1) * charScale;
        }




    }


    IEnumerator directionSwitch()
    {
        yield return new WaitForSeconds(randSwitch); //wait a set time before changing directions

        randSwitch = Random.Range(0, 15.0f); //random time before direction switch
        float pauseTime = Random.Range(0.5f, 5.0f); //random time before direction switch
        canMove = false; //pause before moving again    
        
        StartCoroutine(pause(pauseTime)); //pause for a small time

    }


    IEnumerator pause(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration); //pause
        direction *= -1; //switch direction
        canMove = true; //move again
        StartCoroutine(directionSwitch()); //move for a set amount of time
    }


    private void OnTriggerEnter2D(Collider2D collision) //for when the player hits the fish
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "good")
            {
                Debug.Log("You hit the good one!");
                Destroy(gameObject);

                GameManager.instance.goodFish();

            }
            else
            {
                Debug.Log("You hit the bad one!");
                Destroy(gameObject);

                GameManager.instance.badFish();
                collision.gameObject.GetComponent<SharkController>().sickShark();
            }
        }
            
    }
}
