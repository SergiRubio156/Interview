using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }


    public string GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();        
        return currentScene.name;
    }


    // Update is called once per frame
    public void ChangeScene(string _name)
    {

        if (_name != GetCurrentSceneName())
        {
            switch (_name)
            {
                case "Menu":
                    SceneManager.LoadScene(_name);

                    break;
                case "Play":
                    SceneManager.LoadScene(_name);
                    break;

                case "Laser":
                    levelManager.ChooseLevel();
                    break;

            }
        }
    }

    public void RemoveLevel(string _name)
    {
        levelManager.RemoveLvlList(_name);
    }

    public string GetLevelName()
    {
        return levelManager.GetNameLevel();
    }
}
