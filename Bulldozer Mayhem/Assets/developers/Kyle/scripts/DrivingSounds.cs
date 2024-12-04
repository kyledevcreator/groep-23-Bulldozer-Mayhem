using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingSounds : MonoBehaviour
{

    public AudioSource TrackSound;
    private float volume;
    private float maxVolume = 0.071f;
    private float transitionSpeed = 2;
    private MovementPlayerOne movementPlayer;
    private void Awake()
    {
        movementPlayer = GetComponent<MovementPlayerOne>();

    }

    private void Update()
    {

        bool isMovingP1 = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isMovingP2 = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);


        float targetVolume = isMovingP1 ? maxVolume : 0;
        if (movementPlayer.player == MovementPlayerOne.PlayerEnum.Player2)
            targetVolume = isMovingP2 ? maxVolume : 0;

        volume = Mathf.Lerp(volume, targetVolume, Time.deltaTime * transitionSpeed);
        TrackSound.volume = volume;
    }
}
