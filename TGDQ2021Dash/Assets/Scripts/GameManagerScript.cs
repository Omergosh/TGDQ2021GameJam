using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        dashAction = playerInput.actions["Dash"];
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

    void ResetGame()
    {
        winScreen.SetActive(false);
    }
}
