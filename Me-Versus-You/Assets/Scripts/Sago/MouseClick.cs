using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject gunBullet;
    public Transform myFirePoint;

    public GameObject knifeBullet;
    public Transform myKnifePoint;
    void Start()
    {

    }
    void Update()
    {
        onMouseClickForBullet();
        onMouseClickForKnife();

    }
    void onMouseClickForBullet()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))   
        {

            Instantiate(gunBullet, myFirePoint.position, myFirePoint.rotation);


        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 6.5f;

        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
    void onMouseClickForKnife()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            Instantiate(knifeBullet, myKnifePoint.position, myKnifePoint.rotation);


        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 6.5f;

        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
