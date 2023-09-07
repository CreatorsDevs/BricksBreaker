using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string currentlLevelName;
    public void onClick()
    {
        int level = 0;
        if(currentlLevelName == Strings.yellowLevel)
        {
            level = (int)LevelName.Yellow;
            SceneManager.LoadScene(level);
        }
        if(currentlLevelName == Strings.magentaLevel)
        {
            level = (int)LevelName.Magenta;
            SceneManager.LoadScene(level);
        }
        if( currentlLevelName == Strings.cyanLevel)
        {
            level = (int)LevelName.Cyan;
            SceneManager.LoadScene(level);
        }
    }
}
