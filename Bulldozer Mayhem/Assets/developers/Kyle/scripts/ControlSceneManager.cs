using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlSceneManager : MonoBehaviour
{
    public TMP_InputField player1NameInputField;  // Input field for Player 1's name
    public TMP_InputField player2NameInputField;  // Input field for Player 2's name
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private PlayerStatistic player1, player2;

    // This function will be called when the "Start Game" button is clicked
    public void StartGame()
    {
        // Get the names from the input fields
        string player1Name = player1NameInputField.text;
        string player2Name = player2NameInputField.text;

        // If the names are empty, set them to default names
        if (string.IsNullOrEmpty(player1Name))
            player1Name = "Player 1";

        if (string.IsNullOrEmpty(player2Name))
            player2Name = "Player 2";

        gameStatus.player1Name = player1Name;
        gameStatus.player2Name = player2Name;
        gameStatus.currentRound = 0;
        gameStatus.player1Wins = 0;
        gameStatus.player2Wins = 0;

        // Load the Main Game Scene
        SceneManager.LoadScene("MainGame"); // Make sure the scene name matches your main game scene
    }
}
