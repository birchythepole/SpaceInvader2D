using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Config Menu")]
public class WaveConfig : ScriptableObject {

    /*W tym skrypcie tworzymy zmienne kore będa danymi używanymi przez pozostałe skrypty i obiekty
     w grze. Plik jest plikiem konfiguracyjnym dlatego warto dodać wartości Range aby łatwo edytowac
     ustawienia w poziomy Silnika. Musimy oczywiście w skrypcie stworzyć odpowiednie metody
     publiczne zwracające poszczególne wartości tak aby inne skrypty mogły z nich skorzystać*/
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField][Range (0,10)] float timeBetweenSpawns = 0.5f;
    [SerializeField][Range(0, 10)] float spawnRandomFactor = 0.3f;
    [SerializeField][Range(0, 10)] float moveSpeed = 2f;
    [SerializeField][Range(0, 10)] int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetPathWaypoints() {
        /*Ta metoda tworzy liste w obiekcie pathPrefab i dla kazdego kolejnego "dziecka" obiektu
         dodaje indeks do listy*/
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        
        return waveWaypoints; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public float GetMoveSpeed() { return moveSpeed; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
}
