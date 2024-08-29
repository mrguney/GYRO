using UnityEngine;

public class WeightPredictionChart : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private int currentWeight;
    private int targetWeight;
    private int weeks;
    private float[] weightPredictions;

    void Start()
    {
        currentWeight = PlayerPrefs.GetInt("CurrentWeight", 80);
        targetWeight = PlayerPrefs.GetInt("TargetWeight", 70);
        weeks = PlayerPrefs.GetInt("Weeks", 6);

        CalculateWeightPredictions();

        if (lineRenderer == null)
        {
            Debug.LogError("Line Renderer is not assigned!");
            return;
        }

        DrawChart();
    }

    void CalculateWeightPredictions()
    {
        weightPredictions = new float[weeks];
        float weightLossPerWeek = (float)(currentWeight - targetWeight) / weeks;

        for (int i = 0; i < weeks; i++)
        {
            weightPredictions[i] = currentWeight - (weightLossPerWeek * (i + 1));
            Debug.Log($"Week {i + 1}: {weightPredictions[i]} kg");
        }
    }

    void DrawChart()
    {
        lineRenderer.positionCount = weightPredictions.Length;

        float xSpacing = 10f; // X ekseninde daha fazla boşluk yaratmak için kullanılacak
        float yScale = 10f; // Y ekseninde daha görünür hale getirmek için ölçekleme

        for (int i = 0; i < weightPredictions.Length; i++)
        {
            float xPosition = i * xSpacing;
            float yPosition = (weightPredictions[i] - targetWeight) * yScale;

            lineRenderer.SetPosition(i, new UnityEngine.Vector3(xPosition, yPosition, 0));
        }
    }
}