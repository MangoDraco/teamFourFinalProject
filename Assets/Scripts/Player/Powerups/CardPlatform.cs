using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using teamFourFinalProject;
using UnityEngine;

public class CardPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private float throwCd = 2.0f;
    private bool canThrow;
    private bool redBlack; //true = red, false = black
    public GameObject redPlat; //red platform
    public GameObject blackPlat; //black platform
    public Transform playerPrefab;
    HealthManager healthManager;
    GroundChecker groundChecker;
    PlayerController playerController;
    private Vector3 forwardOffset;
    public float despawnTimer = 7.0f;
    public int cardVal = 0;
    public bool groundUpgrade = false;
    public bool upgradeAppl = false;
    public int tempHealth;

    //Things that need to be done
    //Red card and black card Need the Red Card and Black Card tag
    //both need to be put under the card layermask in groundchecker

    void Start()
    {
        redBlack = false; //start on black
        forwardOffset = new Vector3(20, 0, 0);
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

        if (upgradeAppl)
        {
            if (!groundChecker.isCardGrounded && groundChecker.isGrounded) //erase bonus once you land on something else :3
            {
                EraseBonuses(cardVal);
                upgradeAppl = false;
            }
        }
    }

    void EraseBonuses(int cardVal)
    {
        switch (cardVal)
        {
            case 0:
                if (healthManager.curHealth == tempHealth) //checks if u have not lost the extra hitpoint (true)
                {
                    healthManager.curHealth -= 1; //takes it away
                }
                break;
            case 1:
                playerController.moveSpeed -= 3;
                break;
        }
        
    }

    void OnCollisionEnter(Collision other) //this function checks which card the player is presently standing on
    {
        if(other.gameObject.tag == "Red Card")
        {
            if (groundChecker.isCardGrounded)
            {
                cardVal = 0;
                Upgrade(cardVal);
                upgradeAppl = true;
            }
            
        }
        else if(other.gameObject.tag == "Black Card")
        {
            if (groundChecker.isCardGrounded)
            {
                cardVal = 1;
                Upgrade(cardVal);
                upgradeAppl = true;
            }
        }
    }

    //this function provides the associated upgrade
    void Upgrade(int cardVal)
    {
        if (cardVal == 0) //checks red
        {
            healthManager.curHealth += 1;
            tempHealth = (int)healthManager.curHealth;
            upgradeAppl = true;
        }
        else //checks black
        {
            playerController.moveSpeed += 3;
            upgradeAppl = true;
        }
    }

    void Throw(bool redOrBlack)
    {
        
        if (redOrBlack)
        {
            Instantiate(redPlat, playerPrefab.position + forwardOffset, playerPrefab.rotation);
            redPlat.transform.Translate(Vector3.forward * 70f * 10f * Time.deltaTime); //forward * distance * speed * time.deltatime (if u want to change it)
            canThrow = false;
            throwCd -= Time.deltaTime;
            despawnTimer -= Time.deltaTime;
            if (despawnTimer > 0)
            {
                Despawn(redPlat);
                despawnTimer = 7.0f;
            }
            //UI shift to show the next card
        }
        else
        {
            Instantiate(blackPlat, playerPrefab.position + forwardOffset, playerPrefab.rotation);
            blackPlat.transform.Translate(Vector3.forward * 70f * 10f * Time.deltaTime); //forward * distance * speed * time.deltatime (if u want to change it)
            canThrow = false;
            throwCd -= Time.deltaTime;
            despawnTimer = Time.deltaTime;
            if (despawnTimer > 0)
            {
                Despawn(blackPlat);
                despawnTimer = 7.0f;
            }
            //UI shift to show the next card
        }
    }

    void Despawn(GameObject platPrefab)
    {
        Destroy(platPrefab.gameObject);
    }
}
