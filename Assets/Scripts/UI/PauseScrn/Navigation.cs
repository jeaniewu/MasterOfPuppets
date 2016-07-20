using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
	public Button btnResume;
	private TextBoxManager manager;
	public GameObject OptionsOption;
	public Navigation_Notebook notebookManager;

	// Use this for initialization
	void Start () {
		btnResume.Select ();
		manager = GameObject.FindGameObjectWithTag ("TextBoxManager").GetComponent<TextBoxManager> ();
	}

	public void selectResume(){
		gameObject.SetActive(false);
		manager.enablePlayer ();
	}

	public void selectNotebook(){
		notebookManager.startNotebook();
		gameObject.SetActive(false);
	}

	public void selectOptions(){
		OptionsOption.SetActive(true);
		gameObject.SetActive(false);
	}
	public void selectQuit(){
		SceneManager.LoadScene("NewTitleScene");
	}
}
