using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject on;
    public GameObject off;

    public AudioSource BG;
    public AudioSource click;
    public void Play()
    {
        SceneManager.LoadScene("IntroCutScene");
        click.Play();
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
        click.Play();
    }
    public void Quit()
    {
        Application.Quit();
        click.Play();
    }
    public void OptionBack()
    {
        SceneManager.LoadScene("MainMenu");
        click.Play();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        click.Play();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        click.Play();
    }
    public void On()
    {
        on.SetActive(false);
        off.SetActive(true);
        BG.UnPause();
    }
    public void Off()
    {
        off.SetActive(false);
        on.SetActive(true);
        BG.Pause();
    }
    public void optionOn()
    {
        BG.UnPause();
    }
    public void optionOff()
    {
        BG.Pause();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        click.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public  void Skip()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
