using TMPro;
using UnityEngine;

public class VersionDisplayer : MonoBehaviour
{
    public TMP_Text versionLabel;

    void Start()
    {
        versionLabel.text = Application.version;
        Destroy(this);
    }
}
