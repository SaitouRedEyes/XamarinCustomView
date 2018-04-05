using Android.App;
using Android.OS;

namespace CustomView
{
    [Activity(Label = "CustomView", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            
            SetContentView(new StartScreenView(this));
        }
    }
}

