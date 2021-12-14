using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinsTrials
{
    public class EnemySelector : MonoBehaviour
    {
        private int lastNumber;
        public int EnemyType0, EnemyType1, EnemyType2, EnemyType3;

        ChangePreview Preview0_0, Preview0_1, Preview1_0, Preview1_1;
        PlayerBehaviour Player;

        // Start is called before the first frame update
        void Start()
        {
            Player = FindObjectOfType<PlayerBehaviour>();

            Preview0_0 = GameObject.Find("Preview0_0").GetComponent<ChangePreview>();
            Preview0_1 = GameObject.Find("Preview0_1").GetComponent<ChangePreview>();

            Preview1_0 = GameObject.Find("Preview1_0").GetComponent<ChangePreview>();
            Preview1_1 = GameObject.Find("Preview1_1").GetComponent<ChangePreview>();

            RandomizeEnemies();
            DisplayPreview();
        }

        // Update is called once per frame
        void Update()
        {
            DisplayPreview();
        }

        public void RandomizeEnemies()
        {
            if (Player.stagesCleared > 3)
            {
                EnemyType0 = GetRandom(0, 4);
                EnemyType2 = GetRandom(0, 4);
            }
            else
            {
                EnemyType0 = GetRandom(0, 4);
                EnemyType1 = GetRandom(0, 4);
                EnemyType2 = GetRandom(0, 4);
                EnemyType3 = GetRandom(0, 4);
            }
        }

        void DisplayPreview()
        {
            Preview0_0.PreviewUpdate(EnemyType0);
            Preview1_0.PreviewUpdate(EnemyType2);

            if (Player.stagesCleared > 3)
            {
                Preview0_1.PreviewUpdate(EnemyType1);
                Preview1_1.PreviewUpdate(EnemyType3);
            }
            else
            {
                Preview0_1.PreviewUpdate(-1);
                Preview1_1.PreviewUpdate(-1);
            }
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