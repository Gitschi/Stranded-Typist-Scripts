using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPosition : MonoBehaviour {

    private float smoothingSpeed;
    private float clampOffsetHor;
    private float clampOffsetVert;

    public GameObject textObj;
    public WordManager wordManager;

    private void Start()
    {
        smoothingSpeed = 1f;
        clampOffsetHor = 60f;
        clampOffsetVert = 20f;
    }

    void Awake () {
        wordManager = FindObjectOfType<WordManager>();
	}

    private void Update()
    {
        SetLabelPos();
    }

    //  Sets text object
    public void SetTextObject(GameObject _textObj)
    {
        textObj = _textObj;
    }

    // Spawns tag
    public void SpawnTag()
    {
        wordManager.AddWord(gameObject);
    }

    // Sets position of label based on enemy movement
    private void SetLabelPos()
    {
        if (textObj != null)
        {
            Vector3 newPos = Vector3.Slerp(textObj.transform.position, Camera.main.WorldToScreenPoint(transform.position), Time.deltaTime * smoothingSpeed);
            newPos.x = Mathf.Clamp(newPos.x, clampOffsetHor, Screen.width - clampOffsetHor);
            newPos.y = Mathf.Clamp(newPos.y, clampOffsetVert, Screen.height - clampOffsetVert);

            textObj.transform.position = newPos;
        }
    }
}
