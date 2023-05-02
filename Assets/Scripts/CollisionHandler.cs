using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

	AudioSource audioSource;
    bool isTransitioning = false;
    bool isCollisionEnabled = true;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        NextLevelHandler();
        CollisionDisableProcess();
    }

    void NextLevelHandler()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
    }

    void CollisionDisableProcess()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isCollisionEnabled = !isCollisionEnabled;
        }
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || !isCollisionEnabled)
        {
            return;
        }
        switch (other.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                NextLevelSequence();
                break;
            case "Fuel":
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
        crashParticles.Play();
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
        successParticles.Play();
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
