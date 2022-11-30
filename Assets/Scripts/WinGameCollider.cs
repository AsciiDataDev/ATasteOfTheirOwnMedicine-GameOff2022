using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinGameCollider : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private bool useName;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (useName)
            {
                GameObject.FindObjectOfType<SceneChanger>().ChangeScene(nextLevelName);
            }
            else
            {
                GameObject.FindObjectOfType<SceneChanger>().NextSceneIndex(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
