using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject deathCamera;

    public void SetCamera()
    {
        bool isGameRunning = GameManager.Instance.IsGameRunning();
        deathCamera.SetActive(!isGameRunning);
        thirdPersonCamera.SetActive(isGameRunning);
    }

    private void Update()
    {
        SetCamera();
    }
}
