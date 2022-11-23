using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void PlayButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstStage");
    }
}
