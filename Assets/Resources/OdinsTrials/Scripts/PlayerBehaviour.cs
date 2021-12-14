using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OdinsTrials
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private Animator animator; // animations
        private bool isAttackReady; // works with attackCooldown
        private SpriteRenderer renderer;
        private bool isMovingLeft = false;

        public float speed;
        public int lives;
        public float health = 100f;
        public float maxHealth = 100f;
        public float attackDamage = 10f;
        public float attackCooldown = 1f;
        public int stagesCleared = 0;
        public int enemiesKilled = 0;
        public int lastStageCleared = 0;
        public int lastLife = 3;
        public Animator playerAnimator;
        public GameObject UpgradeButton;
        
        UpgradeScript Upgrade;
        Transform spawnPoint;
        Hammer hammer;

        // Maintains list of enemies touching Mjolnir
        private List<Collider2D> EnemiesTouchingHammer = new List<Collider2D>();


        private void Start()
        {
            animator = GetComponent<Animator>();
            spawnPoint = GameObject.Find("SpawnPoint").transform;
            health = 100f;
            lives = 3;
            isAttackReady = true;
            hammer = FindObjectOfType<Hammer>();
            UpgradeButton.SetActive(false);

            GameObject disableLife = GameObject.Find("Life3");
            disableLife.SetActive(false);
        }


        private void Update()
        {
            playerMovement();
            if (Input.GetKeyDown("space"))
            {
                if (isAttackReady)
                {
                    playerAttack();
                }
            }

            if (stagesCleared == 8)
            {
                SceneManager.LoadScene(5);
            }

            if (stagesCleared > lastStageCleared)
            {
                UpgradeButton.SetActive(true);
            }

        }

        private void playerMovement()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                //this.transform.localScale = new Vector3(-0.5f, this.transform.localScale.y, this.transform.localScale.z);
                animator.SetBool("isWalkingLeft", true);
                hammer.transform.localPosition = new Vector3(-0.2f, -0.2f, 0);
                //animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                //this.transform.localScale = new Vector3(0.5f, this.transform.localScale.y, this.transform.localScale.z);
                animator.SetBool("isWalkingLeft", false);
                hammer.transform.localPosition = new Vector3(0.2f, -0.2f, 0);
                //animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                //animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                //animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            //animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

        public void takeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                if (lives > 0)
                {
                    respawn();
                    string nameToFind = "Life" + lives;
                    GameObject life = GameObject.Find(nameToFind);
                    life.SetActive(false);
                }

                else
                {
                    SceneManager.LoadScene(2);
                }
            }
        }

        private void respawn()
        {
            lives--;

            health = maxHealth;

            transform.position = spawnPoint.position;
        }

        // Only destroy enemies
        private void playerAttack()
        {
            StartCoroutine(AttackCooldown());
            for (int i = 0; i < EnemiesTouchingHammer.Count; i++)
            {
                if (EnemiesTouchingHammer[i] != null && EnemiesTouchingHammer[i].gameObject.tag == "Enemy")
                {
                    if (EnemiesTouchingHammer[i].gameObject != null)
                    {
                        EnemyBehaviour enemy = EnemiesTouchingHammer[i].gameObject.GetComponent<EnemyBehaviour>();
                        enemy.TakeDamage(attackDamage);
                    }
                }
            }
        }

        // Basic attack cooldown
        private IEnumerator AttackCooldown()
        {
            isAttackReady = false;
            animator.SetBool("isSwinging", true);
            yield return new WaitForSeconds(attackCooldown);
            animator.SetBool("isSwinging", false);
            isAttackReady = true;
           
        }


        public void HammerColliding(Collider2D collisionObj, bool hasLeft)
        {
            if (!hasLeft)
            {
                if (!EnemiesTouchingHammer.Contains(collisionObj)) { EnemiesTouchingHammer.Add(collisionObj); }
            }
            else
            {
                if (EnemiesTouchingHammer.Contains(collisionObj)) { EnemiesTouchingHammer.Remove(collisionObj); }
            }
        }

        public void ShowUpgrade()
        {
            GameObject.Find("Life3").SetActive(true);
            //Upgrade.GetComponent<UpgradeScript>().Activate();
        }

    }
}
