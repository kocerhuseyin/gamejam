using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventManScript : MonoBehaviour
{
    public Transform desk; // The target position and rotation you want the camera to move to
    public Transform pano; // The target position and rotation you want the camera to move to
    public float transitionDuration = 1f; // Duration of the transition effect in seconds

    public GameObject cameraObject;
    private GameObject startButton;

    public bool isGameLost;

    public GameObject losePanel;
    public GameObject pausePanel;

    private void Start()
    {
        Time.timeScale = 1f;
        isGameLost = false;

        if (losePanel != null)
        {
            losePanel.SetActive(false);

        }

        cameraObject = GameObject.Find("Main Camera");
        startButton = GameObject.Find("startButton");
        if (startButton != null)
        {
            startButton.SetActive(true);
        }

    }
    void Update()
    {
        if (isGameLost)
        {
            //Time.timeScale = 0f;
            //losePanel.SetActive(true);
            //isGameLost = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object hit has a tag and if the tag is "quit"
                if (hit.collider.gameObject.CompareTag("start"))
                {
                    loadLevel1();
                }
                else if (hit.collider.gameObject.CompareTag("levels"))
                {
                    MoveAndRotateCameraPano();
                }
                else if (hit.collider.gameObject.CompareTag("desk"))
                {
                    MoveAndRotateCameraDesk();
                }
                else if (hit.collider.gameObject.CompareTag("level1"))
                {
                    Debug.Log("load level 1");
                    loadLevel1();
                }
                else if (hit.collider.gameObject.CompareTag("level2"))
                {
                    Debug.Log("load level 1");
                    loadLevel2();
                }
                else if (hit.collider.gameObject.CompareTag("level3"))
                {
                    loadLevel3();
                }
            }
        }
    }
    public void continueGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void MoveAndRotateCameraDesk()
    {
        if (startButton != null)
        {
            startButton.SetActive(false);
        }
        StartCoroutine(MoveAndRotateCoroutine(desk));
    }
    public void MoveAndRotateCameraPano()
    {
        StartCoroutine(MoveAndRotateCoroutine(pano));
    }
    private IEnumerator MoveAndRotateCoroutine(Transform finalDestination)
    {
        // Get the initial position and rotation of the camera
        Vector3 initialPosition = cameraObject.transform.position;
        Quaternion initialRotation = cameraObject.transform.rotation;

        // Get the target position and rotation
        Vector3 targetPosition = finalDestination.position;
        Quaternion targetRotation = finalDestination.rotation;

        // Time elapsed during the transition
        float elapsedTime = 0f;

        // Perform the transition
        while (elapsedTime < transitionDuration)
        {
            // Calculate the interpolation factor (0 to 1) based on the elapsed time and transition duration
            float t = elapsedTime / transitionDuration;

            // Use smoothstep interpolation
            float smoothT = t * t * (3 - 2 * t);

            // Interpolate the position and rotation using Quaternion.Slerp and Vector3.Slerp
            Vector3 lerpedPosition = Vector3.Slerp(initialPosition, targetPosition, smoothT);
            Quaternion lerpedRotation = Quaternion.Slerp(initialRotation, targetRotation, smoothT);

            // Set the camera's position and rotation
            cameraObject.transform.position = lerpedPosition;
            cameraObject.transform.rotation = lerpedRotation;

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the camera reaches the exact target position and rotation
        cameraObject.transform.position = targetPosition;
        cameraObject.transform.rotation = targetRotation;
    }
}
