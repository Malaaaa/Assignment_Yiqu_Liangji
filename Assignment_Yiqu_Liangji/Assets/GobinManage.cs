using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GobinManage : MonoBehaviour
{
    public GameObject[] Target;
    public GameObject StudentsID;
    public GameObject Golbin;
    public enum State
    {
        Idle, Walk, Run, Attack, Damage, Dead,
    }
    private Coroutine async;
    public State CurrentState;
    public Transform player;
    public float distance;
    private Animator ani;
    private NavMeshAgent agent;
    private bool AttackLock;//Aviod attack a lot
    private int i = 0;
    private float speed = 2.0f;
    private float time = 5.0f;
    private float CD = 2f;
    public Slider hpUI;
    public float Maxhealth = 100;
    public float Curhealth;
    public float amount;
    public float damage_value;
    public int j;
    private void Start()
    {
        CurrentState = State.Idle;
        player = GameObject.FindWithTag("Player").transform;
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Curhealth = Maxhealth;
        AttackLock = false;
        hpUI.maxValue = Maxhealth;
        StudentsID.SetActive(false);
    }

    private void Update()
    {
        StudentsID.transform.position = Golbin.transform.position;
        distance = Vector3.Distance(player.transform.position, transform.position);
        switch (CurrentState)
        {
            case State.Idle:
                StateIdle();
                break;
            case State.Walk:
                StateWalk();
                break;
            case State.Attack:
                StateAttack();
                break;
            case State.Damage:
                StateDamage();
                break;
            case State.Dead:
                StateDead();
                break;
        }
        hpUI.value = Curhealth;
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerWappon"){
            ChangeDamage();
            ChangeHealth(10f);
        }
        if(other.tag == "FireBall"){
            ChangeDamage();
            ChangeHealth(15f);
        }
    }
    public void ChangeHealth(float amount) {

        Curhealth -= amount;
    }
    public void StateIdle()
    {
        agent.speed = speed;
        ani.SetFloat("Speed", speed);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 5.0f;
            i = Random.Range(0, Target.Length);
        }
        agent.SetDestination(Target[i].transform.position);
        if (distance < 15)
        {
            ChangeWalk();
        }
    }
    public void StateWalk()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        agent.speed = 2*speed;
        agent.SetDestination(player.transform.position);
        ani.SetFloat("Speed", 2*speed);
        if (distance < 2)
        {
            ChangeAttack();
        }
    }

    public void StateAttack()
    {
        if (AttackLock == false){
            j = Random.Range(0, 2);
            AttackLock = true;
            agent.speed = 0f;
            gameObject.transform.LookAt(player.transform.position);
            if (j == 0) ani.SetTrigger("Attack1");
            else ani.SetTrigger("Attack2");
            if (async != null)
            {
                StopCoroutine(StateChange());
            }
            async=StartCoroutine(StateChange());
        }
    }

    private void StateDamage()
    {
        ani.SetTrigger("Hit");
        agent.speed = 0;
    }

    private void StateDead()
    {
        agent.speed = 0;
        ani.SetBool("Dead", true);
        Golbin.SetActive(false);
        StudentsID.SetActive(true);
    }
    private void ChangeIdle()
    {
        ani.SetTrigger("Idle");
        ChangeIdle();
    }
    private void ChangeWalk()
    {
        CurrentState = State.Walk;
    }
    private void ChangeAttack()
    {
        CurrentState = State.Attack;
    }
    private void ChangeDamage()
    {
        CurrentState = State.Damage;
    }
    private void ChangeDead()
    {
        CurrentState = State.Dead;
    }
    private IEnumerator StateChange( )
    {   
        yield return new WaitForSeconds(CD);
        AttackLock = false;
        async = null;
        if (distance > 1)
        {
            ChangeWalk();
        }
        else
        {
            ChangeAttack();
        }
    }
}