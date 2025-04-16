using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/*
public class CardPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private float throwCd = 2.0f;
    private bool canThrow;
    private bool redBlack; //true = red, false = black
    public GameObject redPlat; //red platform
    public GameObject blackPlat; //black platform
    public Transform playerPrefab;
    private Vector3 forwardOffset;
    public float despawnTimer = 5.0f;
    public int cardVal = 0;
    public bool groundUpgrade = false;
    public bool upgradeAppl = false;
    public int tempHealth;

    void Start()
    {
        redBlack = false; //start on black
        forwardOffset = new Vector3(150, 0, 0);
        canThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canThrow)
        {
            Throw(redBlack);
            redBlack = !redBlack;
        }

        if (throwCd > 0)
        {
            canThrow = true;
            throwCd = 2.0f;
        }

        if (!GroundChecker.isCardGrounded && groundUpgrade = false) //not on card
        {
            if (GroundChecker.isCardGrounded ) //on card 
            {
                groundUpgrade = true; //upgrade can be put on
                if(groundUpgrade )
                {
                    Upgrade(cardVal); //applies the upgrade
                    groundUpgrade = false; //upgrade can no longer be put on
                }
                
            }
            if (!GroundChecker.isCardGrounded) //off card
            {
                groundUpgrade = false; //upgrade cannot be put on
            }

            //This controls and manages the health gain
            if (GroundChecker.isGrounded || (GroundChecker.isCardGrounded && cardVal = 1)) //touch a different ground or black card
            {
                if (upgradeAppl) //upgrade has been applied
                {
                    upgradeAppl = false;
                    if (HealthManager.curHealth == tempHealth) //checks if u have not lost the extra hitpoint (true)
                    {
                        HealthManager.curHealth -= 1; //takes it away
                    }
                }
            }

            if (GroundChecker.isGrounded || GroundChecker.isCardGrounded) //touch a different ground or new card
            {
                PlayerController.moveSpeed -= 3; //move the speed back to normal
            }
        }
        

    }

    void CheckCard(Collision other) //this function checks which card the player is presently standing on
    {
        if(other.gameObject.tag == "Red Card")
        {
            cardVal = 0;
        }
        else if(other.gameObject.tag == "Black Card")
        {
            cardVal = 1;
        }
    }

    //this function provides the associated upgrade
    void Upgrade(int cardVal)
    {
        if (cardVal == 0) //checks red
        {
            HealthManager.curHealth += 1;
            tempHealth = HealthManager.curHealth;
            upgradeAppl = true;
        }
        else //checks black
        {
            PlayerController.moveSpeed += 3;
            upgradeAppl = true;
        }
    }

    void Throw(bool redOrBlack)
    {
        
        if (redOrBlack)
        {
            Instantiate(redPlat, playerPrefab.position + forwardOffset, playerPrefab.rotation);
            canThrow = false;
            throwCd -= Time.deltaTime;
            despawnTimer -= Time.deltaTime;
            if (despawnTimer > 0)
            {
                Despawn(redPlat);
                despawnTimer = 5.0f;
            }
            cardVal = 0;
            //UI shift
        }
        else
        {
            Instantiate(blackPlat, playerPrefab.position + forwardOffset, playerPrefab.rotation);
            canThrow = false;
            throwCd -= Time.deltaTime;
            despawnTimer = Time.deltaTime;
            if (despawnTimer > 0)
            {
                Despawn(blackPlat);
                despawnTimer = 5.0f;
            }
            cardVal = 1;
            //UI shift
        }
    }

    void Despawn(GameObject platPrefab)
    {
        Destroy(platPrefab.gameObject);
    }
}
*/