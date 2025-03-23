using UnityEngine;
using TMPro;

public class InputTextManager : MonoBehaviour
{
    public TMP_InputField[] inputFields;  // Array om de inputvelden in op te slaan
    public TMP_Text[] txtFields;          // Array om de tekstvelden in op te slaan
    public ValueManager valueManager;

    private void Update()
    {

    }

    public void HideInputField(int index)
    {
        if (index >= 0 && index < inputFields.Length)
        {
            inputFields[index].gameObject.SetActive(false);  // Verberg het inputveld op de opgegeven index
        }
    }

    public void HideTextField(int index)
    {
        if (index >= 0 && index < txtFields.Length)
        {
            txtFields[index].gameObject.SetActive(false);  // Verberg het tekstveld op de opgegeven index
        }
    }

    // Toon alle inputvelden
    public void EnableAllInputFields()
    {
        foreach (var inputField in inputFields)
        {
            inputField.gameObject.SetActive(true);  // Activeer alle inputvelden
        }
    }

    // Toon alle tekstvelden
    public void EnableAllTextFields()
    {
        foreach (var txtField in txtFields)
        {
            txtField.gameObject.SetActive(true);  // Activeer alle tekstvelden
        }
    }
}
