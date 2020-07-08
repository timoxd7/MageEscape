using UnityEngine;

public class KeySwap : MonoBehaviour
{
    public GameObject normalKey;
    public GameObject swappedKey;

    public void Swap()
    {
        if (normalKey != null)
        {
            if (normalKey.activeSelf)
            {
                normalKey.SetActive(false);

                if (swappedKey != null)
                    swappedKey.SetActive(true);
            }
        }
    }
}
