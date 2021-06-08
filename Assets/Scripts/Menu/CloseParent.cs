using UnityEngine;

public class CloseParent : MonoBehaviour
{
    public void Close()
    {
        transform.parent.gameObject.SetActive(false);
    }
}