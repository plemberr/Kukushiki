using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public static List<int> collectedPuzzlePieces = new List<int>();
    public GameObject[] puzzlePieces;

    public static void AddPuzzlePiece(int pieceNumber)
    {
        if (!collectedPuzzlePieces.Contains(pieceNumber))
        {
            collectedPuzzlePieces.Add(pieceNumber);
        }
    }

    private void OnEnable()
    {
        UpdatePuzzlePieces();
    }

    private void UpdatePuzzlePieces()
    {
        foreach (var piece in puzzlePieces)
        {
            piece.SetActive(false);
        }

        foreach (var pieceNumber in collectedPuzzlePieces)
        {
            if (pieceNumber >= 0 && pieceNumber < puzzlePieces.Length)
            {
                puzzlePieces[pieceNumber].SetActive(true);
            }
        }
    }
}






