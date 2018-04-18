using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celione_Player : MonoBehaviour {

    //Movement Variables
    private float celioneVelocity;
    public float celioneSpeed = .5f;
    public float celioneJumpHeight = 2f;
    //private bool myFacingRight;

    //Gun Attack logic
    private float myGunCoolCD = .5f;
    public float myNextShot;
    public float myChargeTimer;
    public Transform myFirePoint1;
    public Transform myFirePoint2;
    public Transform myFirePoint3;
    public Transform myFirePoint4;
    public Transform myFirePoint5;
    //public Transform myFirePoint6;
    public GameObject gunBullet;

    //GUN CHARGE Animation
    public Renderer myCharge;

    //controller
    public GameObject celione;
    public celion_Player celion;
    public GameObject otherObjects;
    private Rigidbody2D celioneRigidBody2D;
    private Animator anim;
   
    //ground logic
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool myCelioneGrounded;
    private bool myCelioneCharging;
    private bool myCelioneAttacking;


    void Start ()
    {
        celion = otherObjects.GetComponent<celion_Player>();
        celioneRigidBody2D = gameObject.GetComponent<Rigidbody2D>(); //initiates rigidbody
        anim = gameObject.GetComponent<Animator>(); //initiates animator

    }
	
	// Update is called once per frame
	void Update () {
        CelioneMove();
        CelioneShoot();
        anim.SetBool("Grounded", myCelioneGrounded);
        anim.SetBool("Attacking", myCelioneAttacking);
        anim.SetBool("Charging", myCelioneCharging);
        myCelioneGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    void CelioneShoot()
    {
        if (Input.GetKey(KeyCode.J) && (Time.time > myNextShot))
        {
            myChargeTimer += Time.deltaTime;
            myCharge.enabled = true;
            myCelioneCharging = true;
            

            if (myChargeTimer > 1f)
            {
                myCharge.material.SetColor("_Color", Color.red);
            }
        }
        if (Input.GetKeyUp(KeyCode.J) && (myChargeTimer > 1f) && (Time.time > myNextShot))
        {
            myCelioneAttacking = true;
            celion.audioManager.PlaySound("starThrow");
            GameObject star1 = Instantiate(gunBullet, myFirePoint1.position, myFirePoint1.rotation) as GameObject;
            star1.GetComponent<starBullet>().Damage = 25f;
            star1.GetComponent<starBullet>().starBulletSpeed = 1.5f;

            GameObject star2 = Instantiate(gunBullet, myFirePoint2.position, myFirePoint2.rotation) as GameObject;
            star2.GetComponent<starBullet>().Damage = 25f;
            star2.GetComponent<starBullet>().starBulletSpeed = 1.3f;

            GameObject star3 =Instantiate(gunBullet, myFirePoint3.position, myFirePoint3.rotation) as GameObject;
            star3.GetComponent<starBullet>().Damage = 25f;
            star3.GetComponent<starBullet>().starBulletSpeed = 1.1f;

            GameObject star4 = Instantiate(gunBullet, myFirePoint4.position, myFirePoint4.rotation) as GameObject;
            star4.GetComponent<starBullet>().Damage = 25f;
            star4.GetComponent<starBullet>().starBulletSpeed = .9f;

            GameObject star5 = Instantiate(gunBullet, myFirePoint5.position, myFirePoint5.rotation) as GameObject;
            star5.GetComponent<starBullet>().Damage = 25f;
            star5.GetComponent<starBullet>().starBulletSpeed = .725f;

            /*GameObject star6 = Instantiate(gunBullet, myFirePoint6.position, myFirePoint6.rotation) as GameObject;
            star6.GetComponent<starBullet>().Damage = 10f;
            star6.GetComponent<starBullet>().starBulletSpeed = .675f;*/
            myNextShot = Time.time + myGunCoolCD;
        }
        if (Input.GetKeyUp(KeyCode.J) && (myChargeTimer < 1f) && (Time.time > myNextShot))
        {
            celion.audioManager.PlaySound("starThrow");
            myCelioneAttacking = true;
            myNextShot = Time.time + myGunCoolCD;
            
            GameObject star1 = Instantiate(gunBullet, myFirePoint1.position, myFirePoint1.rotation) as GameObject;
            star1.GetComponent<starBullet>().Damage = 12.5f;
            star1.GetComponent<starBullet>().starBulletSpeed = 1.5f;


            /*GameObject star2 = Instantiate(gunBullet, myFirePoint2.position, myFirePoint2.rotation) as GameObject;
            star2.GetComponent<starBullet>().Damage = 100f;
            star2.GetComponent<starBullet>().starBulletSpeed = 1.3f;

            GameObject star3 = Instantiate(gunBullet, myFirePoint3.position, myFirePoint3.rotation) as GameObject;
            star3.GetComponent<starBullet>().Damage = 100f;
            star3.GetComponent<starBullet>().starBulletSpeed = 1.1f;

            GameObject star4 = Instantiate(gunBullet, myFirePoint4.position, myFirePoint4.rotation) as GameObject;
            star4.GetComponent<starBullet>().Damage = 100f;
            star4.GetComponent<starBullet>().starBulletSpeed = .9f;*/

            GameObject star5 = Instantiate(gunBullet, myFirePoint5.position, myFirePoint5.rotation) as GameObject;
            star5.GetComponent<starBullet>().Damage = 12.5f;
            star5.GetComponent<starBullet>().starBulletSpeed = 1.1f;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            myCharge.enabled = false;
            myCharge.material.SetColor("_Color", Color.white);
            myChargeTimer = 0f;
            myCelioneCharging = false;



        }
    }
    void CelioneMove()
    {
        celioneVelocity = 0f;
        //Jump
        if (Input.GetKey(KeyCode.W) && myCelioneGrounded) // if grounded and not wallsliding then the player can jump
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, celioneJumpHeight); // dictates how high you jump

        }
        //Moves Character
        if (Input.GetKey(KeyCode.D))
        {
            celioneVelocity = celioneSpeed;
            //myFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            celioneVelocity = -celioneSpeed;
            //myFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);

        }
        celioneRigidBody2D.velocity = new Vector2(celioneVelocity, celioneRigidBody2D.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }
}
