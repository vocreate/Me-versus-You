using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myKnife : MonoBehaviour {

    public float knifeSpeed = 1f;
    //private celion_Player Celion;
    public float Damage = 2f;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -knifeSpeed);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Enemy" && col.tag != "Ground")

        {
            Destroy(gameObject);
            
        }
        if (col.isTrigger != true && col.CompareTag("Player"))
        {
            col.SendMessageUpwards("Damage", Damage);
            col.SendMessageUpwards("soundDamage", "knifeHit");
            Destroy(gameObject);
        }
        else
        {
            Object.Destroy(gameObject, 2.0f);
        }

    }
}
