using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Raycasting
{
    class Game : GameWindow
    {
        double time = 0f;
        double oldTime = 0f;
        Player player;
        Map map;

        Vector2[] vertices = new Vector2[1250]; //TODO: hier nochma Zahk schauen

        private Shader shader;

        int VertexBufferObject;
        int VertexArrayObject;


        public Game(Player player, Map map) : base(500, 300, GraphicsMode.Default, "Raycasting")
        {
            this.map = map;
            this.player = player;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.BindVertexArray(0); //TODO: kann vllt weg
            int counter = 0; //TODO: Entfernen!
            for (int x = 0; x < this.Width; x++)
            {
                float cameraX = 2 * x / this.Width - 1;
                Vector2 rayDir = player.direction + player.plane * cameraX;

                Point currentMapPosition = new Point((int)player.position.X, (int)player.position.Y);
                Vector2 sideDistance;

                float deltaDistX = (rayDir.Y == 0) ? 0 : ((rayDir.X == 0) ? 1 : Math.Abs(1 / rayDir.X));
                float deltaDistY = (rayDir.X == 0) ? 0 : ((rayDir.Y == 0) ? 1 : Math.Abs(1 / rayDir.Y));
                float perpWallDist;

                //what direction to step in x or y-direction (either +1 or -1)
                int stepX;
                int stepY;

                bool hit = false;
                int side = 0;


                if (rayDir.X < 0)
                {
                    stepX = -1;
                    sideDistance.X = (player.position.X - currentMapPosition.X) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistance.X = (currentMapPosition.X + 1.0f - player.position.X) * deltaDistX;
                }
                if (rayDir.Y < 0)
                {
                    stepY = -1;
                    sideDistance.Y = (player.position.Y - currentMapPosition.Y) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistance.Y = (currentMapPosition.Y + 1.0f - player.position.Y) * deltaDistY;
                }

                //perform DDA
                while (!hit)
                {
                    if (sideDistance.X < sideDistance.Y)
                    {
                        sideDistance.X += deltaDistX;
                        currentMapPosition.X += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistance.Y += deltaDistY;
                        currentMapPosition.Y += stepY;
                        side = 1;
                    }
                    //Check if ray has hit a wall
                    hit = map.worldMap[currentMapPosition.X, currentMapPosition.Y] > 0;
                }
                if (side == 0) 
                    perpWallDist = (currentMapPosition.X - player.position.X + (1 - stepX) / 2) / rayDir.X;
                else 
                    perpWallDist = (currentMapPosition.Y - player.position.Y + (1 - stepY) / 2) / rayDir.Y;

                int lineHeight = (int)(this.Height / perpWallDist);

                //calculate lowest and highest pixel to fill in current stripe
                int drawStart = -lineHeight / 2 + this.Height / 2;
                if (drawStart < 0) 
                    drawStart = 0;
                int drawEnd = lineHeight / 2 + this.Height / 2;
                if (drawEnd >= this.Height) 
                    drawEnd = this.Height - 1;

                //TODO: Überarbeiten!!!!
                float drawXScaled = (float)x / this.Width * 2 - 1f;
                float drawStartScaled = (float)lineHeight / this.Height / 2;
                float drawEndScaled = (float)drawEnd / this.Height * -1f;
                vertices[counter] = new Vector2(drawXScaled, drawStartScaled);
                counter++;
                vertices[counter] = new Vector2(drawXScaled, -drawStartScaled);
                counter++;


                //TODO: Hier nochma nach Farben schauen und das Array füllen

            }
            //timing for input and FPS counter
            oldTime = time;
            time = e.Time;
            float frameTime = (float)((time - oldTime) / 1000.0f); //frameTime is the time this frame has taken, in seconds
            //TODO: in Player Klasse verlagern
            //speed modifiers
            float moveSpeed = frameTime * 5.0f; //the constant value is in squares/second
            moveSpeed = 0.1f; //TODO: RAUS
            float rotSpeed = frameTime * 3.0f; //the constant value is in radians/second
            rotSpeed = 0.02f; //TODO: RAUS

            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (input.IsKeyDown(Key.W))
            {
                if (map.worldMap[(int)(player.position.X + player.direction.X * moveSpeed), (int)player.position.Y] == 0)
                    player.position += new Vector2(player.direction.X * moveSpeed, 0f);
                if (map.worldMap[(int)player.position.X, (int)(player.position.Y + player.direction.Y * moveSpeed)] == 0)
                    player.position += new Vector2(0f, player.direction.Y * moveSpeed);
            }

            if (input.IsKeyDown(Key.S))
            {
                if (map.worldMap[(int)(player.position.X - player.direction.X * moveSpeed), (int)player.position.Y] == 0)
                    player.position -= new Vector2(player.direction.X * moveSpeed, 0f);
                if (map.worldMap[(int)player.position.X, (int)(player.position.Y - player.direction.Y * moveSpeed)] == 0)
                    player.position -= new Vector2(0f, player.direction.Y * moveSpeed);
            }

            if (input.IsKeyDown(Key.D))
            {
                float newDirX = (float)(player.direction.X * Math.Cos(-rotSpeed) - player.direction.Y * Math.Sin(-rotSpeed));
                float newDirY = (float)(player.direction.X * Math.Sin(-rotSpeed) + player.direction.Y * Math.Cos(-rotSpeed));
                player.direction = new Vector2(newDirX, newDirY);

                float newPlaneX = (float)(player.plane.X * Math.Cos(-rotSpeed) - player.plane.Y * Math.Sin(-rotSpeed));
                float newPlaneY = (float)(player.plane.X * Math.Sin(-rotSpeed) + player.plane.Y * Math.Cos(-rotSpeed));
                player.plane = new Vector2(newPlaneX, newPlaneY);
            }

            if (input.IsKeyDown(Key.A))
            {
                float newDirX = (float)(player.direction.X * Math.Cos(rotSpeed) - player.direction.Y * Math.Sin(rotSpeed));
                float newDirY = (float)(player.direction.X * Math.Sin(rotSpeed) + player.direction.Y * Math.Cos(rotSpeed));
                player.direction = new Vector2(newDirX, newDirY);

                float newPlaneX = (float)(player.plane.X * Math.Cos(rotSpeed) - player.plane.Y * Math.Sin(rotSpeed));
                float newPlaneY = (float)(player.plane.X * Math.Sin(rotSpeed) + player.plane.Y * Math.Cos(rotSpeed));
                player.plane = new Vector2(newPlaneX, newPlaneY);
            }
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vector2.SizeInBytes, vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.8f, 0.3f, 0.3f, 1f);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vector2.SizeInBytes, vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);


            shader = new Shader("shader.vert", "shader.frag");
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            shader.Dispose();
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Length);
            

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

    }
}
