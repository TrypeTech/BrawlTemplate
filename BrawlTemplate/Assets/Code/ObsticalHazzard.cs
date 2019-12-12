using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalHazzard : MonoBehaviour
{
    public float Speed = 5f;
    public float Damage = 22f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // calculate direction to hit player
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            other.gameObject.GetComponent<PlayerStats>().TakeDamage(Damage, hitDirection);

        }
    }
}
