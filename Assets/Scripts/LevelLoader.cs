using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    float transitionTime = 1f;
    int levelIndex;
    int nextlevelIndex;
    void Start(){
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        nextlevelIndex = nextLevelPointer(levelIndex);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            if (levelIndex == 0){
                LoadNextLevel();
            }
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nextlevelIndex);
    }
    int nextLevelPointer(int lvlIndex){
        if (lvlIndex == 2){
            return 1;
        } else {
            return lvlIndex + 1;
        }
    }
}
