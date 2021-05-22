using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera gameCamera;
    public Camera ortoCamera;
    public RobotActor player1;
    public RobotActor player2;
    public GameObject player1WinText;
    public GameObject player2WinText;
    public GameObject ortoGraphics;

    public Button resetButton;

    public float cameraLerpSpeed = 0.02f;
    public float winTimeLength = 300;

    private bool _winActive = false;
    private float _winTime;

    private float _killHeight = -2;

    private Vector3 _cameraTargetVector = Vector3.zero;
    private Vector3? _cameraTarget = null;

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
            ResetGame();
        }

        if(Input.GetKeyDown(KeyCode.Home)){
          gameCamera.gameObject.SetActive(!gameCamera.gameObject.activeSelf);
          ortoCamera.gameObject.SetActive(!ortoCamera.gameObject.activeSelf);
          ortoGraphics.SetActive(!ortoGraphics.activeSelf);
        }

        if (!_winActive){
          if(player1.transform.position.y < _killHeight)
            Player2Win();
          
          if(player2.transform.position.y < _killHeight)
            Player1Win();
        }
    }

    private void ResetGame() {
      player1.ResetActor();
      player2.ResetActor();
      resetButton.interactable = true;
      _winTime = 0;
      _winActive = false;
      _cameraTarget = null;
      player1WinText.SetActive(false);
      player2WinText.SetActive(false);
    }

    private void AnyPlayerWin(){
      resetButton.interactable = false;
      _winActive = true;
    }

    private void Player1Win(){
      AnyPlayerWin();
      player1WinText.SetActive(true);
      _cameraTarget = player1.transform.position;
    }
    private void Player2Win(){
      AnyPlayerWin();
      player2WinText.SetActive(true);
      _cameraTarget = player2.transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    { 
        Debug.Log(col.transform.tag);
        if ((col.transform.tag != "Player1" && col.transform.tag != "Player2") || _winActive == true)
            return;

        if (col.transform.tag == "Player1")
        {
            Player2Win();
        }

        if (col.transform.tag == "Player2")
        {
            Player1Win();
        }
    }
}
