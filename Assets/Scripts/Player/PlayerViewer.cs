using TMPro;
using UnityEngine;

public class PlayerViewer : MonoBehaviour
{
    [SerializeField] TMP_Text lifeText;

    public void OnHPModified(int life)
    {
        lifeText.text = life.ToString();
    }
}
