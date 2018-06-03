using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class celion_Player : MonoBehaviour {
    /* TO DO LIST
   
    -----------------------------MISCELLANEOUS-----------------------------------
    **CLEAR** CLEAR* HEALTH UI (WORLD SPACE)                              **CLEAR**
    **CLEAR** Finish Health system                                        **CLEAR**
    **CLEAR** Work on Sounds (CHIPTUNE)                                   **CLEAR**
    **CLEAR** Work on Animator wiring (ATTACKS)                           **CLEAR**
    -----------------------------SAGO--------------------------------------------
     ** CLEAR  Work on Mouse instantiation                                **CLEAR**
     **CLEAR** Work on FALLING KNIVES                                     **CLEAR**
     Work on SAGO CORE instantiation
        - CORE SHIELDS
        - ANIMATION
     **CLEAR** WHAT THE FUCK IS UP WITH THE FLIP                          **CLEAR** Celion's character code was flipping the mouse's.
    -----------------------------CHARACTERS--------------------------------------
    **CLEAR** WORK ON CELION                                              **CLEAR**
    **CLEAR** POWER CHARGE                                                **CLEAR**
    **CLEAR** TIME DEPENDENT( SIZE , POWER , SPEED) MAX TIME:1.5seconds   **CLEAR**  
    **CLEAR** SHOOT PROJECTILE                                            **CLEAR**
    
    **CLEAR** WORK ON Celione                                             **CLEAR**
    **ERASE** PENTASTAR                                                   **ERASE**
    **ERASE** SHIELD BLOCK                                                **ERASE**
    **ERASE** SLASH                                                       **ERASE**
    */
    //Health
    public float myMaxHealth = 100f;
    public float myCurrentHealth = 100f;
    public Image myHealthBar;
    public Image delayedHealthBar;
    //Movement Variables
    private float celionVelocity;
    public float celionSpeed = .5f;
    public float celionJumpHeight = 2f;
    private bool myFacingRight;

    //Gun LOGIC
    private float myGunCoolCD = .5f;
    public float myNextShot;
    public float myChargeTimer;

    //GUN CHARGE Animation
    public Renderer myCharge;

    //Controllers
    public GameObject celion;
    private Rigidbody2D celionRigidBody2D;
    private Animator anim;
    public AudioManager audioManager;

    //Gun Attack logic
    public Transform myFirePoint;
    public GameObject fireBullet;

    //anim logic
    private bool myCelionAttacking;
    private bool myCelionCharging;

    //groundCheck
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool myCelionGrounded;


    //initiates
    //-Rigidbody
    //-Animator
    void Start() {

        celionRigidBody2D = gameObject.GetComponent<Rigidbody2D>(); //initiates rigidbody
        anim = gameObject.GetComponent<Animator>(); //initiates animator
       
    }

    void FixedUpdate() {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No Audiomanager in the scene");
        }
 
        anim.SetBool("Grounded", myCelionGrounded);
        anim.SetBool("Attacking", myCelionAttacking);
        anim.SetBool("Charging", myCelionCharging);
        CelionMove();
        CelionShoot();

        myCelionGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        delayedHealthBar.fillAmount = Mathf.Lerp(delayedHealthBar.fillAmount, myHealthBar.fillAmount, 2.75f * Time.deltaTime);

    }
    //responsible for Celion's attacks
    void CelionShoot()
    {
        //Button to start charging
        if (Input.GetKey(KeyCode.Space) && (Time.time > myNextShot))
        {
            myChargeTimer += Time.deltaTime;
            myCharge.enabled = true;
            myCelionCharging = true;

            //changes render from white to red when fully charged.
            if (myChargeTimer > 1f)
            {
                myCharge.material.SetColor("_Color", Color.red);
            }
        }
        //CHARGED SHOT
        if (Input.GetKeyUp(KeyCode.Space) && (myChargeTimer > 1f) && (Time.time > myNextShot)) 
        {
            GameObject fire = Instantiate(fireBullet, myFirePoint.position, myFirePoint.rotation) as GameObject;
            fire.GetComponent<myFire>().Damage = 100f;
            fire.GetComponent<myFire>().fireBulletSpeed = 1.25f;
            fire.transform.localScale = new Vector3(2f, 2f, 2f);
            myNextShot = Time.time + myGunCoolCD;
            audioManager.PlaySound("megaCharge");
            KnockBack(3);
            myCelionAttacking = true;
        }
        //NORMAL SHOT
        if (Input.GetKeyUp(KeyCode.Space) && (myChargeTimer < 1f) && (Time.time > myNextShot))
        {
            Instantiate(fireBullet, myFirePoint.position, myFirePoint.rotation);
            myNextShot = Time.time + myGunCoolCD;
            audioManager.PlaySound("gunShoot");
            myCelionAttacking = true;
        }
        //SHARED TRAITS
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myCharge.enabled = false;
            myCharge.material.SetColor("_Color", Color.white);
            myChargeTimer = 0f;
            myCelionCharging = false;
        }
    }
    //responsible for moving Celion's Character
    void CelionMove()
    {
        celionVelocity = 0f; //resets velocity to 0 if not moving

        //Jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && myCelionGrounded) // if grounded and not wallsliding then the player can jump
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, celionJumpHeight); // dictates how high you jump

        }
        //Moves Character RIGHT
        if (Input.GetKey(KeyCode.RightArrow))
        {
            celionVelocity = celionSpeed;
            myFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Moves Character LEFT
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            celionVelocity = -celionSpeed;
            myFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);

        }

        
        celionRigidBody2D.velocity = new Vector2(celionVelocity, celionRigidBody2D.velocity.y); //moves rigidbody to new
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)); //Controller for move animation
    }
    //knockback from CHARGED shot
    void KnockBack(int KnockBackAmount)
    {
        //if facingRight, get knockedback to the left.
        if (myFacingRight) 
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-this.transform.localScale.x * KnockBackAmount / 2, 0) * 100 * Time.deltaTime * .5f, ForceMode2D.Impulse);
        }
        //if not facingRight, get knockedback to the right.
        else if (!myFacingRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-this.transform.localScale.x * KnockBackAmount / 2, 0) * 100 * Time.deltaTime * .5f, ForceMode2D.Impulse);

        }
    }
    public void Damage(float Damage)
    {
        myCurrentHealth -= Damage;
        float totalHealth = myCurrentHealth / myMaxHealth;
        setHealth(totalHealth);
        //enemyHealthAnim.SetBool("myShake", true);
        //myShake = true;
        //myNextShake = Time.time + myShakeCd;
        if (myCurrentHealth < 0)
        {
            myCurrentHealth = myMaxHealth;
        }

    }
    void setHealth(float myHealth)
    {
        myHealthBar.fillAmount = myHealth;
    }
    public void soundDamage(string sound)
    {
        if (sound == "knifeHit")
        {
            audioManager.PlaySound("knifeHit");
        }
        if (sound == "bulletHit")
        {
            audioManager.PlaySound("bulletHit");
        }

    }




}
