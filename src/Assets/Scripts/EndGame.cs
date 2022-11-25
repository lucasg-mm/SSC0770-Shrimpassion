using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private AudioSource finishSound;
    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnderGame2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("here");
            finishSound.Play();
            Invoke("CompleteGame", 2f);
            CompleteGame();
        }
    }

    private void CompleteGame()
    {
        SceneManager.LoadScene("EndGame");
    }
}