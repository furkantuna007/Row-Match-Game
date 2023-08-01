using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_Text remainingText;
    public TMP_Text scoreText;

    public GameObject roundOverScreen;
    public GameObject winScreen;
    public RoundManager roundManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainingText.text =  "" + roundManager.remainingMoves;
    }
}
