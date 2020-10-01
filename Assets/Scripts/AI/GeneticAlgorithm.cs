using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class GeneticAlgorithm : MonoBehaviour {

    #region Parameters
    public float bestDistanceOverall = 0;
    public float bestDistancePerGeneration = 0.0f;
    public int bestIndex = 0;
    public float secondBestDistancePerGeneration = 0.0f;
    public int secondBestIndex = 0;
    public int totalPopulationPerGeneration = 25;
    int currentPopulationCount;
    int generation=0;
    public GameObject CarAgent;
    public bool loadFromFile = false;
    public static bool loaded = false;

    List<NeuralNetwork> children = new List<NeuralNetwork>();

    public Text Populationtext;
    #endregion
	
    #region Start Method
    // Use this for initialization
	void Start () {

        int i;

        if (loadFromFile == false){

            //Create initial population
            for (i = 0; i < totalPopulationPerGeneration; i++)
            {
                children.Add(new NeuralNetwork());
            }

            StartCoroutine(Generate());

        }
        else{

            Thread.Sleep(5000);
            loaded = true;
            CarAgent.GetComponent<MLCarController>().network = new NeuralNetwork("Assets/Scripts/AI/network.txt");


        }
               
	}
    #endregion

    #region Update Method
	// Update is called once per frame
	void Update () {

        Populationtext.text = "Population: "+(currentPopulationCount+1)+"/"+totalPopulationPerGeneration
            +"\nGeneration: "+generation
            +"\nBest: "+ bestDistanceOverall
            + "\nBest distance: " + bestDistancePerGeneration
            + "\nSecond Best distance: " + secondBestDistancePerGeneration;

	}
    #endregion

    #region Reset Car Position
    void ResetCar(){

        CarAgent.transform.position = new Vector3(15.6f, 41.593f, 329.3f);
        CarAgent.transform.eulerAngles = new Vector3(0, 180f, 0);
        CarAgent.GetComponent<MLCarController>().TotalDistance = 0;
        CarAgent.GetComponent<MLCarController>().hasCollided = false;
        CarAgent.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        CarAgent.gameObject.GetComponent<MLCarController>().lastLocation = new Vector3(15.6f, 41.593f, 329.3f);

    }
    #endregion

    #region Genetic Algorithm
    IEnumerator Generate(){

        float distance;
        int i;

        while (true){

            generation++;
            bestDistancePerGeneration = 0;
            secondBestDistancePerGeneration = 0;

            for (currentPopulationCount = 0; currentPopulationCount < totalPopulationPerGeneration; currentPopulationCount++){

                ResetCar();
                CarAgent.GetComponent<MLCarController>().network = children[currentPopulationCount];
                yield return new WaitUntil(()=>CarAgent.GetComponent<MLCarController>().hasCollided);
                distance = CarAgent.GetComponent<MLCarController>().TotalDistance;

                if (distance > bestDistanceOverall){

                    bestDistanceOverall = distance;
                
                }

                if (distance > bestDistancePerGeneration) {

                    secondBestDistancePerGeneration = bestDistancePerGeneration;
                    secondBestIndex = bestIndex;
                    bestDistancePerGeneration = distance;
                    bestIndex = currentPopulationCount;

                }

                else if (distance > secondBestDistancePerGeneration){

                    secondBestDistancePerGeneration = distance;
                    secondBestIndex = currentPopulationCount;

                    }

            }

            NeuralNetwork parentA = new NeuralNetwork(children[bestIndex]);
            NeuralNetwork parentB = new NeuralNetwork(children[secondBestIndex]);
            
            children.Clear();

            for (i = 0; i < totalPopulationPerGeneration; i++){

                children.Add(new NeuralNetwork(parentA, parentB));

            }

        }

    }
    #endregion

    
}
