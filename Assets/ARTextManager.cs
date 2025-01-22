using TMPro;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ARTextManager : MonoBehaviour
{
    public TMP_InputField textInputField; 
    public Button submitButton;      
    public Slider scaleSlider;          
    public GameObject textPrefab; 
    public Transform cameraTransform;    
    private float currentScale = 1.0f;   

    private void Start()
    {
        submitButton.onClick.AddListener(OnSubmitText);
        scaleSlider.onValueChanged.AddListener(OnScaleChange);
    }

    private void OnSubmitText()
    {
        string userInput = textInputField.text;
        if (!string.IsNullOrEmpty(userInput))
        {
            CreateTextInAR(userInput);
            textInputField.text = ""; // Clear input field for new text
        }
    }

   private void CreateTextInAR(string text)
{
    // Calculate position directly in front of the camera
    Vector3 position = cameraTransform.position + cameraTransform.forward * 1.0f;

    // Instantiate new TextMeshPro object
    GameObject newTextObject = Instantiate(textPrefab, position, Quaternion.identity);

    // Set the text
    TextMeshPro textMesh = newTextObject.GetComponent<TextMeshPro>();
    textMesh.text = text;

    // Scale the text
    newTextObject.transform.localScale = Vector3.one * currentScale;

    // Rotate to face the camera
    newTextObject.transform.rotation = Quaternion.LookRotation(newTextObject.transform.position - cameraTransform.position);
}


    private void OnScaleChange(float value)
    {
        currentScale = value; // Update current scale for new text
    }
       public void CreateText(string text)
    {
        // Determine the position for the new text (1 meter in front of the camera)
        Vector3 position = cameraTransform.position + cameraTransform.forward * 1.0f;

        // Instantiate the text prefab using Photon
        GameObject textObject = PhotonNetwork.Instantiate(textPrefab.name, position, Quaternion.identity);

        // Set the text and position on the new ARTextSync instance
        ARTextSync textSync = textObject.GetComponent<ARTextSync>();
        textSync.SetText(text);
        textSync.SetPosition(position);
    }
}
