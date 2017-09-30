using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateBlock : MonoBehaviour
{


   
    //материалы данного объекта
    public Material Normal,  //когда объект нормальный 
                    Select;  //когда объект выделен

    //состояние объекта выделен ли он или нет
    public bool SelectObject = false;

    //Компонент отвечающий за отображение объекта
    Renderer MyMesh;

    // Use this for initialization
    void Start ()
    {
        //получим ссылку на  компонент Renderer для смены на объекте материала
        MyMesh = this.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (SelectObject)
        {
            //задаем объекту материал отображающий выбор объекта
            MyMesh.material = Select;
        }
        else //если флаг не установлен на объекте 
        {
            //устанавливаем материал на нормальный вид
            MyMesh.material = Normal;
        }
    }

    public void HitForMe()
    {
        Debug.Log("You are shoot at me.");
        SelectObject = !SelectObject;
    }
}
