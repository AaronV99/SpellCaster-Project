using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator transitionAnim;
    [SerializeField] private string sceneName;


    public void changeScene()
    {
        StartCoroutine(LoadScene());
    }

    public void thisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
