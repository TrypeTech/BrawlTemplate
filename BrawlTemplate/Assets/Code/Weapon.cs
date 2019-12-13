using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset = -90;

    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public KeyCode BombFireButton = KeyCode.F;

    Animator anim;


    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Update()
    {
        // Handles the Weapon rotation
       // Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition) - transform.position;
      //  float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
       // transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        // shoots bomb at Shoot position
        if (timeBtwShots <= 0)
        {
            if (Input.GetKeyDown(BombFireButton))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

 
}
