using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

    public Button newGameButton;
    public Button continueButton;
    public Button noButton;
    public Image background;
    public AnimationClip warningAnimation;
    private Animator titleAnimator;
    private bool isGameSaved;

    static int logoFadeStateHash;
    static int doneStateHash;

    public GameObject newGameWarning;
    public GameObject bottomButtons;

    // Use this for initialization
    void Start () {
        //Get animator and set the animation states hashes
        titleAnimator = GetComponent<Animator>();
        logoFadeStateHash = Animator.StringToHash("Base Layer.Fade In Logo");
        doneStateHash = Animator.StringToHash("Base Layer.AllDone");

        isGameSaved = checkIfGameIsSaved();
        continueButton.interactable = isGameSaved;

        //Start the coroutine that will wait for the animation states
        StartCoroutine(waitForLogoAndStartMusic());
        StartCoroutine(waitForNewGameButtonAndSelect());
        titleAnimator.SetBool("isGameReady", true);
    }

   
    //Start playing the music when the logo starts fading in
    IEnumerator waitForLogoAndStartMusic() {
        while(titleAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != logoFadeStateHash) {
            Debug.Log("Still waiting");
            yield return null;
        }
		ThemeMusicManager.getInstance().startTitleTheme();
        yield break;
    }

    //Select the button when we get to the done state
    IEnumerator waitForNewGameButtonAndSelect() {
        while (titleAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != doneStateHash) {
            yield return null;
        }
        Debug.Log("Got to done");
        newGameButton.Select();
        yield break;
    }

    //Check if there is a save file. If so, then we can show the Continue button. 
    private bool checkIfGameIsSaved () {
        return System.IO.File.Exists(Application.persistentDataPath + "/MasterOfPuppets.dat");
    }

    //Trigger when start new game button is clicked
    public void startNewGameButton() {
        if (isGameSaved) {
            StartCoroutine(openNewGameWarning());
        } else {
            loadFirstScene();
        }
    }

    //Go back to the main menu from the warning screen
    public void goBackToMainMenu() {
        StartCoroutine(closeNewGameWarning());
    }

    //TODO: If have time fix these waiting states to wait for the animation state instead of using the animation length (like above). For now this works.

    //Opens new game warning and selects the No options
    IEnumerator openNewGameWarning() {
        titleAnimator.SetBool("showNewGameWarning", true);
        yield return new WaitForSeconds(warningAnimation.length);
        noButton.Select();
    }

    //Closes new game warning and selects the new game options
    IEnumerator closeNewGameWarning() {
        titleAnimator.SetBool("showNewGameWarning", false);
        yield return new WaitForSeconds(warningAnimation.length);
        newGameButton.Select();
    }

    //To actually start the scene
    public void loadFirstScene() {
        SceneManager.LoadScene("OpenScene");
    }

	//Reset Save data then load first scene
	public void loadFirstSceneWithReset() {
		GameManager.getInstance().secretItemFound = new bool[5];
		loadFirstScene ();
	}

    //Continue the game
    public void continueGame() {
        GameManager.getInstance().Load();
    }
    
    //Quit the game
    public void exitGameToDesktop() {
        Application.Quit();
    }

    //For Debugging Purposes there is a secret button in the top right corner
    public void openSceneSelect() {
        SceneManager.LoadScene("SceneSelect");
    }

    //This is used to rehide the mouse, if the player exits the app and comes back to the title screen
    public void hideMouseAgainAndSelect() {
        //Disabling the cursor and locking it, as we have no mouse interaction in the game.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //select either the newGameButton or the No button
        if (newGameWarning.activeSelf) {
            noButton.Select();
        } else if (bottomButtons.activeSelf){
            newGameButton.Select();
        }
    }

}
