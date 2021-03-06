﻿using UnityEngine;

public class SaveDataManager : Singleton<SaveDataManager> {
    private const string HIGH_SCORE_KEY = "HIGHSCORE";
    private const string SOUND_EFFECTS_KEY = "SOUND_EFFECTS";
    private const string BATTERY_SAVER_KEY = "BATTERY_SAVER";
    private const string TUTORIAL_COMPLETE_KEY = "TUTORIAL_COMPLETE";
    private const string ADS_REMOVED_KEY = "ADS_REMOVED";
    private const string TOTAL_GAMES_KEY = "TOTAL_GAMES";

    #region High Scores

    public void TrySubmitHighScore(int score) {
        if (IsHighScore(score)) {
            SetHighScore(score);
            Debug.Log("New high score submitted.");
        } else {
            Debug.Log("Score is not high score.");
        }
    }

    bool IsHighScore(int score) {
        return score > PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    void SetHighScore(int score) {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    public int GetHighScore() {
        int prefsScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        int gCenterScore = GameCenterManager.Instance.GetHighScore();

        if (prefsScore >= gCenterScore) {
            return prefsScore;
        } else {
            return gCenterScore;
        }
    }

    #endregion

    #region Sound Effects

    public void ToggleSound() {
        int num = PlayerPrefs.GetInt(SOUND_EFFECTS_KEY, -1);

        if (num == 0) // Set to on
        {
            PlayerPrefs.SetInt(SOUND_EFFECTS_KEY, 1);
            PlayerPrefs.Save();
            AudioManager.Instance.SoundEnabled = true;
        } else if (num == 1 || num == -1) // Set to off
          {
            PlayerPrefs.SetInt(SOUND_EFFECTS_KEY, 0);
            PlayerPrefs.Save();
            AudioManager.Instance.SoundEnabled = false;
        }
    }

    public bool IsSoundOn() {
        return PlayerPrefs.GetInt(SOUND_EFFECTS_KEY, -1) == 1;
    }

    #endregion

    #region Battery Saver

    public void ToggleBatterySaver() {
        int num = PlayerPrefs.GetInt(BATTERY_SAVER_KEY, -1);

        if (num == 0 || num == -1) // Set to false
        {
            PlayerPrefs.SetInt(BATTERY_SAVER_KEY, 1);
            PlayerPrefs.Save();
            Application.targetFrameRate = 30;
        } else if (num == 1) // Set to true
          {
            PlayerPrefs.SetInt(BATTERY_SAVER_KEY, 0);
            PlayerPrefs.Save();
            Application.targetFrameRate = 60;
        }
    }

    public bool IsBatterySaverOn() {
        return PlayerPrefs.GetInt(BATTERY_SAVER_KEY, -1) == 1;
    }

    #endregion

    #region Tutorial Completion

    public void OnTutorialComplete() {
        PlayerPrefs.SetInt(TUTORIAL_COMPLETE_KEY, 1);
        PlayerPrefs.Save();
    }

    // used to check if we should load the tutorial on start.
    public bool IsTutorialComplete() {
        return PlayerPrefs.GetInt(TUTORIAL_COMPLETE_KEY, 0) == 1 ? true : false;
    }

    #endregion

    /*
    #region Remove Ads

    public void OnPayToRemoveAds() {
        PlayerPrefs.SetInt(ADS_REMOVED_KEY, 1);
    }

    public bool HasPaidToRemoveAds() {
        return PlayerPrefs.GetInt(ADS_REMOVED_KEY, 0) == 1 ? true : false;
    }

    #endregion

    #region Ad Delay

    public void IncrementTotalGames() {
        int currentTotal = PlayerPrefs.GetInt(TOTAL_GAMES_KEY, 0);
        PlayerPrefs.SetInt(TOTAL_GAMES_KEY, currentTotal + 1);
        PlayerPrefs.Save();
    }

    public int GetTotalGames() {
        return PlayerPrefs.GetInt(TOTAL_GAMES_KEY, 0);
    }

    #endregion
    */
}
