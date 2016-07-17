using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour {

    private Animator creditsAnimator;

    static int doneStateHash;

	// Use this for initialization
	void Start () {
        creditsAnimator = GetComponent<Animator>();
        doneStateHash = Animator.StringToHash("Base Layer.Done");
        StartCoroutine(waitForEnd());
    }

    void Update() {
        if (Input.GetButtonDown("Interact")) {
            creditsAnimator.speed = 4;
        } 
        if (Input.GetButtonUp("Interact")) {
            creditsAnimator.speed = 1;
        }
    }

    IEnumerator waitForEnd () {
        while (creditsAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != doneStateHash) {
            yield return null;
        }
        ThemeMusicManager.instance.fadeOutMainTheme();
        SceneManager.LoadScene("NewTitleScene");
        yield return null;
    }
}
