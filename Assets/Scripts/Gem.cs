using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    //[HideInInspector]
    public Vector2Int posIndex;
    //[HideInInspector]
    public Board board;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;

    private bool mousePressed;
    private float swipeAngle = 0f;

    private Gem otherGem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mousePressed && Input.GetMouseButtonUp(0))
        {
            mousePressed = false;

            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    public void SetupGem(Vector2Int pos, Board theBoard)
    {
        posIndex = pos;
        board = theBoard;
    }

    private void OnMouseDown()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePressed = true;
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x);
        swipeAngle = swipeAngle * 180 / Mathf.PI;

        if (Vector3.Distance(firstTouchPosition, finalTouchPosition) > .5f)
        {
            MovePieces();
        }
    }

    void MovePieces()
    {
        if (swipeAngle < 45 && swipeAngle > -45 && posIndex.x < board.width - 1)
        {
            otherGem = board.allGems[posIndex.x + 1, posIndex.y];
            otherGem.posIndex.x--;
            posIndex.x++;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && posIndex.y < board.height - 1)
        {
            otherGem = board.allGems[posIndex.x, posIndex.y+1];
            otherGem.posIndex.y--;
            posIndex.y++;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && posIndex.y > 0)
        {
            otherGem = board.allGems[posIndex.x, posIndex.y - 1];
            otherGem.posIndex.y++;
            posIndex.y--;
        }
        else if (swipeAngle > 135 || swipeAngle < -135 && posIndex.x > 0)
        {
            otherGem = board.allGems[posIndex.x + 1, posIndex.y];
            otherGem.posIndex.x++;
            posIndex.x--;
        }

        board.allGems[posIndex.x, posIndex.y] = this;
        board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;
    }
}
