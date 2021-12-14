using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OdinsTrials
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void StartInstructions()
        {
            SceneManager.LoadScene(3);
        }

        public void StartCredits()
        {
            SceneManager.LoadScene(4);
        }

        
    }
}