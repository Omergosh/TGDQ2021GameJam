using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public CreatureScript playerCreature;
    public List<NPCScript> NPCCreatures;

    bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            if (CheckWinCondition())
            {
                Debug.Log("Tag, everyone's it! We all win.");
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
}
