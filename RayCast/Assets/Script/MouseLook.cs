using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameObject Flash;
    public GameObject Weapon;
    public int timer;
   
    public int health;
    //настройка сеансы 
    public float SenX = 5, SensY = 10;
    //на сколько поворачивать камеру по X и по Y
    float moveY, moveX;
    //флаги движения по осям 
    public bool RootX = true, //разрешаем или запрещаем перемещение по оси X 
    RootY = true; //разрешаем или запрещаем перемещение по оси X
    public bool TestY = true,  //включаем ограничение поворота камеры вдоль оси Y
    TestX = false; //включение ограничения поворота камеры вдоль оси X
    public Vector2 MinMax_Y = new Vector2(-40, 40),    //ограничение по оси Y
    MinMax_X = new Vector2(-360, 360);  //ограничение по оси X
    CharacterController MyPawnBody; //контроллер игрока для вращения камерой

    //функция расчета угла поворота
    static float ClampAngle(float angle, float min, float max)
    {
        //если угол прошел расстояние от 0 до -360 то обнуляем его 
        if (angle < -360F) angle += 360F;
        //если угол прошел расстояние от 0 до 360 то обнуляем его 
        if (angle > 360F) angle -= 360F;
        //рассчитываем среднее значение поворота относительно угла 
        return Mathf.Clamp(angle, min, max);
    }
    // Use this for initialization
    void Start ()
    {
        //Cursor.visible = false;//для отключения курсора
        //для блокировки курсора в центр
        //получаем тело нашего игрока
        MyPawnBody = this.GetComponent<CharacterController>();
        health = 100;
        timer=0;
        Flash.SetActive(false); ;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        Cursor.lockState = CursorLockMode.Locked;

	    if (timer == 0)
	    {
	        if (Input.GetKeyDown(KeyCode.Mouse0))
	        {
                if (RootY) moveY -=  SensY;
                timer++;
	            Vector3 posWeapon = new Vector3(-20, 0, 0);
	            Weapon.transform.eulerAngles = Weapon.transform.eulerAngles + posWeapon;

	            RaycastHit info;
	            if (Physics.Raycast(transform.position, transform.forward, out info, 10000f))
	            {
	                if (info.collider.gameObject.CompareTag("Team2"))
	                {
	                    info.collider.gameObject.BroadcastMessage("HitForMe");
	                }

	                Debug.Log(info.collider.gameObject);
	            }
                Flash.transform.localScale += new Vector3( 2.0f,0.0f,  0.0f);
                Flash.SetActive(true);
              
            }
         
	    }
	    else
	    {
	        timer++;
            Vector3 posWeapon = new Vector3(2, 0, 0);
	        Weapon.transform.eulerAngles = Weapon.transform.eulerAngles + posWeapon;

            Flash.transform.localScale -= new Vector3( timer/10, 0.0f, 0.0f);
            if (timer > 10)
	        {
	            timer = 0;
                Flash.SetActive(false);

            }
	    }
	    //получаем угол на который мышь улетела от центра экрана по Y
        if (RootY) moveY -= Input.GetAxis("Mouse Y") * SensY;
        //ограничиваем угол поворота камеры чтобы она не вращалась под ноги 
        if (TestY) moveY = ClampAngle(moveY, MinMax_Y.x, MinMax_Y.y);
        //получаем угол на который мышь улетела от центра экрана по Х
        if (RootX) moveX += Input.GetAxis("Mouse X") * SenX;
        //ограничиваем угол поворота камеры чтобы она не вращалась по оси X
        if (TestX) moveX = ClampAngle(moveX, MinMax_X.x, MinMax_X.y);
        //поворачиваем тело персонажа по осям 
        MyPawnBody.transform.rotation = Quaternion.Euler(moveY, moveX, 0);
	    if (gameObject.transform.position.y < 2.1)
	    {
            youare = youarewin;
        }
    }

    public Texture2D Pricel;
    public Texture2D health2D;
    public Texture2D green;
    public Texture2D youaredead;
    public Texture2D youarewin;
    private Texture2D youare;

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - 20, Screen.height / 2 - 20, 40, 40), Pricel);
        GUI.DrawTexture(new Rect(5, 20, 100, 15), health2D);
        GUI.DrawTexture(new Rect(5, 20, health, 15), green);
        GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), youare);
        
    }
    public void HealthDamage(int damage)
    {

        health = health - damage;
        Debug.Log("You are damage " + health);
        if (health < 1)
        {            
            youare = youaredead;
        }


    }
}
