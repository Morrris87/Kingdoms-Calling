//  Name: GameStatus.cs
//  Author: Connor Larsen
//  Date: 1/24/2020
//  Resource: Unity Tutorial: Preserving Data between Scene Loading/Switching - https://www.youtube.com/watch?v=WchH-JCwVI8

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    // Private Variables
    static private GameStatus instance;  // Keeps track of this instance of GameStatus, in the case of duplicates
    private Health p1Health, p2Health, p3Health, p4Health;  // Keeps track of each player's health values

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)  // If instance is already in use...
        {
            // Destroy this instance of GameStatus
            Destroy(this.gameObject);
        }
        else    // instance is not in use, meaning we can store this instance
        {
            instance = this;                           // Stores this instance of GameStatus as our current and main instance
            GameObject.DontDestroyOnLoad(instance);    // When a new scene is loaded, GameStatus is carried over, keeping hold of variables we need
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When called, loads the scene with the name matching sceneName
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}