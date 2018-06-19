using Android.Content;
using Android.Graphics;
using Android.OS;

namespace CustomView
{
    class Ball
    {
        private Paint red;
        private float x, y, radius, speedX, speedY;
        private bool couldCollide;
        private BlockManager bm;

        public Ball()
        {
            red = new Paint();
            red.SetARGB(255, 255, 0, 0);

            x = GameView.screenW / 2;
            y = GameView.screenH / 2;
            radius = GameView.screenW * 0.04f;
            speedX = 6;
            speedY = -6f;
            couldCollide = true;

            bm = BlockManager.getInstance();
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawCircle(x, y, radius, red);
        }

        public void Update(Player player)
        {
            x += speedX;
            y += speedY;

            CollisionWithScreen();
            CollisionWithBlocks();
            CollisionWithPlayer(player);
        }

        private void CollisionWithScreen()
        {
            if (x + radius > GameView.screenW || x - radius < 0) ChangeBallState(true);
            else if (y - radius < 0) ChangeBallState(false);
            else if (y + radius > GameView.screenH) GameView.isDead = true;
            else couldCollide = true;
        }

        private void ChangeBallState(bool width)
        {
            if (couldCollide)
            {
                if (width) speedX *= -1;
                else speedY *= -1;

                couldCollide = false;
            }
        }

        private void CollisionWithBlocks()
        {
            foreach(Block b in bm.blocks)
            {
                if (!b.Removed &&
                    x - radius < b.GetX() + b.GetW() && x + radius > b.GetX() &&
                    y - radius < b.GetY() + b.GetH() && y + radius > b.GetY())
                {
                    speedY *= -1;
                    b.Removed = true;
                    //bm.SetNumberOfBlocksRemoved(); isso deixa comentado
                    Score.score += 10;

                    if (Score.score == 20)
                        GameView.isSendingLetter = true;

                    break;
                }
            }
        }

        private void CollisionWithPlayer(Player player)
        {
            if (x - radius < player.GetX() + player.GetW() && x + radius > player.GetX() &&
                y - radius < player.GetY() + player.GetH() && y + radius > player.GetY())
            {
                speedY *= -1;
                couldCollide = false;
            }
        }
    }
}