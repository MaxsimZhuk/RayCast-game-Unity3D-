using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour {

    //получаем текущую позицию персонажа
    CharacterController MyPawnBody;
    //пустой вектор направления персонажа (0, 0, 0)
    Vector3 moveDirection = Vector3.zero;
    //скорость движения
    public float speed = 6.0F;
    //высота прыжка
    public float jumpSpeed = 10.0F;
    //гравитация сила с которой персонаж будет прижиматься к полу
    public float gravity = 20.0F;

    // Use this for initialization
    void Start () {
        //получение компонента отвечающего за движение персонажа 
        MyPawnBody = this.GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {
        //если есть компонент то будем что то делать иначе ничего не делаем 
        if (MyPawnBody != null)
        {
            //если на земле то будем двигать пешку
            if (MyPawnBody.isGrounded)
            {
                //получаем команды по горизонтали от клавиш A и D
                float AD = Input.GetAxis("Horizontal");
                //получаем команды по вертикали от клавиш W и S
                float WS = Input.GetAxis("Vertical");
                //задаем новое направление куда нужно двигаться
                moveDirection = new Vector3(AD, 0, WS);
                //преобразуем вектор движения в направление движения
                moveDirection = transform.TransformDirection(moveDirection);
                //задаем скорость движения 
                moveDirection *= speed;
                //если нажат прыжок пробуем подпрыгнуть
                if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
                //гравитация все время тянет персонажа вниз пока он висит в воздухе


            }
            moveDirection.y -= gravity * Time.deltaTime;
            //двигаем тело в указанном направлении
            MyPawnBody.Move(moveDirection * Time.deltaTime);
        }              
    }

}
