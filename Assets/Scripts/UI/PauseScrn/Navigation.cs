using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
	public Button btnResume;
	private TextBoxManager manager;
	public GameObject Options_Panel;
	public GameObject Control_Panel;
	public GameObject Notebook_Option;
	public Navigation_Notebook notebookManager;

	// Use this for initialization
	void Start () {
		btnResume.Select ();
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
	}

	public void selectResume(){
		gameObject.SetActive(false);
		manager.enablePlayer ();
		GameManager.getInstance().isPaused = false;
	}

	public void selectNotebook(){
		notebookManager.startNotebook();
		gameObject.SetActive(false);
	}
	public void selectControls(){
		Control_Panel.SetActive(true);
		gameObject.SetActive(false);
	}

	public void selectOptions(){
		Options_Panel.SetActive (true);
		gameObject.SetActive(false);
	}


	public void selectQuit(){
		SceneManager.LoadScene("NewTitleScene");
	}
}
