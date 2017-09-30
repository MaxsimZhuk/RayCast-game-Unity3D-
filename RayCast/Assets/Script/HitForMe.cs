using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitForMe : MonoBehaviour {

    // Use this for initialization
    public GameObject Player;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HitForPlayer()
    {
        Player.BroadcastMessage("HealthDamage", 20);
    }

}
