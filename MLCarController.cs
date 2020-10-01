using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;

public class MLCarController : MonoBehaviour {

    #region Parameters
    private CarController carAgent;
    public NeuralNetwork network;
    private float inSpeed = 0f;
    RaycastHit rayDistanceFRONT, rayDistanceLEFT, rayDistanceRIGHT, rayDistanceLEFT45, rayDistanceRIGHT45;
    Vector3 Left45 = new Vector3(0.5f,0, 0.5f);
    Vector3 Right45 = new Vector3(-0.5f, 0, 0.5f);
    Vector3 Left = new Vector3(1, 0, 0);
    Vector3 Right = new Vector3(-1, 0, 0);
    float distanceFront, distanceLEFT, distanceRIGHT, distanceLEFT45, distanceRIGHT45;
    double predictedSteering, predictedAcceeration;
    public Vector3 lastLocation;
    public Text StatsText;
    public float TotalDistance = 0;
    public bool hasCollided = false;
    public bool hasDoneLap = false;
    #endregion

    #region Start Method
    // Use this for initialization
    void Start () {
        
        lastLocation = this.transform.position;

    }
    #endregion
    
    #region Awake Method
    private void Awake(){

        carAgent = GetComponent<CarController>();

    }
    #endregion

    #region Update Method | Distances
    private void Update(){

        if(Input.GetKey(KeyCode.Escape)){

            Application.Quit();

        }

        Vector3 currentPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);

        if(Physics.Raycast(currentPosition, transform.TransformDirection(Vector3.forward), out rayDistanceFRONT)){

            Debug.DrawRay(currentPosition, transform.TransformDirection(Vector3.forward) * rayDistanceFRONT.distance, Color.red);
            distanceFront = rayDistanceFRONT.distance;

        }

        if(Physics.Raycast(currentPosition, transform.TransformDirection(Left), out rayDistanceLEFT)){

            Debug.DrawRay(currentPosition, transform.TransformDirection(Left) * rayDistanceLEFT.distance, Color.red);
            distanceLEFT = rayDistanceLEFT.distance;

        }

        if(Physics.Raycast(currentPosition, transform.TransformDirection(Right), out rayDistanceRIGHT)){

            Debug.DrawRay(currentPosition, transform.TransformDirection(Right) * rayDistanceRIGHT.distance, Color.red);
            distanceRIGHT = rayDistanceRIGHT.distance;

        }

        if(Physics.Raycast(currentPosition, transform.TransformDirection(Left45), out rayDistanceLEFT45)){

            Debug.DrawRay(currentPosition, transform.TransformDirection(Left45) * rayDistanceLEFT45.distance, Color.red);
            distanceLEFT45 = rayDistanceLEFT45.distance;

        }

        if(Physics.Raycast(currentPosition, transform.TransformDirection(Right45), out rayDistanceRIGHT45)){

            Debug.DrawRay(currentPosition, transform.TransformDirection(Right45) * rayDistanceRIGHT45.distance, Color.red);
            distanceRIGHT45 = rayDistanceRIGHT45.distance;

        }

    }
    #endregion

    #region Collision Check
    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.tag == "Wall"){

            hasCollided = true;

        }
        
    }
    #endregion

    #region Finish Check
    private void OnTriggerEnter(Collider collider){
        
        if(collider.gameObject.tag == "Finish" && hasDoneLap == false && GeneticAlgorithm.loaded==false){

            network.save();
            hasDoneLap = true;

        }

    }
    #endregion

    #region Interpret Prediction
    void interpretPrediction(ref double output){

        output = 2 * output - 1;

    }
    #endregion

    #region Car Controller
    private void FixedUpdate(){

        TotalDistance += Vector3.Distance(this.transform.position, lastLocation);
        inSpeed = Vector3.Distance(this.transform.position, lastLocation) / Time.deltaTime;
        lastLocation = transform.position;

        float[] Inputs = { inSpeed, distanceFront, distanceLEFT, distanceRIGHT, distanceLEFT45 , distanceRIGHT45 };
        network.predict(Inputs,ref predictedSteering, ref predictedAcceeration);
        
        if(predictedSteering<0.5) Debug.Log("Left");
        else if(predictedSteering == 0.5) Debug.Log("Straight");
            else Debug.Log("Right");
        
        interpretPrediction(ref predictedSteering);
        carAgent.Move((float)predictedSteering,1,1,0f);

        StatsText.text = "Speed: " + inSpeed + "\nDistance: " + TotalDistance;

    }
    #endregion

}
