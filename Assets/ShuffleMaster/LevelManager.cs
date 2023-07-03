using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level System")]
    private int whichlevel = 0;
    // [SerializeField] public List<Scriptable> levels = new List<Scriptable>();
    [SerializeField] public List<GameObject> level = new List<GameObject>();

    public GameObject mainPlayer;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        whichlevel = PlayerPrefs.GetInt("whichlevel");

        if (PlayerPrefs.GetInt("randomlevel") > 0)
        {
            whichlevel = Random.Range(0, level.Count);
        }

        level[whichlevel].SetActive(true);
        //Instantiate(levels[whichlevel].LevelPrefab, Vector3.zero, Quaternion.identity);
        //Instantiate(levels[whichlevel].Player, Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        whichlevel++;
        PlayerPrefs.SetInt("whichlevel", whichlevel);

        if (whichlevel >= level.Count)
        {
            whichlevel--;
            PlayerPrefs.SetInt("randomlevel", 1);
        }

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public int Whichlevel()
    {
        return whichlevel;
    }
}