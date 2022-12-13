using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    //Camera
    public float speed = 2f;
    public GameObject cameraMain;
    public GameObject targetMain;
    private Vector3 cameraPosition;
    private Vector3 targetPosition;
    private float maxX = 24f;
    private float maxY = 26f;

    //UI
    /*public GameObject Layer2;
    public GameObject Layer5;
    public TextMeshProUGUI Count;
    public TextMeshProUGUI Sum;*/
    // Start is called before the first frame update
    void Start()
    {
        //Sum.text = (Layer2.GetComponent<Spawn_Chest>().maxChestNum + Layer5.GetComponent<Spawn_Chest>().maxChestNum).ToString("/0");
    }

    // Update is called once per frame
    void Update()
    {
        //Camera
        float targetX = targetMain.transform.position.x;
        float targetY = targetMain.transform.position.y;
        if(targetX < 0) { targetX = 0f; }
        else if(targetX > maxX) { targetX = maxX; }
        if(targetY < 0) { targetY = 0f; }
        else if(targetY > maxY) { targetY = maxY; }
        targetPosition = new Vector3(targetX, targetY, 0f);

        cameraPosition = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, -10f);
        cameraMain.transform.position = Vector3.Lerp(cameraPosition, targetPosition, speed * Time.deltaTime);

        //UI
        //Count.text = (Layer2.GetComponent<Spawn_Chest>().currentChestNum + Layer5.GetComponent<Spawn_Chest>().currentChestNum).ToString();
    }
}
