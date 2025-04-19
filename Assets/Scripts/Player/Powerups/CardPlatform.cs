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
        public HealthManager healthManager;
        public GroundChecker groundChecker;
        public PlayerController playerController;
        private Vector3 forwardOffset;
        public float despawnTimer = 5.0f;
        public int cardVal = 0;
        public bool groundUpgrade = false;
        public bool upgradeAppl = false;
        public int tempHealth;

        public CardPowerup powerupData; //Scriptable object

        //Things that need to be done
        //Red card and black card Need the Red Card and Black Card tag
        //both need to be put under the card layermask in groundchecker

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
                Throw(redBlack ? 0 : 1);
                redBlack = !redBlack;
                canThrow = false;
                throwCd = 2.0f;
            }

            if (throwCd > 0)
            {
                throwCd -= Time.deltaTime;
            }
            else
            {
                canThrow = true;
            }

            if (!groundChecker.isCardGrounded && groundUpgrade == false) //not on card
            {
                if (groundChecker.isCardGrounded) //on card 
                {
                    groundUpgrade = true; //upgrade can be put on
                    if (groundUpgrade)
                    {
                        Upgrade(cardVal); //applies the upgrade
                        groundUpgrade = false; //upgrade can no longer be put on
                    }

                }
                if (!groundChecker.isCardGrounded) //off card
                {
                    groundUpgrade = false; //upgrade cannot be put on
                }

                //This controls and manages the health gain
                if (groundChecker.isGrounded || (groundChecker.isCardGrounded && cardVal == 1)) //touch a different ground or black card
                {
                    if (upgradeAppl) //upgrade has been applied
                    {
                        upgradeAppl = false;
                        if (healthManager.curHealth == tempHealth) //checks if u have not lost the extra hitpoint (true)
                        {
                            healthManager.curHealth -= 1; //takes it away
                        }
                    }
                }

                if (groundChecker.isGrounded || groundChecker.isCardGrounded) //touch a different ground or new card
                {
                    //playerController.changeMoveSpeed(-3); //move the speed back to normal
                }
            }
        }

        void OnCollisionEnter(Collision other) //this function checks which card the player is presently standing on
        {
            if (other.gameObject.tag == "Red Card")
            {
                cardVal = 0;
            }
            else if (other.gameObject.tag == "Black Card")
            {
                cardVal = 1;
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
                //playerController.changeMoveSpeed(3);
                upgradeAppl = true;
            }
        }

        public void Throw(int cardVal)
        {
            GameObject plat = cardVal == 0 ? redPlat : blackPlat;

            Instantiate(plat, playerPrefab.position + forwardOffset, playerPrefab.rotation);

            canThrow = false;
            throwCd = 2.0f;

            this.cardVal = cardVal;

            if (powerupData != null)
            {
                powerupData.cardVal = cardVal;
            }

            if (playerController != null && powerupData != null)
            {
                powerupData.ApplyEffects(playerController);
                Debug.Log("CardPlatform powerup applied after throw");
            }

            despawnTimer = 5.0f;
            StartCoroutine(Despawn(plat, despawnTimer));
        }

        IEnumerator Despawn(GameObject platPrefab, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(platPrefab.gameObject);
        }
    }
