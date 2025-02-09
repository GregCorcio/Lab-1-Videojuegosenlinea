using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro; // Librería correcta de TextMeshPro
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameInputField; // Campo de entrada de texto para el nombre de la sala
    [SerializeField] private TMP_Text roomName; // Texto para mostrar el nombre de la sala
    [SerializeField] private TMP_Text ErrorMessage; // Mensaje de error a mostrar si falla la creación de la sala

    void Start()
    {
        Debug.Log("Conectando...");

        // Llamar OpenMenuName("Loading") en Start()
        MenuManager.Instance?.OpenMenuName("Loading");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Conectado al Lobby");

        // Llamar OpenMenuName("Home") en OnJoinedLobby()
        MenuManager.Instance?.OpenMenuName("Home");
    }

    // Método para crear una sala
    public void CreateRoom()
    {
        // Si el campo de texto está vacío, no hace nada
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            Debug.Log("⚠️ El nombre de la sala está vacío. No se puede crear.");
            return;
        }

        // Crear la sala en Photon con el nombre ingresado
        PhotonNetwork.CreateRoom(roomNameInputField.text);

        // Abrir el menú de carga
        MenuManager.Instance?.OpenMenuName("Loading");

        Debug.Log($"Creando sala: {roomNameInputField.text}");
    }

    // Sobreescribimos la función OnJoinedRoom
    public override void OnJoinedRoom()
    {
        // Cambiamos al menú de la sala
        MenuManager.Instance?.OpenMenuName("Room");

        // Mostramos el nombre de la sala
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Sobreescribimos la función OnCreateRoomFailed
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // Mostramos el mensaje de error
        ErrorMessage.text = "Error al crear la sala: " + message;

        // Abrimos el menú de error
        MenuManager.Instance?.OpenMenuName("Error");
    }

    // Función para salir de la sala
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        // Abrimos el menú de carga al salir
        MenuManager.Instance?.OpenMenuName("Loading");
    }

    // Sobreescribimos la función OnLeftRoom
    public override void OnLeftRoom()
    {
        // Abrimos el menú de inicio al dejar la sala
        MenuManager.Instance?.OpenMenuName("Home");
    }
}
