using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OdinsTrials
{

    public class ChangePreview : MonoBehaviour
    {

        public Sprite skeleSprite, goblinSprite, mushroomSprite, flyingSprite;
        private SpriteRenderer renderer;

        void Start()
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void PreviewUpdate(int num)
        {
            //Debug.Log("Preview Num:" + num);
            if (num == 0)
            {
                renderer.sprite = skeleSprite;
            }
            else if (num == 1)
            {
                renderer.sprite = goblinSprite;
            }
            else if (num == 2)
            {
                renderer.sprite = mushroomSprite;
            }
            else if (num == 3)
            {
                renderer.sprite = flyingSprite;
            }
            else
            {
                renderer.sprite = null;
            }
        }
        
    }
}