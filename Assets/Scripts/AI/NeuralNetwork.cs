using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralNetwork : MonoBehaviour {

    #region Parameters
    /// <summary>
    /// The probability that a weight is going to be mutated.
    /// </summary>
    float mutationRate = 0.2f;

    /// <summary>
    /// The mutation amount a certain weight is going to be mutated with.
    /// </summary>
    float mutationAmount = 2.0f;

    /// <summary>
    /// The input bias which is applied to the first hidden layer.
    /// </summary>
    public float[] inputBias = new float[7];

    /// <summary>
    /// The input weights. between the Input later and the first Hidden layer.
    /// </summary>
    float[,] inputWeights = new float[6,7];

    /// <summary>
    /// The hidden bias which is applied to the second hidden layer.
    /// </summary>
    float[] hiddenBias = new float[6];

    /// <summary>
    /// The hidden weights from the first hidden layer to the second hidden layer.
    /// </summary>
    float[,] hiddenWeights = new float[7,6];

    /// <summary>
    /// The output bias which is applied to the output layer.
    /// </summary>
    float[] outputBias = new float[2];
    /// <summary>
    /// The output weights between the second hidden layer and the output layer.
    /// </summary>
    float[,] outputWeights = new float[6,2];
    #endregion

    #region Constructors
    /// <summary>
    /// Constructor for creating a new Neural Network with Random Weights in the [-2.0 -> 2.0] interval
    /// </summary>
    public NeuralNetwork(){

        int i,j;

        #region Bias Initialisation
        for (i = 0; i < 7; i++){

            inputBias[i] = Random.Range(-2f,2f);

        }

        for (i = 0; i < 6; i++){

            hiddenBias[i] = Random.Range(-2f,2f);

        }

        for (i = 0; i < 2; i++){

            outputBias[i] = Random.Range(-2f,2f);

        }
        #endregion

        #region Weights Initialisation
        for (i = 0; i < 6; i++){

            for (j = 0; j < 7; j++){

                inputWeights[i,j] = Random.Range(-2f,2f);

            }

        }

        for (i = 0; i < 7; i++){

            for(j = 0; j < 6; j++){

                hiddenWeights[i,j] = Random.Range(-2f,2f);

            }

        }

        for (i = 0; i < 6; i++){

            for(j = 0; j < 2; j++){

                outputWeights[i,j] = Random.Range(-2f,2f);

            }

        }

        #endregion


    }

    /// <summary>
    /// Constructor for creating a Neural Network with a given network
    /// </summary>
    /// <param name="child"> The Neural Network to be copied.</param>
    public NeuralNetwork(NeuralNetwork child){

        int i, j;

        #region Bias Initialisation
        for (i = 0; i < 7; i++){

            inputBias[i] = child.inputBias[i];

        }

        for (i = 0; i < 6; i++){

            hiddenBias[i] = child.hiddenBias[i];

        }

        for (i = 0; i < 2; i++){

            outputBias[i] = child.outputBias[i];

        }
        #endregion

        #region Weights Initialisation
        for (i = 0; i < 6; i++){

            for (j = 0; j < 7; j++){

                inputWeights[i,j] = child.inputWeights[i,j];

            }

        }

        for (i = 0; i < 7; i++){

            for(j = 0; j < 6; j++){

                hiddenWeights[i,j] = child.hiddenWeights[i,j];

            }

        }

        for (i = 0; i < 6; i++){

            for(j = 0; j < 2; j++){

                outputWeights[i,j] = child.outputWeights[i,j];

            }

        }

        #endregion

    }

    /// <summary>
    /// Method for crossovering two Neural Networks.
    /// </summary>
    /// <param name="parentA">First Neural Network</param>
    /// <param name="parentB">Second Neural Network</param>
    public NeuralNetwork(NeuralNetwork parentA, NeuralNetwork parentB){

        int i, j;

        #region Bias Crossover
        for (i = 0; i < 7; i++){

            inputBias[i] = i % 2 == 0 ? parentA.inputBias[i] : parentB.inputBias[i];

            if(Random.Range(0.0f,1.0f) < mutationRate){

                inputBias[i] += (float)(Random.Range(-1 * mutationAmount, mutationAmount));

            }

        }

        for (i = 0; i < 6; i++){

            hiddenBias[i] = i % 2 == 0 ? parentA.hiddenBias[i] : parentB.hiddenBias[i];

            if(Random.Range(0.0f,1.0f) < mutationRate){

                hiddenBias[i] += (float)(Random.Range(-1 * mutationAmount, mutationAmount));

            }

        }

        for (i = 0; i < 2; i++){

            outputBias[i] = i % 2 == 0 ? parentA.outputBias[i] : parentB.outputBias[i];

            if(Random.Range(0.0f,1.0f) < mutationRate){

                outputBias[i] += (float)(Random.Range(-1 * mutationAmount, mutationAmount));

            }

        }
        #endregion

        #region Weights Crossover
        for (i = 0; i < 6; i++){

            for (j = 0;j < 7; j++){

                inputWeights[i,j] = i % 2 == 0 ? parentA.inputWeights[i,j] : parentB.inputWeights[i,j];
                
                if(Random.Range(0.0f,1.0f) < mutationRate){

                    inputWeights[i,j] += (float)(Random.Range(-1 * mutationAmount, mutationAmount));

                }

            }

        }

        for (i = 0; i < 7; i++){

            for(j = 0; j < 6; j++){

                hiddenWeights[i,j] = i % 2 == 0 ? parentA.hiddenWeights[i,j] : parentB.hiddenWeights[i,j];

                if(Random.Range(0.0f,1.0f) < mutationRate){

                    hiddenWeights[i,j] += (float)(Random.Range(-1 * mutationAmount, mutationAmount));

                }

            }

        }

        for (i = 0; i < 6; i++){

            for(j = 0; j < 2; j++){

                outputWeights[i,j] = i % 2 == 0 ? parentA.hiddenWeights[i,j] : parentB.outputWeights[i,j];

                if(Random.Range(0.0f,1.0f) < mutationRate){

                    outputWeights[i,j] += (float)(Random.Range(-1 * mutationAmount, mutationAmount));

                }

            }

        }

        #endregion

    }

    /// <summary>
    /// Constructor for reading a Neural Network configuration from a file.
    /// </summary>
    /// <param name="filepath">The file path from where to read the configuration</param>
    public NeuralNetwork(string filepath){

        StreamReader streamReader = new StreamReader(filepath, true);
        string line;
        int i, j;
        try{

            line = streamReader.ReadLine();
            Debug.Log(line);
            string[] lineArray = line.Split(',');

            #region Bias Reading
            for (i = 0; i < 7; i++){

                inputBias[i] = float.Parse(lineArray[i]);

            }

            line = streamReader.ReadLine();
            Debug.Log(line);
            lineArray = line.Split(',');

            for (i = 0; i < 6; i++){

                hiddenBias[i] = float.Parse(lineArray[i]);

            }

            line = streamReader.ReadLine();
            Debug.Log(line);
            lineArray = line.Split(',');

            for (i = 0; i < 2; i++){

                outputBias[i] = float.Parse(lineArray[i]);

            }
            #endregion

            #region Weights Reading
            line = streamReader.ReadLine();
            Debug.Log(line);
            lineArray = line.Split(',');

            for (i = 0; i < 6; i++){

                for (j = 0; j < 7; j++){

                    inputWeights[i,j] = float.Parse(lineArray[i * 7 + j]);

                }

            }

            line = streamReader.ReadLine();
            Debug.Log(line);
            lineArray = line.Split(',');

            for (i = 0; i < 7; i++){

                for(j = 0; j < 6; j++){

                    hiddenWeights[i,j] = float.Parse(lineArray[i * 6 + j]);

                }

            }

            line = streamReader.ReadLine();
            Debug.Log(line);
            lineArray = line.Split(',');

            for (i = 0; i < 6; i++){

                for(j = 0; j < 2; j++){

                    outputWeights[i,j] = float.Parse(lineArray[i * 2 + j]);

                }

            }

            streamReader.Close();

            #endregion


        }catch(System.Exception e){

            Debug.Log("Error reading the file " + e.Message + e.StackTrace);

        }

    }

    #endregion

    #region File Saving
    /// <summary>
    /// Function for saving the Weights of the Neural Network into a text file.
    /// </summary>
    public void save(){

        int i,j;
        StreamWriter streamWriter = new StreamWriter("Assets/Scripts/AI/network.txt", true);

        #region Bias Saving
        for (i = 0; i < 7; i++){

            streamWriter.Write(inputBias[i] + ",");

        }

        streamWriter.Write('\n');

        for (i = 0; i < 6; i++){

            streamWriter.Write(hiddenBias[i] + ",");

        }

        streamWriter.Write('\n');

        for (i = 0; i < 2; i++){

            streamWriter.Write(outputBias[i] + ",");

        }

        streamWriter.Write("\n");
        #endregion

        #region Weights Saving
        for (i = 0; i < 6; i++){

            for (j = 0; j < 7; j++){

                streamWriter.Write(inputWeights[i,j] + ",");

            }

        }

        streamWriter.Write('\n');

        for (i = 0; i < 7; i++){

            for(j = 0; j < 6; j++){

                streamWriter.Write(hiddenWeights[i,j] + ",");

            }

        }

        streamWriter.Write('\n');

        for (i = 0; i < 6; i++){

            for(j = 0; j < 2; j++){

                streamWriter.Write(outputWeights[i,j] + ",");

            }

        }

        #endregion

        streamWriter.Close();

    }
    #endregion    

    #region Prediction
    /// <summary>
    /// Function for predicting the output of the Neural Network, given the inputs, via multiplicating the inputs with the weights of the Network, adding the Bias and then
    /// applying an Activation function to the result. This is done until we have reached the last layer, the output layer.
    /// </summary>
    /// <param name="inputs">The inputs are the speed and distances projected in 5 different dirrections from the car to the next nearest object.</param>
    /// <param name="outSteering">The predicted steering.</param>
    /// <param name="outAxeleration">The predicted axeleration</param>
    public void predict(float[] inputs, ref double outSteering, ref double outAxeleration){

        double[] firstHiddenLayer = new double[7];
        double[] secondHiddenLayer = new double[6];
        double[] outputLayer = new double[2];
        int i, j;
        
        #region Prediction Algorithm
        for (j = 0; j < 7; j++){

            for(i = 0; i < 6; i++){

                firstHiddenLayer[j] += inputs[i] * inputWeights[i,j];

            }

        }

        for (i = 0; i < 7; i++){

            firstHiddenLayer[i] = ActivationFunctions.SigmoidFunction(firstHiddenLayer[i] + inputBias[i]);

        }

        for(j = 0; j < 6; j++){

            for(i = 0; i < 7; i++){

                secondHiddenLayer[j] += firstHiddenLayer[i] * hiddenWeights[i,j];

            }

        }

        for(i = 0; i < 6; i++){

            secondHiddenLayer[i] = ActivationFunctions.SigmoidFunction(secondHiddenLayer[i] + hiddenBias[i]);

        }

        for(j = 0; j < 2; j++){

            for(i = 0; i < 6; i++){

                outputLayer[j] += secondHiddenLayer[i] * outputWeights[i,j];

            }

        }

        for(i = 0; i < 2; i++){

            outputLayer[i] = ActivationFunctions.SigmoidFunction(outputLayer[i] + outputBias[i]);

        }

        outSteering = outputLayer[0];
        outAxeleration = outputLayer[1];
        
        #endregion

    }
    #endregion

}
