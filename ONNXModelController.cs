using UnityEngine;
using TMPro;
using Unity.Barracuda;
using UnityEngine.UI;
using System;

public class ONNXModelController : MonoBehaviour
{
    public NNModel modelSquatsAsset;
    public NNModel modelJumpingJacksAsset;
    public NNModel modelSitUpsAsset;
    public NNModel modelPushUpsAsset;

    public TMP_InputField nameInput;
    public TMP_Text ageValueText;
    public TMP_Text weightValueText;
    public TMP_Text heightValueText;
    public TMP_Text targetWeightValueText;
    public TMP_InputField footStepsInput;
    public TMP_Text resultText;

    public Slider ageSlider;
    public Slider weightSlider;
    public Slider heightSlider;
    public Slider targetWeightSlider;
    public Slider weeksSlider;
    public TMP_Text weeksValueText;

    public Toggle maleToggle;
    public Toggle femaleToggle;

    public Button startButton;

    private IWorker workerSquats;
    private IWorker workerJumpingJacks;
    private IWorker workerSitUps;
    private IWorker workerPushUps;

    void Start()
    {
        var modelSquats = ModelLoader.Load(modelSquatsAsset);
        workerSquats = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, modelSquats);

        var modelJumpingJacks = ModelLoader.Load(modelJumpingJacksAsset);
        workerJumpingJacks = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, modelJumpingJacks);

        var modelSitUps = ModelLoader.Load(modelSitUpsAsset);
        workerSitUps = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, modelSitUps);

        var modelPushUps = ModelLoader.Load(modelPushUpsAsset);
        workerPushUps = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, modelPushUps);

        ageSlider.onValueChanged.AddListener(delegate { UpdateSliderText(ageSlider, ageValueText); });
        weightSlider.onValueChanged.AddListener(delegate { UpdateSliderText(weightSlider, weightValueText); UpdateTargetWeightSlider(); });
        heightSlider.onValueChanged.AddListener(delegate { UpdateSliderText(heightSlider, heightValueText); });
        targetWeightSlider.onValueChanged.AddListener(delegate { UpdateSliderText(targetWeightSlider, targetWeightValueText); });
        weeksSlider.onValueChanged.AddListener(delegate { UpdateSliderText(weeksSlider, weeksValueText); });

        maleToggle.onValueChanged.AddListener(delegate { OnGenderToggleChanged(maleToggle, femaleToggle); });
        femaleToggle.onValueChanged.AddListener(delegate { OnGenderToggleChanged(femaleToggle, maleToggle); });

        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void UpdateSliderText(Slider slider, TMP_Text text)
    {
        text.text = slider.value.ToString("0");
    }

    void UpdateTargetWeightSlider()
    {
        if (targetWeightSlider.value >= weightSlider.value)
        {
            targetWeightSlider.value = weightSlider.value - 1;
        }
        targetWeightSlider.maxValue = weightSlider.value - 1;
        UpdateSliderText(targetWeightSlider, targetWeightValueText);
    }

    void OnGenderToggleChanged(Toggle changedToggle, Toggle otherToggle)
    {
        if (changedToggle.isOn)
        {
            otherToggle.isOn = false;
        }
    }

    public void Predict()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            resultText.text = "Please enter your name.";
            return;
        }

        int age = Mathf.RoundToInt(ageSlider.value);
        int weight = Mathf.RoundToInt(weightSlider.value);
        int height = Mathf.RoundToInt(heightSlider.value);
        int targetWeight = Mathf.RoundToInt(targetWeightSlider.value);
        int footSteps = Mathf.RoundToInt(float.Parse(footStepsInput.text));
        int gender = maleToggle.isOn ? 1 : 0;
        int weeks = Mathf.RoundToInt(weeksSlider.value);
        

        float heightInMeters = height / 100f;
        float bmi = weight / (heightInMeters * heightInMeters);

        if (targetWeight >= weight)
        {
            resultText.text = "Target Weight cannot be greater than Current Weight.";
            return;
        }

        Debug.Log($"Inputs: Age={age}, Weight={weight}, Height={heightInMeters}, Target Weight={targetWeight}, Foot Steps={footSteps}, Gender={gender}, BMI={bmi}, Weeks={weeks}");

        Tensor inputTensor = new Tensor(1, 7, new float[] { age, weight, heightInMeters, targetWeight, footSteps, gender, bmi });

        // Squats
        workerSquats.Execute(inputTensor);
        Tensor outputSquats = workerSquats.PeekOutput();
        int squatsValue = Mathf.RoundToInt(Mathf.Max(0, outputSquats[0]));
        Debug.Log($"Squats Prediction: {squatsValue}");

        // Jumping Jacks
        workerJumpingJacks.Execute(inputTensor);
        Tensor outputJumpingJacks = workerJumpingJacks.PeekOutput();
        int jumpingJacksValue = Mathf.RoundToInt(Mathf.Max(0, outputJumpingJacks[0]));
        Debug.Log($"Jumping Jacks Prediction: {jumpingJacksValue}");

        // Sit-Ups
        workerSitUps.Execute(inputTensor);
        Tensor outputSitUps = workerSitUps.PeekOutput();
        int sitUpsValue = Mathf.RoundToInt(Mathf.Max(0, outputSitUps[0]));
        Debug.Log($"Sit-Ups Prediction: {sitUpsValue}");

        // Push Ups
        workerPushUps.Execute(inputTensor);
        Tensor outputPushUps = workerPushUps.PeekOutput();
        int pushUpsValue = Mathf.RoundToInt(Mathf.Max(0, outputPushUps[0]));
        Debug.Log($"Push-Ups Prediction: {pushUpsValue}");

        resultText.text = $"Squats: {squatsValue}\nJumping Jacks: {jumpingJacksValue}\nSit-Ups: {sitUpsValue}\nPush-Ups: {pushUpsValue}";

        PlayerPrefs.SetString("UserName", nameInput.text);
        PlayerPrefs.SetInt("Age", age);
        PlayerPrefs.SetInt("Weight", weight);
        PlayerPrefs.SetInt("Height", height);
        PlayerPrefs.SetInt("TargetWeight", targetWeight);
        PlayerPrefs.SetInt("FootSteps", footSteps);
        PlayerPrefs.SetInt("Gender", gender);
        PlayerPrefs.SetInt("Weeks", weeks);
        PlayerPrefs.SetInt("TargetSquats", squatsValue);
        PlayerPrefs.SetInt("TargetJumpingJacks", jumpingJacksValue);
        PlayerPrefs.SetInt("TargetSitUps", sitUpsValue);
        PlayerPrefs.SetInt("TargetPushUps", pushUpsValue);
        PlayerPrefs.Save();

        startButton.gameObject.SetActive(true);
    }

    public void OnStartButtonClicked()
    {
        ResetAllScores();
        SceneSwitcher.LoadScene("MainMenu");
    }

    private void ResetAllScores()
    {
        PlayerPrefs.SetInt("Squats", 0);
        PlayerPrefs.SetInt("JumpingJacks", 0);
        PlayerPrefs.SetInt("SitUps", 0);
        PlayerPrefs.SetInt("PushUps", 0);
        PlayerPrefs.SetFloat("CaloriesBurned", 0f); // Kalorileri sıfırlama

        // MiniGame skoru sıfırlama
        PlayerPrefs.SetInt("currentScore", 0);
        PlayerPrefs.SetInt("highScore", 0);

        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        workerSquats.Dispose();
        workerJumpingJacks.Dispose();
        workerSitUps.Dispose();
        workerPushUps.Dispose();
    }
}