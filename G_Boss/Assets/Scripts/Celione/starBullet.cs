using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starBullet : MonoBehaviour {

    public float starBulletSpeed = 1f;
    private celione_Player Celione;
    public float Damage = 2f;

    void Start()
    {
        Celione = FindObjectOfType<celione_Player>();
        if (Celione.transform.localScale.x < 0) //SWITCH FOR MOUSE
            starBulletSpeed = -starBulletSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(starBulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
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
            col.SendMessageUpwards("soundDamage", "starHit");
            Destroy(gameObject);
        }
        else
        {
            Object.Destroy(gameObject, 2.0f);
        }
    }



}
