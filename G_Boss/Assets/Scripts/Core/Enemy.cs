using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float enemyMaxHealth = 100f;
    public float enemyCurrentHealth = 100f;
    public Image enemyHealthBar;
    public Image delayedHealthBar;
    private bool myShake;

    private float myShakeCd = .1f;
    private float myNextShake;
    public celion_Player celion;

    public GameObject otherObject;
    public GameObject otherObjects;
    Animator enemyHealthAnim;

    void Start () {
        enemyHealthAnim = otherObject.GetComponent<Animator>();
        celion = otherObjects.GetComponent<celion_Player>();
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if(myShake && Time.time > myNextShake)
        {
            enemyHealthAnim.SetBool("myShake", false);
        }

       
        delayedHealthBar.fillAmount = Mathf.Lerp(delayedHealthBar.fillAmount, enemyHealthBar.fillAmount, 2.75f * Time.deltaTime);

    }
    public void Damage(float Damage)
    {
        enemyCurrentHealth -= Damage;
        float totalHealth = enemyCurrentHealth / enemyMaxHealth;
        setHealth(totalHealth);
        enemyHealthAnim.SetBool("myShake", true);
        myShake = true;
        myNextShake = Time.time + myShakeCd;
        celion.audioManager.PlaySound("bulletHit");
        if (enemyCurrentHealth < 0)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }

    }
    public void soundDamage(string sound)
    {
        if(sound == "knifeHit")
        {
            celion.audioManager.PlaySound("knifeHit");
        }
        if (sound == "bulletHit")
        {
            celion.audioManager.PlaySound("bulletHit");
        }
        if(sound == "starHit")
        {
            celion.audioManager.PlaySound("starHit");
        }
    }
    void setHealth(float EnemyHealth)
    {
        enemyHealthBar.fillAmount = EnemyHealth;

    }
}
