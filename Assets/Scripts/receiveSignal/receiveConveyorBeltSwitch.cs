using UnityEngine;
using System.Collections;

public class receiveConveyorBeltSwitch : receiveSignal {
    private ConveyorBelt[] conveyorBelts; 

    void Start () {
        conveyorBelts = GetComponentsInChildren<ConveyorBelt>();
    }

    /**
    * Switch the direction of all child conveyor Belts
    **/
    private void switchConveyorBelts(bool switched) {
        foreach (ConveyorBelt conveyorBelt in conveyorBelts) {
            conveyorBelt.setConveyorBeltDirection(switched);
        }
    }

    public override void activate() {
        switchConveyorBelts(true);
    }

    public override void deactivate() {
        switchConveyorBelts(false);
    }
}
