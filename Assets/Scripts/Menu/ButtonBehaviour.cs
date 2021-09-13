using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public void Close()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void PlayClip(AudioClip clip)
    {
        GameManager.Instance.PlayClip(clip);
    }

    public void ResetGame()
    {
        GameManager.Instance.ResetGame();
    }

    public void NextTurn()
    {
        if (GameManager.Instance.battleManager.playerMove == true)
        {
            GameManager.Instance.battleManager.NextTurn();
        }
    }
}