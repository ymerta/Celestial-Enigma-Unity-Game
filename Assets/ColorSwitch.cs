using UnityEngine;
using System.Collections.Generic;

public class ColorSwitch : MonoBehaviour
{
    public enum ColorState { Red, Green, Blue }
    public ColorState currentColor = ColorState.Red;

    private List<GameObject> redPlatforms = new List<GameObject>();
    private List<GameObject> greenPlatforms = new List<GameObject>();
    private List<GameObject> bluePlatforms = new List<GameObject>();

    void Start()
    {
        // Sadece bir kez bulunur
        redPlatforms.AddRange(GameObject.FindGameObjectsWithTag("RedPlatform"));
        greenPlatforms.AddRange(GameObject.FindGameObjectsWithTag("GreenPlatform"));
        bluePlatforms.AddRange(GameObject.FindGameObjectsWithTag("BluePlatform"));

        UpdatePlatforms();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentColor = ColorState.Red;
            UpdatePlatforms();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentColor = ColorState.Green;
            UpdatePlatforms();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentColor = ColorState.Blue;
            UpdatePlatforms();
        }
    }

    void UpdatePlatforms()
    {
        foreach (GameObject obj in redPlatforms)
            obj.SetActive(currentColor == ColorState.Red);

        foreach (GameObject obj in greenPlatforms)
            obj.SetActive(currentColor == ColorState.Green);

        foreach (GameObject obj in bluePlatforms)
            obj.SetActive(currentColor == ColorState.Blue);
    }
}
