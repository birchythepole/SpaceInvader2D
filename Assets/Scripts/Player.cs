using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] [Range(1, 30)] float moveSpeedHorizontal = 10f;
    [SerializeField] [Range(1, 30)] float moveSpeedVertical = 6f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}
    /*Dela.time jest funkcja, ktora powoduje iz poruszanie sie obiektu w Unity jest
     niezalezne od ilosci klatek (frame rate independent), jest to bardzo potrzebna
     funkcja gdyz sprawia ze gra ma caly czas taka sama predkosc, niezaleznie od
     predkosci komputera na ktorym gramy. Delta time mowi nam ile trwa wygenerowanie
     pojedynczej klatki, i jezeli pomnozmy to przez nasza wartosc, za kazdym razem
     otrzymamy taka sama predkosc niezaleznie od liczby klatek
     FPS 10, Duration of frame 0.1s, Distance per second, 1 x 10 x 0.1 = 1 
     FPS 100, Duration of frame 0.01s, Distance per second, 1 x 10 x 0.01 = 1 
         */
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeedHorizontal; // Zmienna pobierajaca os pozioma z Unity, i zmiana jaka robimy
        var newXPos = transform.position.x + deltaX; // Zmienna przechowujaca wartosc o jaka zmieniamy polozenie gracza, polozenie plus zmiana polozenia
        transform.position = new Vector2(newXPos, transform.position.y); //formula ktora przesowa gracza (przesowa jego os Y) o wartosc newXPos, zmienia polozenie i mowi zostan tu, w osi Y
        /* Ruch w pionie */
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeedVertical;
        var newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(newXPos, newYPos);// << ważne nie dajemy kolejnego transforma, tylko nowa pozycje Y!!!
    }
}
