using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    public GameObject CarObject;
    public float CarX;
    public float CarY;
    public float CarZ;

    // Update is called once per frame
    void Update() {
        
        CarX = CarObject.transform.eulerAngles.x;
        CarY = CarObject.transform.eulerAngles.y;
        CarZ = CarObject.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(CarX-CarX, CarY, CarZ-CarZ);

    }
    
}
