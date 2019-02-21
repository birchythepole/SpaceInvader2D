using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    /* Metoda Publicznia ktora powoduje, że przy zetknieciu z jakims
     Obiektem Gry obiekt ten jest niszczony*/
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
