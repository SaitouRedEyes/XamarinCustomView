using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace CustomView
{
    [Activity(Label = "Game")]
    [IntentFilter(new[] { "Game"}, Categories = new[] { Intent.CategoryDefault, "Arkanoid"} )]
    public class Game : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

            GetIntentMessage();

            SetContentView(new GameView(this));
        }

        private void GetIntentMessage()
        {
            Intent i = Intent;

            if (i != null)
            {
                Bundle myParameters = i.Extras;

                if (myParameters != null) Toast.MakeText(this, 
                    myParameters.GetString(Intent.ExtraText), 
                    ToastLength.Short).Show();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            GameView.isPaused = true;
            Toast.MakeText(this, "PAUSE!!!!!", ToastLength.Short).Show();
        }

        protected override void OnStop()
        {
            base.OnStop();
            GameView.isPaused = true;
            Toast.MakeText(this, "STOP!!!!!", ToastLength.Short).Show();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameView.isUpdating = false;
            Toast.MakeText(this, "DESTROY!!!!!", ToastLength.Short).Show();
        }
    }
}