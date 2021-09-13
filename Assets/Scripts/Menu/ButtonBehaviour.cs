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
}