using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour

{
    private GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        Button = GameObject.Find("ConfirmButton");
        // Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);

    }

    public void OnClick()
    {
        // Call the function in ouchi.cs to confirm the pattern
        Debug.LogError("Confirm Button Clicked");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
