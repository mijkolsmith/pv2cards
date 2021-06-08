using UnityEngine;

public class GoToMenu : MonoBehaviour
{
    public void ResetGame()
    {
        Debug.Log("BUTTON CLICKED");
        GameManager.Instance.ResetGame();
    }
}