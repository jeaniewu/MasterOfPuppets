using UnityEngine;
using System.Collections;

public class redEye : MonoBehaviour {

	public float size;

	// Use this for initialization
	void Start () {
		this.transform.localScale = Vector3.zero;
	}

	public IEnumerator enlargeObject(){
		while (this.transform.localScale.x < size) {
			this.transform.localScale += new Vector3(0.01F, 0.01F, 0);
			yield return null;
		}

	}

	public IEnumerator fadeBlack(){
		while (GetComponent<SpriteRenderer> ().color != Color.black){
			Color color = (Vector4) GetComponent<SpriteRenderer> ().color - new Vector4 (0.05F, 0.05F, 0.05F, 0);
			GetComponent<SpriteRenderer> ().color = color;
			yield return null;
		}
			
	}
}
