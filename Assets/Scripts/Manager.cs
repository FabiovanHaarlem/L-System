using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private LineRenderer m_LineRenderers;

    private List<Vector3> m_LastGenerationPositions;
    private List<Vector3> m_AllGenerationsPositions;

    private List<string> m_AllUsedStrings;

    private Vector3 m_NextGenerationPosition;

    private int m_Index;
    private int m_CurrentGeneration;
    private string m_NextGenerationString;

    [SerializeField]
    private Text m_ButtonText;
    [SerializeField]
    private GameObject m_InputField;
    [SerializeField]
    private Text m_GenerationText;
    [SerializeField]
    private Text m_CurrentStringText;
    [SerializeField]
    private Text m_StartingString;
    [SerializeField]
    private GameObject m_ShowCurrentStringButton;

    private RectTransform m_TextTransform;


    private void Start()
    {
        m_LineRenderers = GetComponent<LineRenderer>();
        m_LineRenderers.SetPosition(0, transform.position);


        m_LastGenerationPositions = new List<Vector3>();
        m_LastGenerationPositions.Add(transform.position);

        m_AllGenerationsPositions = new List<Vector3>();
        m_AllGenerationsPositions = m_LastGenerationPositions;

        m_AllUsedStrings = new List<string>();

        m_TextTransform = m_CurrentStringText.GetComponent<RectTransform>();

        m_NextGenerationPosition = transform.position;

        m_Index = 0;
        
        m_CurrentGeneration = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetPlayerGeneratedString(string playerGeneratedString)
    {
        m_NextGenerationString = playerGeneratedString;
    }

    public void NextGeneration()
    {
        m_InputField.gameObject.SetActive(false);
        m_StartingString.gameObject.SetActive(true);
        m_GenerationText.gameObject.SetActive(true);
        m_ShowCurrentStringButton.SetActive(true);
        m_GenerationText.text = "Current Generation: " + m_CurrentGeneration;
        StartNextGeneration();
    }

    public void StartNextGeneration()
    {
        if (m_CurrentGeneration == 0)
        {
            m_ButtonText.text = "Next Generation";
            m_StartingString.text = m_NextGenerationString;
        }

        m_CurrentGeneration += 1;
        m_GenerationText.text = "Current Generation: " + m_CurrentGeneration;
        ReadString();
    }

    public void ShowStringForNextGeneration()
    {
        //float cameraSize = Camera.main.orthographicSize;

        //m_TextTransform.localScale = new Vector3(0.2f * (cameraSize * 0.02f), 0.2f * (cameraSize * 0.02f), 1f);

        m_CurrentStringText.gameObject.transform.position = transform.position;
        m_CurrentStringText.text = m_NextGenerationString;

        m_CurrentStringText.gameObject.SetActive(!m_CurrentStringText.gameObject.activeInHierarchy);
    }

    private void ReadString()
    {
        m_NextGenerationString = m_NextGenerationString.ToUpper();
        m_AllUsedStrings.Add(m_NextGenerationString);

        string text = m_NextGenerationString;
        string[] characters = new string[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            characters[i] = System.Convert.ToString(text[i]);
        }

        SetNextGenerationPositions(characters);
        MakeNextGenerationString(characters);
    }

    private void SetNextGenerationPositions(string[] characters)
    {
        Vector3 nextGenerationPosition = new Vector3();

        for (int i = 0; i < m_LastGenerationPositions.Count; i++)
        {
            nextGenerationPosition = m_NextGenerationPosition;

            for (int j = 0; j < characters.Length; j++)
            {
                Vector3 step = CheckCharacter(characters[j]);
                nextGenerationPosition = new Vector3(m_NextGenerationPosition.x + step.x, m_NextGenerationPosition.y + step.y);
                

                m_NextGenerationPosition = nextGenerationPosition;
                DrawLine(nextGenerationPosition);
            }
        }
    }

    private Vector3 CheckCharacter(string character)
    {
        Vector3 steps = new Vector3();

        if (character == "A" || character == "T" || character == "J" || character == "M")
            steps = new Vector3(0f, 1f, 0f);
        else if (character == "B" || character == "F" || character == "K" || character == "N")
            steps = new Vector3(1f, 0f, 0f);
        else if (character == "C" || character == "G" || character == "Y" || character == "P")
            steps = new Vector3(-1f, 0f, 0f);
        else if (character == "D" || character == "H" || character == "L" || character == "O")
            steps = new Vector3(0f, -1f, 0f);
        else if (character == "P" || character == "U" || character == "X")
            steps = new Vector3(-1f, 1f, 0f);
        else if (character == "Q" || character == "V" || character == "I")
            steps = new Vector3(1f, 1f, 0f);
        else if (character == "R" || character == "E" || character == "Z")
            steps = new Vector3(-1f, -1f, 0f);
        else if (character == "S" || character == "W")
            steps = new Vector3(1f, 1f, 0f);
        else
            steps = new Vector3(0f, 1f, 0f);

        return steps;
    }

    private void DrawLine(Vector3 position)
    {
        m_LineRenderers.positionCount += 1;
        m_LineRenderers.SetPosition(m_LineRenderers.positionCount - 1, position);
    }

    private void MakeNextGenerationString(string[] characters)
    {
        string nextGenString = "";

        for (int i = 0; i < characters.Length; i++)
        {
            string nextStringPiece = GetCharactersForNextString(characters[i]);

            nextGenString = nextGenString + nextStringPiece;
        }

        m_NextGenerationString = nextGenString;
    }

    private string GetCharactersForNextString(string character)
    {
        string nextStringPiece = "";

        if (character == "A" || character == "T" || character == "J" || character == "M")
            nextStringPiece = "ABA";
        else if (character == "B" || character == "F" || character == "K" || character == "N")
            nextStringPiece = "BBC";
        else if (character == "C" || character == "G" || character == "Y" || character == "P")
            nextStringPiece = "ACY";
        else if (character == "D" || character == "H" || character == "L" || character == "O")
            nextStringPiece = "DDB";
        else if (character == "P" || character == "U" || character == "X")
            nextStringPiece = "YUX";
        else if (character == "Q" || character == "V" || character == "I")
            nextStringPiece = "GES";
        else if (character == "R" || character == "E" || character == "Z")
            nextStringPiece = "AUN";
        else if (character == "S" || character == "W")
            nextStringPiece = "PMQ";
        else
            nextStringPiece = "ACB";

        return nextStringPiece;
    }
}
