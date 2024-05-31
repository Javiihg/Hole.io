using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PlayGame() {
    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
}

public void QuitGame() {
    Application.Quit();
}
}
