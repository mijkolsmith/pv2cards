using UnityEngine;
using System.Linq;

public class EndTurn : MonoBehaviour
{
    public void NextTurn()
    {
        if (GameManager.Instance.battleManager.playerMove == true)
        {
            GameManager.Instance.battleManager.NextTurn();
        }
    }
}