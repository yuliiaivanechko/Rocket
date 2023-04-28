using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
	AudioSource audioSource;
    bool isTransitioning = false;

void Start() {
    audioSource = GetComponent<AudioSource>();
}

    void OnCollisionEnter(Collision other) {
        if (isTransitioning)
        {
            return;
        }
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
        audioSource.Stop();
        isTransitioning = true;
        var move = GetComponent<Movement>(); 
        move.enabled = false;
        audioSource.PlayOneShot(crash);
        Invoke("Reload", delay);
    }

    void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    void NextLevelSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        var move = GetComponent<Movement>(); 
        move.enabled = false;
        audioSource.PlayOneShot(success);
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
