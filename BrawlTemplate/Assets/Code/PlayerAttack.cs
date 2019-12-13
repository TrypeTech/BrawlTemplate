using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    // NOTe: Needed for to work
    // Create a New layer call it Enemy and add your enemies to that layer
    // Create a NewEmpty Object and place it where you want your player to attack
    // add it to the attackPos and make sure it is on the playerObject that has a animator
    // Make sure the player animator attack trigger name is "Attack"
    // Ajust Attack range and you should see it in the editor the range
    // Add this script to the Object that has a animator

    // add layer called Enemy to  pushables and enemies
    // also add the tag Enemy to enimes an Pushable to pushables

    public KeyCode attackButton = KeyCode.Mouse0;
    private float timeBtwAttack = 0.3f;
    public float startTimeBtwAttack;
    public float AttackDelayTime = 0.3f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage = 10;
    Animator anim;

    public GameObject HitEffect;

    public float AnimationSpeed = 0.3f;

    public float enemyKnockBackStrenght = 4f;


   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(attackButton) || Input.GetKey(KeyCode.JoystickButton1))
            {
                //anim.speed = AnimationSpeed;
                anim.SetTrigger("Attack");
               
                //Invoke("DelayAndAttack", AttackDelayTime);
                DelayAndAttack();
            }

            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            // anim.speed = 1;
            timeBtwAttack -= Time.deltaTime;
        }
    }

    // properly time the attack to when it acctually hits the enemy
    public void DelayAndAttack()
    {

        if (HitEffect != null)
            Instantiate(HitEffect, attackPos.position, Quaternion.identity);

        Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].gameObject.tag == "Player" )
            {
                if (enemiesToDamage[i].gameObject == transform.gameObject)
                {
                    // do nothing
                }
                else
                {
                    Vector3 direction = enemiesToDamage[i].transform.position - transform.position;
                    direction.y = 0;
                    enemiesToDamage[i].GetComponent<PlayerStats>().TakeDamage(damage, direction);


                    // Add Force to enemy when hit
                    //   if (enemiesToDamage[i].gameObject.GetComponent<Rigidbody>() != null && enemiesToDamage[i].gameObject.GetComponent<PlayerStats>().Invencible == false)
                    //  {
                    //   Vector3 direction = enemiesToDamage[i].transform.position - transform.position;
                    //  direction.y = 0;
                    //   enemiesToDamage[i].gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * enemyKnockBackStrenght, ForceMode.Impulse);
                    // }
                }
            }
            else if (enemiesToDamage[i].gameObject.tag == "Pushable")
            {
                enemiesToDamage[i].gameObject.GetComponent<SimplePush>().PlayerHitObject();
            }
        }

        // do same for pushables


    }

    // displayes in the editor where the attack point of the player is
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
