using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    GameObject[] characterCards = new GameObject[4];
    List<GameObject> playerCharacters = new List<GameObject>();
    bool isInCollision = false;

    public GameObject LevelLoad;
    public GameObject gameStatus;

    [Header("Character Prefabs")]
    public GameObject ArcherPrefab;
    public GameObject AssassinPrefab;
    public GameObject WarriorPrefab;
    public GameObject PaladinPrefab;

    [Header("Character Prefab Spawn Locations")]
    public GameObject ArcherPrefabSpawnLocation;
    public GameObject AssassinPrefabSpawnLocation;
    public GameObject WarriorPrefabSpawnLocation;
    public GameObject PaladinPrefabSpawnLocation;

    GameObject uiCanvas;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GameObject.Find("Canvas");
        gameStatus = GameObject.Find("GameStatus");
        characterCards = GameObject.FindGameObjectsWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {
        //if we dont have a ui canvas find it
        if (!uiCanvas)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
    }

    public void Load()
    {
        uiCanvas.SetActive(false);
        //loop through all player cards
        foreach (GameObject c in characterCards)
        {
            //if the current card we are on is selected create 
            if (c.GetComponent<CharacterCardScript>().isSelected)
            {
                //if archer
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Archer)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(ArcherPrefab, ArcherPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Archer"));
                }
                //if warrior
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Warrior)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(WarriorPrefab, WarriorPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Warrior"));
                }
                //if paladin
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Paladin)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(PaladinPrefab, PaladinPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Paladin"));
                }
                //if assassin
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Assassin)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(AssassinPrefab, AssassinPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Assassin"));
                }


            }
        }
        //LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.FindGameObjectWithTag("Player"));
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("CollisionEntered");
        if (col.transform.tag == "Player")
        {
            if (isInCollision == false)
            {
                isInCollision = true;
                //Do stuff
                GameObject.Destroy(GetComponent<BoxCollider>());
                //SceneManager.LoadScene(2);
                Scene sceneToLoad = SceneManager.GetSceneAt(0); //is my main level
                if (playerCharacters.Count == 0)
                {
                    //playerCharacters = GameObject.FindGameObjectsWithTag("Player");
                }

                for (int i = 0; i < playerCharacters.Count; i++)
                {
                    //reparent the object to the root of the scene
                    playerCharacters[i].transform.SetParent(gameStatus.transform);
                    //SceneManager.MoveGameObjectToScene(playerCharacters[i], sceneToLoad);
                }
                SceneManager.LoadScene(sceneToLoad.name);
            }
        }
    }

    //public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null);
}
