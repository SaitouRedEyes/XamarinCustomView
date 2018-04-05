
using Android.Graphics;

namespace CustomView
{
    class Score
    {
        public static int score;
        private Paint white;

        public Score()
        {
            score = 0;

            white = new Paint();
            white.SetARGB(255, 255, 255, 255);
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawText(score.ToString(), GameView.screenW * 0.02f, GameView.screenH * 0.03f, white);
        }

    }
}