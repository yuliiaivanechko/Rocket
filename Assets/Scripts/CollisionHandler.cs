using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    void OnCollisionEnter(Collision other) {

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("touched friendly");
                break;
            case "Finish":
                NextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("touched fuel");
                break;
            default:
                Crash();
                break;
        }
        
    }
    void Crash()
    {
        var move = GetComponent<Movement>(); 
        move.enabled = false;
        Invoke("Reload", delay);
    }

    void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    void NextLevelSequence()
    {
        var move = GetComponent<Movement>(); 
        move.enabled = false;
        Invoke("NextLevel", delay);
    }

    void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = index + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        } 
        SceneManager.LoadScene(nextLevel);
    }
}
