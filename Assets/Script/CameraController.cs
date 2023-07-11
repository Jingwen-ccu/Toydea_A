using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera Camera;
    private float ZoomSize = 2;
    private bool isZoom = false;
    private bool isRight = false;
    public float velocity = 0.035f;
    private Vector3 Default_CameraPosition = new Vector3(0, 0, -10);
    private float Default_CameraSize = 12;
    private Vector3 Final_CameraPosition;
    public GameObject Prisoner;
    public GameObject Executor;
    private void LateUpdate() {
        if(!isZoom) return;
        if(isRight) {
            Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, ZoomSize, velocity);
            Final_CameraPosition = new Vector3(Prisoner.transform.position.x - 1, Prisoner.transform.position.y, Camera.transform.position.z);
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, Final_CameraPosition, velocity);
        } else {
            Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, ZoomSize, velocity);
            Final_CameraPosition = new Vector3(Executor.transform.position.x + 1, Executor.transform.position.y, Camera.transform.position.z);
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, Final_CameraPosition, velocity);
        }
    }
    public void ZoomCamera(bool isRight) {
        isZoom = true;
        if(isRight) {
            this.isRight = true;
        } else {
            this.isRight = false;
        }
    }
    public void InitCamera() {
        Camera.transform.position = Default_CameraPosition;
        Camera.orthographicSize = Default_CameraSize;
        isZoom = false;
    }
}
