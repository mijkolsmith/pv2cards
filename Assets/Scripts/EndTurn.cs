using UnityEngine;

public class EndTurn : ScriptableObject
{
    public void NextTurn()
    {
        if (GameManager.Instance.battleManager.playerMove == true)
        {
            GameManager.Instance.battleManager.NextTurn();
        }
    }
}