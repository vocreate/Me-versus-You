using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateGun : MonoBehaviour {

    public GameObject myGun;
    public GameObject myKnife;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.E))
        {
            Vector3 mousePos = Input.mousePosition;
            Instantiate(myGun, mousePos, Quaternion.identity);
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 mousePos = Input.mousePosition;
            Instantiate(myKnife, mousePos, Quaternion.identity);

        }

    }
}
