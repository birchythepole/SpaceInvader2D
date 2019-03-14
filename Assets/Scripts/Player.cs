using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    //Parametry konfiguracyjne
    [Header ("Player")]
    [SerializeField] [Range(1, 30)] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] [Range(0, 5000)] int health = 500;
    //Tworzy zmienna typu obiekt 
    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab;  
    [SerializeField] [Range(1, 50)] float projectileSpeed = 25f;
    [SerializeField] [Range(0, 10)] float projectileFiringPeriod = 0.5f;
    Coroutine firingCoroutine;
    [Header ("Explosion")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 2)] float shootSoundVolume = 0.25f;
    [SerializeField] [Range(0, 2)] float deathSoundVolume = 0.7f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Use this for initialization
    void Start () {
        SetUpMoveBounderies();
	}
    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}
    /*Dela.time jest funkcja, ktora powoduje iz poruszanie sie obiektu w Unity jest
     niezalezne od ilosci klatek (frame rate independent), jest to bardzo potrzebna
     funkcja gdyz sprawia ze gra ma caly czas taka sama predkosc, niezaleznie od
     predkosci komputera na ktorym gramy. Delta time mowi nam ile trwa wygenerowanie
     pojedynczej klatki, i jezeli pomnozmy to przez nasza wartosc, za kazdym razem
     otrzymamy taka sama predkosc niezaleznie od liczby klatek
     FPS 10, Duration of frame 0.1s, Distance per second, 1 x 10 x 0.1 = 1 
     FPS 100, Duration of frame 0.01s, Distance per second, 1 x 10 x 0.01 = 1*/
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;// Zmienna pobierajaca os pozioma z Unity, i zmiana jaka robimy

        
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); // Zmienna przechowujaca wartosc o jaka zmieniamy polozenie gracza, polozenie plus zmiana polozenia
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, transform.position.y); //formula ktora przesowa gracza (przesowa jego os Y) o wartosc newXPos, zmienia polozenie i mowi zostan tu, w osi Y
        transform.position = new Vector2(newXPos, newYPos);// << ważne nie dajemy kolejnego transforma, tylko nowa pozycje Y!!!
    }
    private void SetUpMoveBounderies()
    {
        /*Metoda która pobiera do zmienej kamere głowna gry, nastepnie przypisuje do zmiennych 
         xMin oraz xMax, kolejno wartosc krawedzi pola kamery, potrzebujemy tego aby miec
         zdefiniowany obszar poruszania sie gracza. Dzieki temu nawet jak zmienimy 
         rozdzielczosc kamery obszar poruszania sie graczza bedzie responsywny*/
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y+padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-padding;
    }
    private void Fire()
    {
        /*Gdy wciśniemy klawsize przypisane do Fire1 wtedy nasz If tworzy zmienna typu obiekt
         i instancjonuje ja jako klon prefabrykanta, nastepnie pobirana jest do stworzonego 
         klona wartosc velocity z komponentu Rigidbody2D i zmienina o Vektor od zera do zdefiniowanej
         predkosci pocisku, jednoczesnie aktywowona jest korutyna ktora powoduje wystrzeliwanie pociskow
         co zdefiniowana ilosc czasu, oraz zatrzymanie korutyny w przypadku puszczenia klawisza*/
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator FireContinuously()
    {
        while (true) { 
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, shootSoundVolume);
        yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        Invoke('GameOver', 2f);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
