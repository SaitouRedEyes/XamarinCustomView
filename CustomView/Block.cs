
using Android.Graphics;

namespace CustomView
{
    class Block
    {
        private Paint green;
        private float x, y, width, height;
        private bool removed;

        public Block(int i, int j, int columns)
        {
            green = new Paint();
            green.SetARGB(235, 0, 255, 0);

            width = (GameView.screenW / columns) - ((GameView.screenW * 0.02f) + 2 * columns) / columns;
            height = GameView.screenH * 0.05f;
            this.x = (GameView.screenW * 0.02f) + j * width + (j * 2);
            this.y = (GameView.screenH * 0.05f) + i * height + (i * 2);

            removed = false;
        }

        public float GetX() { return x; }
        public float GetY() { return y; }
        public float GetW() { return width; }
        public float GetH() { return height; }

        public bool Removed
        {
            get { return removed; }
            set { removed = value; }
        }

        public void Draw(Canvas canvas)
        {
            if (!removed) canvas.DrawRect(x, y, x + width, y + height, green);
        }
    }
}