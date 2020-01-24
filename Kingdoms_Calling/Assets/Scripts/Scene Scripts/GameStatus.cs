//  Name: GameStatus.cs
//  Author: Connor Larsen
//  Date: 1/24/2020
//  Resource: Unity Tutorial: Preserving Data between Scene Loading/Switching
//  Link: https://www.youtube.com/watch?v=WchH-JCwVI8

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    // Variables to be stored when switching scenes
    private Health p1Health, p2Health, p3Health, p4Health;

    // Start is called before the first frame update
    void Start()
    {
        
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