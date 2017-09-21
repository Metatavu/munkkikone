#pragma strict
public var rb: Rigidbody;
public var movement;
public var movementSpeed = 0.2;


function Update () {
    
}

function OnCollisionEnter (col : Collision) {
	rb = GetComponent.<Rigidbody>();
    if(col.gameObject.tag == "hihna") {
		transform.Translate(Vector3.back * Time.deltaTime * movementSpeed, Space.World);
	} else if(col.gameObject.tag == "paistokoppi_hihna") {
		transform.Translate(Vector3.right * Time.deltaTime * movementSpeed, Space.World);
	} else if(col.gameObject.tag == "sokerirumpu") {
		transform.Translate(Vector3.left * Time.deltaTime * (movementSpeed * 0.5), Space.World);
	} else if(col.gameObject.tag == "rumpuliuska") {
		//movement = new Vector3(0, 0, 1.0);
		//rb.AddForce(movement);
	}
}

function OnCollisionStay(col : Collision) {
	rb = GetComponent.<Rigidbody>();
    if(col.gameObject.tag == "hihna") {
		transform.Translate(Vector3.back * Time.deltaTime * movementSpeed, Space.World);
	} else if(col.gameObject.tag == "paistokoppi_hihna") {
		transform.Translate(Vector3.right * Time.deltaTime * movementSpeed, Space.World);
	} else if(col.gameObject.tag == "sokerirumpu") {
		transform.Translate(Vector3.left * Time.deltaTime * (movementSpeed * 0.5) , Space.World);
	} else if(col.gameObject.tag == "rumpuliuska") {
		//movement = new Vector3(0, 0, 1.0);
		//rb.AddForce(movement);
	}
}
