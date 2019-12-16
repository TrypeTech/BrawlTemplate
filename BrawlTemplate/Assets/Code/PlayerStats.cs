using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStats : MonoBehaviourPun
{
    public int CoinItems;
    public float MaxPlayerHealth = 100f;
    public float health;


    // Invencibility
     Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

    public float invincibilityLength = 1.2f;
    public float invincibilityCounter;

    Movement movement;

    // respawing
    public bool isRespawning;
    public Vector3 respawnPoint;
    public GameObject DieEffect;
    public float respawnTime = 2.5f;
    public MultipleTargetCamera cam;
    // Use this for initialization
    void Start()
    {

        // player starts at this position
        respawnPoint = transform.position;

        health = MaxPlayerHealth;
        movement = FindObjectOfType<Movement>();
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();
        cam = FindObjectOfType<MultipleTargetCamera>();

        //destroy the controller if the player is not controlled by me
     //   if (!photonView.IsMine && GetComponent<Movement>() != null)
        //    Destroy(GetComponent<Movement>());
    }

    // Update is called once per frame
    void Update()
    {

        // flicker Invincablity
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
            }

            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    public void AddCoins(int Amount)
    {
        CoinItems = CoinItems + 1;
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {

            health -= damage;

            if (health <= 0)
            {
                PlayerDie();
            }
            else
            {
                movement.animator.SetTrigger("TakeDamage");

                movement.KnockBack(direction);
                // invenciblity
                invincibilityCounter = invincibilityLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
        }
    }

    public void PlayerDie()
    {
        health = 0;
        playerRenderer.enabled = false;

        if(DieEffect != null)
        Instantiate(DieEffect, transform.position, transform.rotation);

        movement.canMove = false;
        Invoke("Respawn", respawnTime);
        //Respawn();
        Debug.Log("Player Had Died");
      foreach(Transform target in cam.Targets)
        {
            if(target.gameObject == transform.gameObject)
            {
                cam.Targets.Remove(target);
            }
        }
    }

    public void GainHealth(float healthAmount)
    {
        health += healthAmount;

        if (health > MaxPlayerHealth)
        {
            health = MaxPlayerHealth;
        }
    }

    public void Respawn()
    {
        // enable flash when respawning
        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;

        // reEnable everything
        movement.canMove = true;
        // Invoke("DelayMovement", 1.3f);
        playerRenderer.enabled = true;
        transform.position = respawnPoint;
        health = MaxPlayerHealth;
        cam.Targets.Add(transform);
    }

    public void DelayMovement()
    {

        movement.canMove = true;
    }

    /// photon stuff
    public static void RefreshInstance(ref PlayerStats player, PlayerStats Prefab)
    {
        var position = Vector3.zero;
        var rotation  = Quaternion.identity;
        if(player != null)
        {
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<PlayerStats>();
    }
}
