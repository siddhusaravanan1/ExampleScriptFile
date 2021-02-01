using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        StartCoroutine(nextScene());
    }
    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(29);
        SceneManager.LoadScene("MainMenu");
    }
}
