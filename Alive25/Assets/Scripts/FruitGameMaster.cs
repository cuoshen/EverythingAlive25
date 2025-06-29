using System.Collections.Generic;
using UnityEngine;

public class FruitGameMaster : MonoBehaviour
{
    [SerializeField]
    private AudioSource ambientAudio;

    [SerializeField]
    private List<FruitController> fruits = new List<FruitController>();

    private const int ArmyCount = 2;

    private void OnEnable()
    {
        ambientAudio.Play();

        foreach (FruitController fruit in fruits)
        {
            fruit.InjectGameMaster(this);
        }

        for (int armyID = 0; armyID < ArmyCount; armyID++)
        {
            TryEnableInputForOneFruit(armyID);
        }
    }

    private bool TryEnableInputForOneFruit(int armyID)
    {
        foreach (FruitController fruit in fruits)
        {
            if (fruit.ArmyID == armyID)
            {
                fruit.EnablePlayerControl();
                return true;
            }
        }

        return false;
    }

    public void ReportFruitDeath(FruitController dyingFruit)
    {
        Debug.Log(dyingFruit.name + " has died");

        fruits.Remove(dyingFruit);

        if (dyingFruit.IsPlayerControlled)
        {
            // Resurrect
            int armyID = dyingFruit.ArmyID;
            if (TryEnableInputForOneFruit(armyID))
            {
                Debug.Log("Army " + armyID + " has resurrected! Prepare for war!");
            }
            else
            {
                // No more resurrections, game reaches its end
                GameState globalState = GameState.Instance;
                if (armyID == 0)
                {
                    globalState.Player1HP = 0;
                }
                else
                {
                    globalState.Player2HP = 0;
                }
            }
        }
    }
}
