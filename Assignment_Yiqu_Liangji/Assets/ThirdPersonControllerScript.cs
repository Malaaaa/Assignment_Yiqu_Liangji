using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThirdPersonControllerScript : MonoBehaviour
{
    public GameObject studentID;
    public float speed = 2.0f;

    public Animator animator;

    public Transform Enemy;

    public Slider hpUI;

    public GameObject FireBall;

    public float Maxhealth=100;

    public float Curhealth;

    public float attackRange = 2f;

    public bool inCombat = false;

    public bool Reward = false;

    private RaycastHit raycastHit;

    private Vector3 storedClickedPosition;

    private float turnSmooth = 25f;

    // In combat statut
    public string PLAYER_STATUTS_INCOMBAT = "Combat";

    public string ATTACK_FUNCTION = "Attack";

    public string blockedObjectTag;

    // Not in combat walking
    public string MOVING_FUNCTION_NORMAL_WALK = "Moving";

    public string MOVING_FUNCTION_RUNNING = "Running";

    public string SPEAKING = "Speak";

    public string MINIMAP = "MiniMap";

    public string TASKS = "Tasks";

    public string TASK = "Task";

    public string MAIN_MENU = "MainMenu";

    public string ENEMY = "Enemy";

    // check the direction which player to destination
    public float DESTINATION_DIRECTION = 1f;

    // check the distance which player to enemy
    public float ENEMY_DISTANCE;
    
    public float GURAD_DISTANCE = 5f;

    // Start is called before the first frame update
    void Start()
    {
        storedClickedPosition = Vector3.zero;
        Enemy = GameObject.FindWithTag(ENEMY).transform;
        Curhealth = Maxhealth;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * Another thinking:
         * Auto find path
         * Store the click position, check current position and click position
         * if they do not have same x, z position, player should move to the click position contiune.
         * if the have same x,z position, set the click position null, and stop moving
         * if player does not move finish but player has a new clicked position, 
         *   the player should move to the new position 
         *   
         *   
         *  Auto find path function
         *
         *  different situations has different choices
         *  1.if mouse clicked the ground, store the position to storedClickedPosition and player move to this position.
         *      during this moving, if player does not move to the expectant position without new click, 
         *      player should move to the expectant position go on
         *  2.if mouse clicked the ground and player has not move to the expectant position, 
         *      store the new position and player should move to the new position
         *  3.if player has moved to the expectant position, should remove the stored position and stop moving
         *
         *  Add combat function and health cal function
         *  Only two conditions in ture, player should be in combat
         *  1.the distance 
         *  2.mouse clicked to enemy.
         *  If player run away, the distance large than detection distance, player will be in normal status.
         *  Players movement speed should large than enemy
         */ 
        GameObject enemyTempObject = GameObject.FindWithTag(ENEMY);
        if (enemyTempObject == null) {
            ENEMY_DISTANCE = 0f;
        } else {
            Enemy = enemyTempObject.transform;
            ENEMY_DISTANCE = Vector3.Distance(Enemy.transform.position, transform.position);
        }
        if (Input.GetMouseButton(0) && !isClickedTheSpeaking()) {
            
            // get current mouse screen position
            Vector3 currentScreenPosition = Input.mousePosition;
 
            // declear a ray to screen position
            Ray groundCheckRay = Camera.main.ScreenPointToRay(currentScreenPosition);

             // if the ray touch to the ground
            if (Physics.Raycast(groundCheckRay, out raycastHit)) {
                GameObject currentBlockedObject = raycastHit.collider.gameObject;
                // if mouse clicked to the ground, player should be move to this position
                string blockedObjectTag = currentBlockedObject.tag;
                // Debug.Log(blockedObjectTag);
                switch (blockedObjectTag){
                    case "Ground" :
                        /*
                        *  If player in combat, but he run away
                        */
                        // if (inCombat) {
                        //     ChangeAnimatorStatus(ATTACK_FUNCTION, false);
                        //     if (enemyTempObject == null) {
                        //         Debug.Log("in 1");
                        //         inCombat = false;
                        //         ChangeAnimatorStatus(PLAYER_STATUTS_INCOMBAT, false);
                        //     }
                        // }
                        if (inCombat && enemyTempObject == null) {
                            // Debug.Log("Enemy down");
                            inCombat = false;
                            ChangeAnimatorStatus(ATTACK_FUNCTION, false);
                            ChangeAnimatorStatus(PLAYER_STATUTS_INCOMBAT, false);
                        }
                        Moving(raycastHit.point);
                        break;
                    case "Enemy" :
                        // Debug.Log("Clicked enemy");
                        EnemyAttack(currentBlockedObject.transform.position);
                        break;
                    case "EnemyWappon" :
                        // Debug.Log("Clicked enemy");
                        EnemyAttack(currentBlockedObject.transform.position);
                        break;
                    case "studentID":
                        GetReward();
                        break;
                        
                }  
            }
        } else if (Input.GetKeyDown(KeyCode.E)) {
            // player push E to range attack
            GameObject fireImg = (GameObject)Instantiate(FireBall);
            Vector3 firePosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 1f);
            fireImg.transform.position = firePosition;
            fireImg.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
            fireImg.SetActive(true);
        } else {
            if (hasStoredPosition()) {
                Moving2Destination();
            } else if (inCombat) {
                ChangeAnimatorStatus(ATTACK_FUNCTION, true);
            }else {

                ChangeAnimatorStatus(MOVING_FUNCTION_NORMAL_WALK, false);
            }
        }
        /*
         * Check the detective distance
         * if player in combat but far away from enemy, 
         * change player status
         */
        if ((ENEMY_DISTANCE > GURAD_DISTANCE && inCombat) || 
            (enemyTempObject == null && !inCombat)) {
            // ChangeAnimatorStatus(MOVING_FUNCTION_NORMAL_WALK, true);
            ChangeAnimatorStatus(PLAYER_STATUTS_INCOMBAT, false);
        }
        RunOrWalk();
        // check player whether near the destination position less than 1f;
        if (isArrived2DestinationPositionByCustomDistance(DESTINATION_DIRECTION)) {
            RemoveDestinationPosition();
        }
        ResetBodyPosition();
        hpUI.maxValue = Maxhealth;
        hpUI.value = Curhealth;
        // Debug.Log("===========================");
        // Debug.Log("Walk: " + animator.GetBool(MOVING_FUNCTION_NORMAL_WALK));
        // Debug.Log("Running: " + animator.GetBool(MOVING_FUNCTION_RUNNING));
        // Debug.Log("Incombat: " + animator.GetBool(PLAYER_STATUTS_INCOMBAT));
        // Debug.Log("Attack: " + animator.GetBool(ATTACK_FUNCTION));
        // Debug.Log("===========================");
    }

    private void EnemyAttack(Vector3 currentTargetPosition) {
        /*
         *  mouse clicked to the enemy, should check the distance.
         *  If distance less than attack distance, just attack.
         *  If distance large than attack distance, player should move nearly.
         */
        if (ENEMY_DISTANCE <= attackRange) {
            // attack function
            // stop moving, look at enemy and attack
            storedClickedPosition = Vector3.zero;
            transform.LookAt(new Vector3(currentTargetPosition.x, transform.position.y, currentTargetPosition.z));
            ChangeAnimatorStatus(PLAYER_STATUTS_INCOMBAT, true);
            ChangeAnimatorStatus(ATTACK_FUNCTION, true);
            if (!inCombat) {
                inCombat = true;
            }
            ChangeAnimatorStatus(ATTACK_FUNCTION, false);
        } else {
            Moving(new Vector3(raycastHit.point.x, 0f, raycastHit.point.z));
        }
    }

    private void Moving(Vector3 destinationPosition) {

        StoreDestinationPosition(destinationPosition);
        TakeSmoothRotation();
        Moving2Destination();
    }

    /*
     *  Instead of the LookAt function, 
     *  this function will help player turn to new destination smoothly
     */
    private void TakeSmoothRotation() {
        
        // Quaternion oldRotation = transform.rotation;
        // transform.LookAt(storedClickedPosition);
        // Quaternion newRotation = transform.rotation;
        // transform.rotation = oldRotation;
        // transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSmooth * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(storedClickedPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmooth * Time.deltaTime);
    }

    // Checking the new destination or had
    private bool hasStoredPosition() {
        
        if (storedClickedPosition != Vector3.zero) {
            return true;
        }
        return false;
    }

    // if push left shift, player should run
    private void RunOrWalk() {
        if (Input.GetKey(KeyCode.LeftShift) && !inCombat) {
           ChangeAnimatorStatus(MOVING_FUNCTION_RUNNING, true);
           speed = 5f;
        } else{
           ChangeAnimatorStatus(MOVING_FUNCTION_RUNNING, false);
           speed = 2f;
        }
    }

    // moving function
    private void Moving2Destination() {

        // transform.LookAt(storedClickedPosition);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        ChangeAnimatorStatus(MOVING_FUNCTION_NORMAL_WALK, true);
        if (inCombat) {
            ChangeAnimatorStatus(PLAYER_STATUTS_INCOMBAT, true);
        } else {
            ChangeAnimatorStatus(PLAYER_STATUTS_INCOMBAT, false);
        }
    }

    

    // check the current player whether arrived to the destination position
    private bool isArrived2DestinationPositionByCustomDistance(float customDistance) {

        // should use approximate position because the step will not follow the position, it will have 0-1 error
        // if (transform.position.x == storedClickedPosition.x && transform.position.z == storedClickedPosition.z) {
        //     return true;
        // }
        // this is new function to check, use distance to check the position
        if (storedClickedPosition == Vector3.zero) {
            return true;
        }
        float destinationDistance = Vector3.Distance(transform.position, storedClickedPosition);
        if (destinationDistance < customDistance) {
            return true;
        }
        return false;
    }

    // store the destination position
    private void StoreDestinationPosition(Vector3 destinationPosition) {

        storedClickedPosition = new Vector3(destinationPosition.x, destinationPosition.y, destinationPosition.z);
    }

    // remove the stored position
    private void RemoveDestinationPosition() {

        storedClickedPosition = Vector3.zero;
    }
    private void Attack(){
        
    }

    private bool isClickedTheSpeaking() {

        GameObject currentGameObject = EventSystem.current.currentSelectedGameObject;
        if (currentGameObject != null) {
            if (currentGameObject.name == SPEAKING 
                || currentGameObject.name == MINIMAP 
                || currentGameObject.name == TASKS
                || currentGameObject.name == TASK
                || currentGameObject.name == MAIN_MENU) {
                return true;
            }
        }
        return false;
    }

    /*
     *  Yiqu Zhang:
     *  This function will fix the PlayerBody Position
     *  Because when PlayerBody was be blocked, 
     *  current transform will be continue move to destination position.
     *  It means "Axis deflection": PlayerBody do not have same position with transform
     */
    private void ResetBodyPosition() {
        
        float tempDistance = Vector3.Distance(transform.position, animator.transform.position);
        if (tempDistance > 0.5f) {
            animator.transform.position = transform.position;
        }
    }

    private void ChangeAnimatorStatus(string animatorName, bool status) {

        animator.SetBool(animatorName, status);
    }

    /*
     *  Player is attacked by enemy,
     *  Per attack, lose amount health
     */
    public void ChangeHealth(float amount) {

        Curhealth -= amount;

    }
    public void GetReward(){
        if(studentID.activeSelf)
        {
            studentID.SetActive(false);   
            Reward = true;
        }
    }
}
