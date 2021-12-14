using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OdinsTrials 
{ 
    public class HealthBar : MonoBehaviour
    {
        private Image Health_Bar;
        public float CurrHealth;
        private float MaxHP = 100f;
        PlayerBehaviour Player;

        private void Start()
        {
            Health_Bar = GetComponent<Image>();
            Player = FindObjectOfType<PlayerBehaviour>();
            
        }

        private void Update()
        {
            MaxHP = Player.maxHealth;
            CurrHealth = Player.health;
            Health_Bar.fillAmount = CurrHealth / MaxHP;
        }

    }
}