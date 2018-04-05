
using Android.Content;
using Android.Preferences;

namespace CustomView
{
    class DataStorage
    {
        ISharedPreferences prefs;

        public DataStorage(Context c)
        {
            prefs = PreferenceManager.GetDefaultSharedPreferences(c);
        }

        public int GetHighScore()
        {
            int highscore = prefs.GetInt("highscore", 0);
            
            return highscore;
        }

        public void SetHighScore(int score)
        {
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutInt("highscore", score);
            // editor.Commit();    // applies changes synchronously on older APIs
            editor.Apply();        // applies changes asynchronously on newer APIs
        }
    }
}