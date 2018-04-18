using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFire : MonoBehaviour {

    //FIX BULLET COLLUSION WHEN FINISHED WITH HEALTH SCRIPT
    public float fireBulletSpeed = 1f;
    private celion_Player Celion;
    public float Damage = 2f;

    void Start()
    {
        Celion = FindObjectOfType<celion_Player>();
        if (Celion.transform.localScale.x < 0)//SWITCH FOR MOUSE
        {
            fireBulletSpeed = -fireBulletSpeed;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Celion.transform.localScale.x > 0)//SWITCH FOR MOUSE
        {

            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(fireBulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player" && col.tag != "Ground")

        {
            Destroy(gameObject);
        }
        if (col.isTrigger != true && col.CompareTag("Enemy"))
        {
            col.SendMessageUpwards("Damage", Damage);
            col.SendMessageUpwards("soundDamage", "bulletHit");
            Destroy(gameObject);
        }
        else
        {
            Object.Destroy(gameObject, 2.0f);
        }


    }

}
