using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
	public double[] input;
	public double output;
}

public class Perceptron : MonoBehaviour
{

	//public TrainingSet[] ts; //Eğitim seti veya için

	/*public TrainingSet[] ts = new TrainingSet[] // ve için
	{
		new TrainingSet { input = new double[] { 0, 0 }, output = 0 },
		new TrainingSet { input = new double[] { 0, 1 }, output = 0 },
		new TrainingSet { input = new double[] { 1, 0 }, output = 0 },
		new TrainingSet { input = new double[] { 1, 1 }, output = 1 }
	};*/

public TrainingSet[] ts = new TrainingSet[] //XOR işlemini öğrenmesi
{
	new TrainingSet { input = new double[] { 0, 0 }, output = 0 },
	new TrainingSet { input = new double[] { 0, 1 }, output = 1 },
	new TrainingSet { input = new double[] { 1, 0 }, output = 1 },
	new TrainingSet { input = new double[] { 1, 1 }, output = 0 }
};

double[] weights = { 0, 0 };
double bias = 0;
double totalError = 0;

double DotProductBias(double[] v1, double[] v2)
{
	if (v1 == null || v2 == null)
		return -1;

	if (v1.Length != v2.Length)
		return -1;

	double d = 0;
	for (int x = 0; x < v1.Length; x++)
	{
		d += v1[x] * v2[x];
	}

	d += bias;

	return d;
}

double CalcOutput(int i)
{
	return (ActivationFunction(DotProductBias(weights, ts[i].input)));
}

double CalcOutput(double i1, double i2)
{
	double[] inp = new double[] { i1, i2 };
	return (ActivationFunction(DotProductBias(weights, inp)));
}

double ActivationFunction(double dp)
{
	if (dp > 0) return (1);
	return (0);
}

void InitialiseWeights()
{
	for (int i = 0; i < weights.Length; i++)
	{
		weights[i] = Random.Range(-1.0f, 1.0f);
	}
	bias = Random.Range(-1.0f, 1.0f);
}

void UpdateWeights(int j)
{
	double error = ts[j].output - CalcOutput(j);
	totalError += Mathf.Abs((float)error);
	for (int i = 0; i < weights.Length; i++)
	{
		weights[i] = weights[i] + error * ts[j].input[i];
	}
	bias += error;
}

void Train(int epochs)
{
	InitialiseWeights();

	for (int e = 0; e < epochs; e++)
	{
		totalError = 0;
		for (int t = 0; t < ts.Length; t++)
		{
			UpdateWeights(t);
			Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
		}
		Debug.Log("TOTAL ERROR: " + totalError);
	}
}


void Start()
{
	Train(1000);
	Debug.Log("Test 0 0: " + CalcOutput(0, 0));
	Debug.Log("Test 0 1: " + CalcOutput(0, 1));
	Debug.Log("Test 1 0: " + CalcOutput(1, 0));
	Debug.Log("Test 1 1: " + CalcOutput(1, 1));
}

void Update()
{

}
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
	public double[] input;
	public double output;
}

public class Perceptron : MonoBehaviour
{

	List<TrainingSet> ts = new List<TrainingSet>();
	double[] weights = { 0, 0 };
	double bias = 0;
	double totalError = 0;

	public GameObject npc;

	public void SendInput(double i1, double i2, double o)
	{
		//react
		double result = CalcOutput(i1, i2);
		Debug.Log(result);
		if (result == 0) //duck for cover
		{
			npc.GetComponent<Animator>().SetTrigger("Crouch");
			npc.GetComponent<Rigidbody>().isKinematic = false;
		}
		else
		{
			npc.GetComponent<Rigidbody>().isKinematic = true;
		}

		//learn from it for next time
		TrainingSet s = new TrainingSet();
		s.input = new double[2] { i1, i2 };
		s.output = o;
		ts.Add(s);
		Train();
	}

	double DotProductBias(double[] v1, double[] v2)
	{
		if (v1 == null || v2 == null)
			return -1;

		if (v1.Length != v2.Length)
			return -1;

		double d = 0;
		for (int x = 0; x < v1.Length; x++)
		{
			d += v1[x] * v2[x];
		}

		d += bias;

		return d;
	}

	double CalcOutput(int i)
	{
		return (ActivationFunction(DotProductBias(weights, ts[i].input)));
	}

	double CalcOutput(double i1, double i2)
	{
		double[] inp = new double[] { i1, i2 };
		return (ActivationFunction(DotProductBias(weights, inp)));
	}

	double ActivationFunction(double dp)
	{
		if (dp > 0) return (1);
		return (0);
	}

	void InitialiseWeights()
	{
		for (int i = 0; i < weights.Length; i++)
		{
			weights[i] = Random.Range(-1.0f, 1.0f);
		}
		bias = Random.Range(-1.0f, 1.0f);
	}

	void UpdateWeights(int j)
	{
		double error = ts[j].output - CalcOutput(j);
		totalError += Mathf.Abs((float)error);
		for (int i = 0; i < weights.Length; i++)
		{
			weights[i] = weights[i] + error * ts[j].input[i];
		}
		bias += error;
	}

	void Train()
	{
		for (int t = 0; t < ts.Count; t++)
		{
			UpdateWeights(t);
		}
	}


	void Start()
	{
		InitialiseWeights();
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			InitialiseWeights();
			ts.Clear();
		}
	}
}*/