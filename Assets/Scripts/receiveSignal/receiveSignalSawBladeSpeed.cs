using UnityEngine;
using System.Collections;

public class receiveSignalSawBladeSpeed : receiveSignal {

    private MovingPlatform[] sawBlades;

    public float speed1;
    public float speed2;

	// Use this for initialization
	void Start () {
        sawBlades = GetComponentsInChildren<MovingPlatform>();
	}

    public override void activate() {
        foreach(MovingPlatform sawBlade in sawBlades) {
            sawBlade.moveSpeed = speed2;
        }
    }

    public override void deactivate() {
        foreach (MovingPlatform sawBlade in sawBlades) {
            sawBlade.moveSpeed = speed1;
        }
    }
}
