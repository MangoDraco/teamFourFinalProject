using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField, Self] CharacterController controller;
    [SerializeField, Self] Animator animator;
    [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
    SerializeField, Anywhere] InputReader input;
}