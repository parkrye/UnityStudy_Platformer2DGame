using TMPro;
using UnityEngine;

public class PlayerViewer : MonoBehaviour
{
    [SerializeField] TMP_Text lifeText, bulletText;

    public void OnHPModified(int life)
    {
        lifeText.text = life.ToString();
    }

    public void OnBulletModifed(int bullet)
    {
        bulletText.text = bullet.ToString();
    }
}
