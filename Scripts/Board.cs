using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Board : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject bgTilePrefab;

    public Gem[] gems;

    public Gem[,] allGems;

    public float gemSpeed;

    [HideInInspector]
    public MatchFinder matchFind;
    public GameObject checkMarkPrefab;

    private BoardLayout boardLayout;
    private Gem[,] layoutStore;

    
    public RoundManager roundMan;
    

    private void Awake() {
        matchFind = FindObjectOfType<MatchFinder>();
        matchFind.SetBoard(this);
        boardLayout = GetComponent<BoardLayout>();

    }

    // Start is called before the first frame update
    void Start()
    {
        allGems = new Gem[width, height];
        setUp();
        layoutStore = new Gem[width,height];

        
    }

    private void Update() {
        matchFind.FindAllMatches();
    }

     


    private void setUp() {

        if (boardLayout != null) {
            layoutStore = boardLayout.GetLayout();

        }
        for (int x = 0; x < width; x++ ) {
            for (int y = 0; y < height; y++) {
                Vector2 pos = new Vector2(x,y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BG Tile - " + x + ", " + y;

                if (layoutStore[x,y] != null) {

                    SpawnGem(new Vector2Int(x,y), layoutStore[x,y]);
                }
                else {
                int gemToUse = Random.Range(0, gems.Length);
                SpawnGem(new Vector2Int(x,y), gems[gemToUse]);
                }
            }
        }

    }

    public bool AreGemsExploding()
{
    foreach (Gem gem in allGems)
    {
        if (gem != null && gem.isMatched)
            return true;
    }

    return false;
}

private void SpawnGem(Vector2Int pos, Gem gemToSpawn) {
    Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0f), Quaternion.identity); 
    gem.transform.parent = this.transform;
    gem.name = "Gem - " + pos.x + "," + pos.y;
    allGems[pos.x,pos.y] = gem;

    if(this == null) {
        Debug.LogError("Board instance is null while spawning a gem at position " + pos);
    } else {
        gem.SetupGem(pos,this);
    }
}


private void DestroyMatchedGemAt(Vector2Int pos) {
    if (allGems[pos.x,pos.y] != null) {
        if (allGems[pos.x,pos.y].isMatched) {
            Instantiate(allGems[pos.x,pos.y].destroyEffect, new Vector2(pos.x,pos.y), Quaternion.identity);
            GameObject checkMark = Instantiate(checkMarkPrefab, new Vector2(pos.x, pos.y), Quaternion.identity);
            Debug.Log("CheckMark instantiated at " + checkMark.transform.position);
            checkMark.transform.parent = this.transform;
            Destroy(allGems[pos.x,pos.y].gameObject);
            allGems[pos.x,pos.y] = null;
            
        }
    }
}


public void DestroyMatches() {
    Debug.Log($"DestroyMatches - currentMatches count: {matchFind.currentMatches.Count}");
    int scoreSum = 0;
    for (int i = 0; i < matchFind.currentMatches.Count; i++) {
        if (matchFind.currentMatches[i] != null) {
            scoreSum += scoreCheck(matchFind.currentMatches[i]);
            scoreCheck(matchFind.currentMatches[i]);
            DestroyMatchedGemAt(matchFind.currentMatches[i].posIndex);
        }
    }
    roundMan.currentScore += scoreSum;
}

public int scoreCheck (Gem gemToCheck) {
    return gemToCheck.scoreValue;

}



}
