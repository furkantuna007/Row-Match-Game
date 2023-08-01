using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public string levelToLoad;


    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void loadLevel() {
    SceneManager.LoadScene(levelToLoad);
   }
   
}
