using UnityEngine;
using System.Collections;

public class MenuManger : MonoBehaviour {

    public void LoadGame(int scene)
    {
        if (scene > 0)
        {
            Application.LoadLevel(scene);

        }

    }

    public void SaveGame(int scene)
    {
    }

    public void Settings()
    {
    }
    public void ExitGame()
    {
    
        Application.Quit();
    }


}
