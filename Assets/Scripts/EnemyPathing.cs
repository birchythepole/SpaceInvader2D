﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    /*Tworzymy zmienna typu WaveConfig przechowująca tenże plik*/
    WaveConfig waveConfig;
    /*Lista stworzona do przechowywania waypointow z poziomy edytora*/
    List<Transform> waypoints;

    int waypointIndex = 0;
	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetPathWaypoints();
        // ustawienie poczatkowej pozycji przeciwnika na pozycje pierwszego waypointa
        transform.position = waypoints[waypointIndex].transform.position;
		
	}
	public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
	// Update is called once per frame
	void Update ()
    {
        
        Move();
    }

    private void Move()
    {
        /*Metoda która jeżeli numer indeksu listy do ktorej przyczepione sa waypointy
         jest mniejsz lub rowny ilosci tych punkow wykonuje ruch wzdloz punktow
         w przeciwnym wypadku niszczy obiekt*/
        if (waypointIndex <= waypoints.Count - 1)
        {
            /*Deklarujemy zmienna ktora bedzie przechowywac wartosc wspolzednych nastepnego
             waypointa do ktorego bedziemy sie kierowac*/
            var targetPosition = waypoints[waypointIndex].transform.position;
            /*Deklarujemy zmienna ktora bedzie pzechowywac wartosc predkosci przeciwnika 
             pomnożoną przez funkcje Time.delta time tak aby prędkość obiektu była
             framerate independent*/
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            /* W aktualnej pozycj obiektu wywołujemy funkcje MoveTowards w Vector2 tak aby 
             * obiekt poruszał sie w kierunku nastpnego waypointa, jako paametry podajemy
             * aktualna pozycje, pozycje celu oraz predkosc przeciwnika */
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            /* Na konec jezeli pozycja jest rowna pozycji celu inkrementujemy indeks listy aby obiekt
             przemieszczal sie w kierunku kolejnego celu*/
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
