using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        gameObject.AddComponent<DataManager>();
        data = GetComponent<DataManager>();
        DontDestroyOnLoad(gameObject);
    }

    DataManager data;
    public DataManager Data { get {  return data; } }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
        data.Reset();
    }
}
