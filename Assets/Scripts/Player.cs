using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal"); // Zmienna pobierajaca os pozioma z Unity, i zmiana jaka robimy
        var newXPos = transform.position.x + deltaX; // Zmienna przechowujaca wartosc o jaka zmieniamy polozenie gracza, polozenie plus zmiana polozenia
        transform.position = new Vector2(newXPos, transform.position.y); //formula ktora przesowa gracza (przesowa jego os Y) o wartosc newXPos, zmienia polozenie i mowi zostan tu, w osi Y
    }
}
