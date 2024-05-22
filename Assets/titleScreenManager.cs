using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleScreenManager : MonoBehaviour
{
  

    [SerializeField]
    private GameObject imageObj;
    private void Start()
    {
     
        StartCoroutine(title());
    }

    IEnumerator title()
    {
        float duration = 1.5f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float a = Mathf.Lerp(1, 0, t);
            imageObj.GetComponent<Image>().color = new Color(0, 0, 0, a);
           
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("MainMenu");

        yield return null;
    }
}
