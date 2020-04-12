/*
 * Level management script which handles the character select screen cursor and character input switch aswell as level loading
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 04/01/20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    GameObject[] characterCards = new GameObject[4];
    List<GameObject> playerCharacters = new List<GameObject>();
    bool isInCollision = false;

    public float timeRequiredInAreaToLoad = 2f;
    public int numPlayerInAreaToLoad = 1;

    public GameObject LevelLoad;
    public GameObject gameStatus;
    public Text testText;

    [Header("Character Prefabs")]
    public GameObject ArcherPrefab;
    public GameObject AssassinPrefab;
    public GameObject WarriorPrefab;
    public GameObject PaladinPrefab;

    [Header("Scene to load")]
    public string SceneName;

    [Header("Character Prefab Spawn Locations")]
    public GameObject ArcherPrefabSpawnLocation;
    public GameObject AssassinPrefabSpawnLocation;
    public GameObject WarriorPrefabSpawnLocation;
    public GameObject PaladinPrefabSpawnLocation;

    GameObject uiCanvas;
    private float timeInArea = 0;
    private int playerInArea = 0;
    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GameObject.Find("StartGame Menu");
        gameStatus = GameObject.Find("GameStatus");
        characterCards = GameObject.FindGameObjectsWithTag("Character");
        if (SceneName == "")
        {
            SceneName = "PlainsMap";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if we dont have a ui canvas find it
        if (!uiCanvas)
        {
            uiCanvas = GameObject.Find("StartGame Menu");
        }

        if (characterCards.Length == 0)
        {
            characterCards = GameObject.FindGameObjectsWithTag("Character");
        }
    }

    private void FixedUpdate()
    {
        if (testText)
        {
            if (testText.gameObject.activeSelf)
            {
                testText.text = "Player in load Area = " + playerInArea + " | Time in area with max players = " + timeInArea + " seconds";
            }
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
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Character_Archer(Clone)"));
                }
                //if warrior
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Warrior)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(WarriorPrefab, WarriorPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Character_Warrior(Clone)"));
                }
                //if paladin
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Paladin)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(PaladinPrefab, PaladinPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Character_Paladin(Clone)"));
                }
                //if assassin
                if (c.GetComponent<CharacterCardScript>().thisCharacter == CharacterCardScript.character.Assassin)
                {
                    GameObject.Destroy(c.GetComponent<CharacterCardScript>().selectedBy);
                    PlayerInput.Instantiate(AssassinPrefab, AssassinPrefabSpawnLocation.transform);
                    LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.Find("Character_Assassin(Clone)"));
                }
            }
        }
        //LevelLoad.GetComponent<LevelManagement>().playerCharacters.Add(GameObject.FindGameObjectWithTag("Player"));
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("CollisionEntered");
        if (collision.transform.tag == "Player")
        {
            playerInArea++;

            if (playerInArea > 0)
            {
                if (testText)
                    testText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("CollisionExited");
        if (collision.transform.tag == "Player")
        {
            playerInArea--;
            timeInArea = 0f;

            if (playerInArea < 1)
            {
                if (testText)
                    testText.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (isInCollision == false)
            {
                isInCollision = true;
            }
            else if (isInCollision == true && playerInArea >= numPlayerInAreaToLoad)
            {
                if (timeInArea >= timeRequiredInAreaToLoad)
                {
                    //Do stuff
                    GameObject.Destroy(GetComponent<BoxCollider>());
                    
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
                    if (loading == false)
                    {
                        loading = true; 
                        Debug.Log("Loading into: " + SceneName);
                        SceneManager.LoadScene(SceneName);
                    }
                }
                else
                {
                    timeInArea += Time.deltaTime;
                }
            }
        }
    }

    //public PlayerInput JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, string controlScheme = null, InputDevice pairWithDevice = null);
}
