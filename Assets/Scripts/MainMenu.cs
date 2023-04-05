using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private AndroidNoteHandler androidNoteHandler;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDur;

    private int energy;

    private const string EnergyKey = "Enery";
    private const string EnergyRdyKey = "EneryRdy";


    private void  Start() 
    {
        OnApplicationFocus(true);
    }
    private void OnApplicationFocus(bool hasFocus) 
    {
        if(!hasFocus)
        {
            return;
        }

        CancelInvoke();

        int highScore =  PlayerPrefs.GetInt(ScoreSystem.HighScoreKey,0);

        highScoreText.text = $"HighScore: {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if(energy == 0)
        {
            string energyRdyString = PlayerPrefs.GetString(EnergyRdyKey, string.Empty);

            if(energyRdyString == string.Empty)
            {
                return;
            }
            DateTime energyRdy =  DateTime.Parse(energyRdyString);

            if(DateTime.Now > energyRdy)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharge), (energyRdy - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play({energy})";
    }

    private void EnergyRecharge()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play({energy})";
    }
    public void Play()
    {
        if(energy < 1)
        {
            return;
        }
        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);

        if(energy == 0)
        {
            DateTime energyRdy = DateTime.Now.AddMinutes(energyRechargeDur);
            PlayerPrefs.SetString (EnergyRdyKey, energyRdy.ToString());
#if UNITY_ANDROID
            androidNoteHandler.ScheduleNote(energyRdy);
#endif

        }

        SceneManager.LoadScene(1);

    }
}
