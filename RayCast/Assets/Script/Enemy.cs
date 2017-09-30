using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool alive;
    Animation myanimation;
    CharacterController MyPawnBody;

    //точки патрулювання
    Transform PatrolPoint;
    public  List<Transform> MyPatrolPoint;
    private bool procedurePatrol=true;
    private int numberPatrolPoinl=0;
    private Transform target;
    private int timer;
    private int reaction = 50;
    Vector3 moveDirection = Vector3.zero;
    //гравитация сила с которой персонаж будет прижиматься к полу
    public float gravity = 1.0F;
    // Use this for initialization
    void Start ()
	{
        MyPawnBody = this.GetComponent<CharacterController>();
        myanimation = GetComponent<Animation>();
        health = 100;
        PatrolPoint = MyPatrolPoint[0];
        alive = true;

	}

    // Update is called once per frame
	void FixedUpdate() {
	    if (health> 0)
	    {
            if(target == null)
	        {

	            RaycastHit info;
	            Quaternion rotatenionX = Quaternion.AngleAxis(90, Vector3.up);

	            GameObject myGameObject;
	            myGameObject = gameObject;
	            myGameObject.transform.rotation *= rotatenionX;
	            for (int i = 0; i < 180; i++)
	            {
	                rotatenionX = Quaternion.AngleAxis(1, Vector3.up);
	                myGameObject = gameObject;
	                myGameObject.transform.rotation *= rotatenionX;

	                if (Physics.Raycast(myGameObject.transform.position, transform.forward, out info, 1000f))
	                {
	                    if (info.collider.gameObject.CompareTag("Team1"))
	                    {
	                        target= info.collider.gameObject.transform;
                            //Debug.Log(info.collider.gameObject);
	                    }
	                    // Debug.Log(info.collider.gameObject);
	                }
	            }
	            transform.LookAt(PatrolPoint);
	            if (transform.position.x <= PatrolPoint.position.x + 2 &&
	                transform.position.x >= PatrolPoint.position.x - 2 &&
	                transform.position.z <= PatrolPoint.position.z + 2 &&
	                transform.position.z >= PatrolPoint.position.z - 2)
	            {
	                myanimation.CrossFade("soldierIdleRelaxed");
	                if (procedurePatrol)
	                {
	                    if (numberPatrolPoinl == MyPatrolPoint.Count - 1)
	                    {
	                        procedurePatrol = false;
	                    }
	                    else
	                    {
	                        numberPatrolPoinl++;
	                    }
	                }
	                else
	                {
	                    if (numberPatrolPoinl == 0)
	                    {
	                        procedurePatrol = true;
	                    }
	                    else
	                    {
	                        numberPatrolPoinl--;
	                    }
	                }
	                PatrolPoint = MyPatrolPoint[numberPatrolPoinl];
	            }
	            else
	            {
	                myanimation.CrossFade("soldierWalk");
	                transform.position += transform.forward*2*Time.deltaTime;
	            }
	        }
            else
            {
                transform.LookAt(target);
                timer++;
                myanimation.CrossFade("soldierFiring");
                if (timer > reaction)
                {
                    timer=0;
                    RaycastHit info;
                    if (Physics.Raycast(transform.position, transform.forward, out info, 10000f))
                    {
                        if (info.collider.gameObject.CompareTag("Team1"))
                        {
                            info.collider.gameObject.BroadcastMessage("HitForPlayer");
                        }
                        else
                        {
                            target = null;
                        }
                        Debug.Log(info.collider.gameObject);
                    }
                }
            }
	    }
        //гравітація сила з якою персонаж буде притискатися до підлоги

        moveDirection.y -= gravity * Time.deltaTime;
        MyPawnBody.Move(moveDirection * Time.deltaTime);
    }

    void HealthDamage(int damage)
    {
        
        health = health - damage;
        Debug.Log("You are damage " + health);
        if (health < 1 && alive)
        {
            
            myanimation.CrossFade("soldierDieBack");
            alive = false;
        }
    }

}
