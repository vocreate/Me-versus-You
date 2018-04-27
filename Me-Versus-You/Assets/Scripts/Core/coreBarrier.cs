using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreBarrier : MonoBehaviour {
   
    private float coreHealth = 1000f;

    public celion_Player celion;
    public GameObject otherObjects;


    void Start ()
    {

        celion = otherObjects.GetComponent<celion_Player>();

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, 0, 110 * Time.deltaTime);
		
	}
    public void Damage(float Damage)
    {
        coreHealth -= Damage;

        if (coreHealth < 0)
        {
            Destroy(gameObject);
        }
    }
    public void soundDamage(string sound)
    {
        if (sound == "bulletHit" || sound == "starHit")
        {
            celion.audioManager.PlaySound("barrierGuard");
        }
    }
}
