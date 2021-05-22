using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Camera gameCamera;
    public Camera ortoCamera;
    public RobotActor player1;
    public RobotActor player2;
    public GameObject player1WinText;
    public GameObject player2WinText;
    public GameObject ortoGraphics;

    public float cameraLerpSpeed = 0.02f;
    public float winTimeLength = 300;

    private bool _winActive = false;
    private float _winTime;

    private Vector3 _cameraTargetVector = Vector3.zero;
    private Vector3? _cameraTarget = null;
    void Start()
    {

    }

    void Update()
    {
        Vector3 target;
        if(_cameraTarget == null){
          target = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        } else {
          target = _cameraTarget.Value;
        }

        _cameraTargetVector = Vector3.Lerp(_cameraTargetVector, target, cameraLerpSpeed); 
        gameCamera.transform.LookAt(_cameraTargetVector);

        if (_winActive)
        {
            _winTime += Time.deltaTime;
        }

        if (_winTime > winTimeLength)
        {
            _winTime = 0;
            _winActive = false;
            _cameraTarget = null;
            player1.ResetActor();
            player2.ResetActor();
            player1WinText.SetActive(false);
            player2WinText.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Home)){
          gameCamera.gameObject.SetActive(!gameCamera.gameObject.activeSelf);
          ortoCamera.gameObject.SetActive(!ortoCamera.gameObject.activeSelf);
          ortoGraphics.SetActive(!ortoGraphics.activeSelf);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    { 
        Debug.Log(col.transform.tag);
        if (col.transform.tag != "Player1" && col.transform.tag != "Player2")
            return;

        _winActive = true;
        if (col.transform.tag == "Player1")
        {
            player2WinText.SetActive(true);
            _cameraTarget = player2.transform.position;
        }

        if (col.transform.tag == "Player2")
        {
            player1WinText.SetActive(true);
            _cameraTarget = player1.transform.position;
        }
    }
}
