
using Android.Graphics;
using Android.Views;

namespace CustomView
{
    class Player
    {
        private Paint blue;
        private float x, y, width, height, speedX;
        private bool isMoving, isMovingLeft;

        public Player()
        {
            blue = new Paint();
            blue.SetARGB(200, 0, 0, 255);

            width = GameView.screenW * 0.2f;
            height = GameView.screenH * 0.03f;
            x = (GameView.screenW / 2) - (width / 2);
            y = GameView.screenH * 0.8f;

            speedX = 7f;
            isMoving = isMovingLeft = false;
        }

        public float GetX() { return x; }
        public float GetY() { return y; }
        public float GetW() { return width; }
        public float GetH() { return height; }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRect(x, y, x + width, y + height, blue);
        }

        public void Update()
        {
            if (isMoving)
            {
                if (isMovingLeft)
                    x -= speedX;
                else
                    x += speedX;
            }

            CollisionWithScreen();
        }

        private void CollisionWithScreen()
        {
            if (x < 0)
                x += speedX;
            else if (x + width > GameView.screenW)
                x -= speedX;
        }

        public void PreUpdate(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down ||
                e.Action == MotionEventActions.Move)
            {
                isMoving = true;
                isMovingLeft = x > e.RawX; // x = true || false
            }
            else if(e.Action == MotionEventActions.Up)
                isMoving = false;
        }
    }
}