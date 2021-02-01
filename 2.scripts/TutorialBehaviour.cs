using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBehaviour : MonoBehaviour
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
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Level1");
    }
}
