using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OdinsTrials
{
    public class ReturnToMenu : MonoBehaviour
    {
        public void StartMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}