using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{

    private Board board;
    public List<Gem> currentMatches = new List<Gem>();

    public void SetBoard(Board board)
    {
        this.board = board;
    }

    public void FindAllMatches()
{
    currentMatches.Clear();
    for (int x = 0; x < board.width; x++)
    {
        for (int y = 0; y < board.height; y++)
        {
            Gem currentGem = board.allGems[x, y];
            if (currentGem != null)
            {
                if (x > 0 && x < board.width - 1)
                {
                    Gem leftGem = board.allGems[x - 1, y];
                    Gem rightGem = board.allGems[x + 1, y];
                    if (leftGem != null && rightGem != null)
                    {
                        if (leftGem.type == currentGem.type && rightGem.type == currentGem.type)
                        {
                            currentGem.isMatched = true;
                            leftGem.isMatched = true;
                            rightGem.isMatched = true;

                            currentMatches.Add(currentGem);
                            currentMatches.Add(leftGem);
                            currentMatches.Add(rightGem);
                        }
                    }
                }

                if (y > 0 && y < board.height - 1)
                {
                    Gem aboveGem = board.allGems[x, y + 1];
                    Gem belowGem = board.allGems[x, y - 1];
                    if (aboveGem != null && belowGem != null)
                    {
                        if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type)
                        {
                            currentGem.isMatched = true;
                            aboveGem.isMatched = true;
                            belowGem.isMatched = true;

                            currentMatches.Add(currentGem);
                            currentMatches.Add(aboveGem);
                            currentMatches.Add(belowGem);
                        }
                    }
                }
            }
        }
    }

    if (currentMatches.Count > 0)
    {
        currentMatches = currentMatches.Distinct().ToList(); // make a new list with unique elements / no duplicates
    }
}

  public bool CheckPossibleMatch()
{
    for (int y = 0; y < board.height; y++)
    {
        for (int x = 0; x < board.width; x++)
        {
            Gem currentGem = board.allGems[x, y];
            if (currentGem != null)
            {
                // Check for potential horizontal matches
                if (x < board.width - 2)
                {
                    Gem rightGem1 = board.allGems[x + 1, y];
                    Gem rightGem2 = board.allGems[x + 2, y];
                    if (rightGem1 != null && rightGem2 != null)
                    {
                        if (currentGem.type == rightGem1.type && currentGem.type == rightGem2.type)
                        {
                            return true;
                        }
                    }
                }
                // Check for potential vertical matches
                if (y < board.height - 2)
                {
                    Gem aboveGem1 = board.allGems[x, y + 1];
                    Gem aboveGem2 = board.allGems[x, y + 2];
                    if (aboveGem1 != null && aboveGem2 != null)
                    {
                        if (currentGem.type == aboveGem1.type && currentGem.type == aboveGem2.type)
                        {
                            return true;
                        }
                    }
                }
            }
        }
    }
    return false; 
}


    private void SwapGems(Gem gem1, Gem gem2)
    {
        Gem.GemType tempType = gem1.type;
        gem1.type = gem2.type;
        gem2.type = tempType;

        bool tempMatched = gem1.isMatched;
        gem1.isMatched = gem2.isMatched;
        gem2.isMatched = tempMatched;

        FindAllMatches();
    }
}