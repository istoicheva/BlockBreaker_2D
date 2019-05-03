using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    public string GameOverScene = "Game Over";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EditorSceneManager.LoadScene(GameOverScene);
    }
}
