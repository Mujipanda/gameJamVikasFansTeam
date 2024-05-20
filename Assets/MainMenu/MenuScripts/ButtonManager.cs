using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{
     

    public void ChangeScene(string newGameLevel)

    {
        SceneManager.LoadScene(newGameLevel);
    }


    public void QuitToDesktop()
    {
        Application.Quit();
        Debug.Log("Quiting to Desktop");
    }

}
