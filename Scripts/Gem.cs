using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int posIndex;
    [HideInInspector]
    public Board board;

    private bool mousePressed;
    private float swipeAngle = 0;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;

    private Gem otherGem;

    public enum GemType {blue, green, red, yellow, checkMark}
    public GemType type;
    public bool isMatched;

    private Vector2Int previousPos;

    public GameObject destroyEffect;

    public int scoreValue = 100;

    private RoundManager roundMan;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
    roundMan = FindObjectOfType<RoundManager>();
    uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(board == null) {
        Debug.LogError("Board reference is null for Gem at position: " + posIndex);
        return;
    }


       if(Vector2.Distance(transform.position, posIndex) > .01f) {

        transform.position = Vector2.Lerp(transform.position, posIndex, board.gemSpeed * Time.deltaTime);

       } else {
        transform.position = new Vector3(posIndex.x, posIndex.y, 0f);
        board.allGems[posIndex.x, posIndex.y] = this;
       }
        
        
        if(mousePressed && Input.GetMouseButtonUp(0)) {
            mousePressed = false;
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        } 
    }

    public void SetupGem(Vector2Int pos, Board theBoard) {
        posIndex = pos;
        board = theBoard;

        if(board == null) {
        Debug.LogError("Board reference failed to be set for Gem at position: " + posIndex);
    }
    }

    private void OnMouseDown() {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePressed = true;
    }

    private void CalculateAngle() {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x);
        swipeAngle = swipeAngle * 180 / Mathf.PI;
        Debug.Log(swipeAngle);

        if(Vector3.Distance(firstTouchPosition, finalTouchPosition) > .5f) {
        
        MovePieces();

        }

    }

    private void MovePieces() {

        previousPos = posIndex;

        if(swipeAngle < 45 && swipeAngle > -45 && posIndex.x < board.width - 1) { //swiping to right
            otherGem = board.allGems[posIndex.x+1, posIndex.y];
            otherGem.posIndex.x--;
            posIndex.x++;
        } else if(swipeAngle > 45 && swipeAngle <= 135 && posIndex.y < board.height - 1) { 
            otherGem = board.allGems[posIndex.x, posIndex.y+1];
            otherGem.posIndex.y--;
            posIndex.y++;
        } else if(swipeAngle < -45 && swipeAngle >= -135 && posIndex.y > 0) { 
            otherGem = board.allGems[posIndex.x, posIndex.y-1];
            otherGem.posIndex.y++;
            posIndex.y--;
        } else if(swipeAngle > 135 || swipeAngle < -135 && posIndex.x > 0) { 
            otherGem = board.allGems[posIndex.x - 1, posIndex.y];
            otherGem.posIndex.x++;
            posIndex.x--;
        }

        board.allGems[posIndex.x, posIndex.y] = this;
        board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;
        roundMan.remainingMoves--;
        StartCoroutine(CheckMoveCo());

    }

   public IEnumerator CheckMoveCo() 
{
    yield return new WaitForSeconds(.5f);

    // Call FindAllMatches(), which updates the currentMatches list in matchFind
    board.matchFind.FindAllMatches();

    // Check if any gems in currentMatches have moved
    bool anyGemMoved = false;
    foreach (Gem gem in board.matchFind.currentMatches)
    {
        if (gem.posIndex != gem.previousPos)
        {
            anyGemMoved = true;
            break;
        }
    }

    if (otherGem != null) 
    {
        if (!isMatched && !otherGem.isMatched && !anyGemMoved) 
        {
            otherGem.posIndex = posIndex;
            posIndex = previousPos;

            board.allGems[posIndex.x, posIndex.y] = this;
            board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;
        } 
        else 
        {
            board.DestroyMatches();
        }
    }

    // If no matches are made, and there are no possible matches
    if (!isMatched && roundMan.remainingMoves > 0 && !board.matchFind.CheckPossibleMatch()) 
    {
        Debug.Log("Game Over! You Win!");
        uiManager.winScreen.SetActive(true);
    }
}

}
