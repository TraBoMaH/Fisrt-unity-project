using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
public void StartSceneTutorial()
{
SceneManager.LoadScene("SampleScene");
}
public void StartSceneMainGame()
{
SceneManager.LoadScene("2");
}
}
