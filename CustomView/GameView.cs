using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Java.Lang;

namespace CustomView
{
    public class GameView : View, IRunnable
    {
        Context context;

        public static int screenW, screenH;
        public static bool isDead, isPaused, isUpdating, isSendingLetter;

        private Handler handler;

        private Paint scorePaint;
        private Paint pausePaint;

        private Ball ball;
        private Player player;
        private BlockManager bm;
        private Score score;
        private int highScore;
        private DataStorage dt;

        public GameView(Context context) : base(context)
        {
            Initialize(context);
        }

        public GameView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public GameView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context c)
        {
            context = c;

            SetBackgroundColor(Color.Black);

            screenW = context.Resources.DisplayMetrics.WidthPixels;
            screenH = context.Resources.DisplayMetrics.HeightPixels;

            isDead = isPaused = isSendingLetter = false;
            isUpdating = true;

            scorePaint = new Paint();
            scorePaint.SetARGB(255, 255, 255, 255);
            scorePaint.TextSize = 25;

            pausePaint = new Paint();
            pausePaint.TextSize = 70;
            pausePaint.Color = Color.White;

            ball = new Ball();
            player = new Player();
            bm = BlockManager.getInstance();
            score = new Score();

            dt = new DataStorage(context);
            highScore = dt.GetHighScore();

            handler = new Handler();
            handler.Post(this);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (!isDead && !isPaused)
                player.PreUpdate(e);
            else if(isDead)
                RestartGame();
            else if(isPaused)
                isPaused = false;

            return true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (!isDead && !isPaused)
            {
                ball.Draw(canvas);
                player.Draw(canvas);
                score.Draw(canvas);

                foreach(Block b in bm.blocks)
                    b.Draw(canvas);

                canvas.DrawText("High score: " + highScore.ToString(), screenW * 0.7f, screenH * 0.03f, scorePaint);
            }
            else
                canvas.DrawText("Touch to restart", screenW * 0.15f, screenH * 0.4f, pausePaint);
        }

        private void Update()
        {
            if (!isDead && !isPaused)
            {
                player.Update();
                ball.Update(player);
            }
            else if (isDead)
                GameOver();

            /*if (isSendingLetter)
                SendLetterToHangman();*/
        }

        private void GameOver()
        {
            if (Score.score > highScore)
            {
                dt.SetHighScore(Score.score);
                highScore = Score.score;
            }
        }

        private void RestartGame()
        {
            ball = new Ball();
            score = new Score();
            bm.SetupBlocks();
            isDead = false;
        }

        /*private void SendLetterToHangman()
        {
            isSendingLetter = false;
            Intent i = new Intent("Game");
            i.AddCategory("Hangman");

            i.AddFlags(ActivityFlags.ReorderToFront);

            Bundle myParameters = new Bundle();
            myParameters.PutString("Letter", "A");

            i.PutExtras(myParameters);
            context.StartActivity(i);
        }*/

        public void Run()
        {
            if (isUpdating)
            {
                //Add a Runnable in the main thread message queue to be executed after a delayed time (ms).
                handler.PostDelayed(this, 30);

                Update();
                this.Invalidate();
            }
            else
                bm.SetupBlocks();
        }
    }
}