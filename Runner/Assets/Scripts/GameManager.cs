using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] partical;
    public PlayerControl playerControl;
    public Animator anim;
    public GameObject[] Panels;
    public TextMeshProUGUI score;
    public void AnimStart()
    {
        anim.SetBool("Move", true);
    }public void Win()
    {
        foreach (var item in partical)
        {
            item.SetActive(true);
        }
        score.text = "Collectables : " + (playerControl.transform.childCount - 2);
        Panels[1].SetActive(true);

    }
    public void Lose()
    {
        Time.timeScale = 0f;
        Panels[0].SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
