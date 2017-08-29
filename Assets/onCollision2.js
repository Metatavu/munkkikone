#pragma strict
public var rb: Rigidbody;

function Start() {
	rb = GetComponent.<Rigidbody>();
}

function OnCollisionEnter (col : Collision) {

	if(col.gameObject.name == "liukuhihnaCollider") {
		var movement = new Vector3(0, -4.0, 0);
		rb.AddForce(movement);
	} else {
		var movement2 = new Vector3(-3.0, 0, 0);
		rb.AddForce(movement2);
	}
    
}