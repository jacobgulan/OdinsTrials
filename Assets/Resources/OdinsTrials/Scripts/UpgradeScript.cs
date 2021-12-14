using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OdinsTrials
{
    public class UpgradeScript : MonoBehaviour
    {
        private int lastNumber;
        int num0, num1;

        PlayerBehaviour Player;

        void Start()
        {
            Player = FindObjectOfType<PlayerBehaviour>();
            RandomizeChoices();
            DispalyChoices();
        }

        void RandomizeChoices()
        {
            num0 = GetRandom(0, 4);
            num1 = GetRandom(0, 4);
        }

        void DispalyChoices()
        {
            switch (num0)
            {
                case 0:
                    GameObject.Find("Choice0").GetComponentInChildren<Text>().text = "+10 Max Health";
                    break;
                case 1:
                    GameObject.Find("Choice0").GetComponentInChildren<Text>().text = "+5 Damage";
                    break;
                case 2:
                    GameObject.Find("Choice0").GetComponentInChildren<Text>().text = "+1 Life (Max 4)";
                    break;
                case 3:
                    GameObject.Find("Choice0").GetComponentInChildren<Text>().text = "-0.1s Attack Cooldown";
                    break;
            }

            switch (num1)
            {
                case 0:
                    GameObject.Find("Choice1").GetComponentInChildren<Text>().text = "+10 Max Health";
                    break;
                case 1:
                    GameObject.Find("Choice1").GetComponentInChildren<Text>().text = "+5 Damage";
                    break;
                case 2:
                    GameObject.Find("Choice1").GetComponentInChildren<Text>().text = "+1 Life (Max 4)";
                    break;
                case 3:
                    GameObject.Find("Choice1").GetComponentInChildren<Text>().text = "-0.1s Attack Cooldown";
                    break;
            }
        }

        public void Activate()
        {
            RandomizeChoices();
            DispalyChoices();
        }

        public void Choice0()
        {
            switch (num0)
            {
                case 0:
                    Player.maxHealth += 10;
                    break;
                case 1:
                    Player.attackDamage += 5;
                    break;
                case 2:
                    if (Player.lives <= 3)
                    {
                        Player.lives += 1;
                    }
                    break;
                case 3:
                    Player.attackCooldown -= 0.1f;
                    break;
            }
            Player.lastStageCleared += 1;
            GameObject.Find("Upgrade").SetActive(false);
        }

        public void Choice1()
        {
            switch (num1)
            {
                case 0:
                    Player.maxHealth += 10;
                    break;
                case 1:
                    Player.attackDamage += 5;
                    break;
                case 2:
                    if (Player.lives <= 3)
                    {
                        Player.lives += 1;
                    }
                    break;
                case 3:
                    Player.attackCooldown -= 0.1f;
                    break;
            }
            Player.lastStageCleared += 1;
            GameObject.Find("Upgrade").SetActive(false);
        }

        int GetRandom(int min, int max)
        {
            int rand = Random.Range(min, max);
            while (rand == lastNumber)
                rand = Random.Range(min, max);
            lastNumber = rand;
            return rand;
        }

    }
}
