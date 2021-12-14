using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace OdinsTrials
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float damage;
        public float hp;
        public float maxHealth;
        public EnemyHealthBar healthbar;
        public NavMeshAgent agent;
        public Transform target;
        public Animator animator;

        private bool canAttack;
        private float moveSpeed;

        UpgradeScript Upgrade;
        PlayerBehaviour Player;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            Player = FindObjectOfType<PlayerBehaviour>();
            Upgrade = FindObjectOfType<UpgradeScript>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            animator.SetBool("isMoving", true);
            canAttack = true;
            
            RandomizeStats();

            healthbar.SetHealth(hp, maxHealth);
            animator.SetFloat("health", hp);
        }

        void Update()
        {
            //hammer.transform.localPosition = new Vector3(-0.2f, -0.2f, 0);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            PlayerBehaviour player = other.gameObject.GetComponent<PlayerBehaviour>();
            if (other.gameObject.tag=="Player")
            {
                if (canAttack) 
                {
                    player.takeDamage(damage);
                    StartCoroutine(AnimateAttack());
                    StartCoroutine(AttackCooldown());
                }
                
            }
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            Debug.Log(hp);
            if (hp <= 0)
            {
                StartCoroutine(AnimateDeath());
            } 
            else
            {
                healthbar.SetHealth(hp, maxHealth);
                animator.SetFloat("health", hp);
                StartCoroutine(AnimateHit());
            }
        }

        public void FlipDirection()
        {
            Vector3 currDir = (this.transform.position - target.transform.position).normalized;

            if (currDir.x < 0)
            {
                this.transform.localScale = new Vector3(2.5f, this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                this.transform.localScale = new Vector3(-2.5f, this.transform.localScale.y, this.transform.localScale.z);
            }
        }

        void RandomizeStats()
        {
            if (Player.stagesCleared < 3)
            {
                agent.speed = Random.Range(0.8f, 1.15f) * agent.speed;
                maxHealth = Random.Range(0.75f, 1.25f) * maxHealth;
            } 
            else
            {
                agent.speed = Random.Range(1f, 1.25f) * agent.speed;
                maxHealth = Random.Range(1.25f, 2f) * maxHealth;
            }

            moveSpeed = agent.speed;
            hp = maxHealth;
        }

        public void CreateNewStage()
        {
            Player.stagesCleared += 1;
            GameObject.Find("RoomSelect0").GetComponent<RoomSelect>().StageCleared();
            GameObject.Find("RoomSelect1").GetComponent<RoomSelect>().StageCleared();
        }

        IEnumerator AnimateHit()
        {
            animator.SetBool("isHit", true);
            agent.speed = 0;
            yield return new WaitForSeconds(0.3f);
            animator.SetBool("isHit", false);
            agent.speed = moveSpeed;
        }
        
        IEnumerator AnimateDeath()
        {
            healthbar.SetHealth(0, maxHealth);
            animator.SetFloat("health", -1);
            agent.speed = 0;
            Player.enemiesKilled += 1;
            if (Player.enemiesKilled % 6 == 0)
            {
                CreateNewStage();
                Player.health = Player.maxHealth;
            }
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }

        IEnumerator AnimateAttack()
        {
            animator.SetBool("isAttack1", true);
            agent.speed = 0;
            yield return new WaitForSeconds(0.6f);
            animator.SetBool("isAttack1", false);
            agent.speed = moveSpeed;
        }

        IEnumerator AttackCooldown()
        {
            canAttack = false;
            yield return new WaitForSeconds(0.8f);
            canAttack = true;
        }

        

    }
}
