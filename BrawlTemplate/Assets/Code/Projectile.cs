using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // set enmey layer and projectile layer

    public float speed = 60f;
    public float lifeTime = 0.5f;
    public float distance = 0.5f;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
      //  RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
     //   if(hitInfo.collider != null)
     //   {
      //      if (hitInfo.collider.CompareTag("Enemy"))
        //    {
       //         Debug.Log("Enemy Must take damage!");
        //    }

       //     DestroyProjectile();
     //   }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision != null)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Enemy Must take damage!");
            }

            DestroyProjectile();
        }

    }

    void DestroyProjectile()
    {
        // check to see if there is a destroy effect if not dont do it
        if(destroyEffect != null)
        Instantiate(destroyEffect, transform.position, Quaternion.identity);

        // destroy the projectile
        Destroy(gameObject);
    }
}
