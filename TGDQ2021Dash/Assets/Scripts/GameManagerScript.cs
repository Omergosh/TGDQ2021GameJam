using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Game entities
    public CreatureScript playerCreature;
    public List<NPCScript> NPCCreatures;

    // UI
    public GameObject winScreen;

    // Input management
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction resetAction;

    bool gameEnded = false;

    // Start is called when the script instance is being loaded
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        dashAction = playerInput.actions["Dash"];
        resetAction = playerInput.actions["Reset"];
    }

    private void OnEnable()
    {
        dashAction.performed += Dash;
    }

    private void OnDisable()
    {
        dashAction.performed -= Dash;
    }

    // Update is called once per frame
    void Update()
    {
        playerCreature.inputMovement = moveAction.ReadValue<Vector2>();

        if (!gameEnded)
        {
            if (CheckWinCondition())
            {
                Debug.Log("Tag, everyone's it! We all win.");
                winScreen.SetActive(true);
            }
        }
        
        if (resetAction.IsPressed())
        {
            ResetGame();
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        playerCreature.ApplyDashForce();
    }

    void ResetGame()
    {
        // Some actual process for resetting the game board?
        //winScreen.SetActive(false);

        // Nah, just reload the entire scene.
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    bool CheckWinCondition()
    {
        gameEnded = true;

        // If anyone has not been tagged, the game is not over. Otherwise, it is! Congratulations!
        foreach(NPCScript npcCreature in NPCCreatures)
        {
            if (!npcCreature.hasBeenTagged)
            {
                gameEnded = false;
            }
        }

        return gameEnded;
    }
}
