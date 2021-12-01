using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 1.0f; //speed at which the player moves
    [SerializeField]
    float charScale = 0.5f; //scale of the character
   

    void Update()
    {
        float leftRight = Input.GetAxis("Horizontal"); //horizontal move input
        float upDown = Input.GetAxis("Vertical"); //vertical move imput

        if (leftRight > 0.01f) //for turing the sprite left or right
        {
            transform.localScale = Vector3.one * charScale ;
        }
        else if (leftRight < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1) * charScale;
        }



        transform.position += new Vector3(leftRight, upDown, 0) * Time.deltaTime * moveSpeed; //move the player
        
    }



    public void sickShark()
    {
        //this is where the code goes for animating the shark being sick
        Debug.Log("The shark is sick");
    }

}
