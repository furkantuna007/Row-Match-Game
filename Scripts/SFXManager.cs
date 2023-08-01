using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource gemSound, explodeSound, stoneSound, roundOverSound;

    public void PlayGemBreak()
    {
        gemSound.Stop();

      

        gemSound.Play();
    }

    public void PlayExplode()
    {
        explodeSound.Stop();

        
        explodeSound.Play();
    }

    public void PlayStoneBreak()
    {
        stoneSound.Stop();

        

        stoneSound.Play();
    }

    public void PlayRoundOver()
    {
        roundOverSound.Play();
    }
}
