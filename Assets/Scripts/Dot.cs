using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {
    public float swipeAngle = 0;
    public int row;
    public int column;
    public int targetX;
    public int targetY;

    private Board board;
    private GameObject otherDot;
    private Vector2 firstTouchPos;
    private Vector2 releaseTouchPos;
    private Vector2 tempPos;
   

	// Use this for initialization
	void Start () {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
	}
	
	void Update () {
        targetX = column;
        targetY = row;
        if (targetX != transform.position.x || targetY != transform.position.y)
        {
            tempPos = new Vector2(targetX, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
            board.allDots[column, row] = this.gameObject;
        }
        /*if(targetX != transform.position.x || targetY != transform.position.y)
        //if(Mathf.Abs(targetX - transform.position.x)> .1)
        {
            //Move towrads the target
            tempPos = new Vector2(targetX,targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);

        }
        */
        /*else
        {
            //Directly set the position
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = tempPos;
            board.allDots[column, row] = this.gameObject;
        }*/
	}
    private void OnMouseDown()
    {
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        releaseTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
        MoveDots();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2((releaseTouchPos.y - firstTouchPos.y), (releaseTouchPos.x - firstTouchPos.x))/Mathf.PI*180;
        Debug.Log(swipeAngle);
    }

    void MoveDots()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column<board.width-1) //moving right
        {
            otherDot = board.allDots[column + 1, row]; //get the right dot on the board
            otherDot.GetComponent<Dot>().column -= 1; // move the other dot to left
            column += 1; //move the current dot to the right
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row <board.height-1) //moving up
        {
            otherDot = board.allDots[column , row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }else if ( Mathf.Abs(swipeAngle)>=135 && column>0) //moving left
        {
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }else if (swipeAngle <-45 && swipeAngle >= -135 && row>0) //moving down
        {
            otherDot = board.allDots[column, row-1];
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
    }
}
