using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinsTrials
{
    // Class for Mjolnir
    public class Hammer : MonoBehaviour
    {
        private bool isColliding;

        void OnTriggerEnter2D(Collider2D other)
        {
           transform.parent.GetComponent<PlayerBehaviour>().HammerColliding(other, false);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            transform.parent.GetComponent<PlayerBehaviour>().HammerColliding(other, true);
        }
    }
}