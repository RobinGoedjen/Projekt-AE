using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using MapLibrary;

namespace Raycasting
{
    class Game : GameWindow
    {
        Player player;
        Map map;

        List<Vector2> verticesRed, verticesGreen, verticesBlue, verticesWhite, verticesShadow, verticesGroundPlane;
        List<Vector4> colors = new List<Vector4>();


        private Shader shader;

        int VBORed, VBOGreen, VBOBlue, VBOWhite, VBOShadow;
        int VAORed, VAOGreen, VAOBlue, VAOWhite, VAOShadow;
        int barrelID; //TODO

        //Fields for Sprites
        List<float> ZBuffer = new List<float>();


        public Game(Player player, Map map) : base(500, 300, GraphicsMode.Default, "Raycasting", GameWindowFlags.Fullscreen)
        {
            this.map = map;
            this.player = player;
            this.CursorVisible = false;
            this.CursorGrabbed = true;
            GL.Enable(EnableCap.Texture2D);
            for (sbyte i = 1; i <= Map.colorCount; i++)
            {
                var currColor = Map.getColorFromTileID(i);
                colors.Add(new Vector4((float)currColor.R / 255, (float)currColor.G / 255, (float)currColor.B / 255, (float)currColor.A / 255));
            }
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            verticesRed.Clear();
            verticesGreen.Clear();
            verticesBlue.Clear();
            verticesWhite.Clear();
            verticesShadow.Clear();

            ZBuffer.Clear();
            for (int x = 0; x < this.Width; x++)
            {
                float cameraX = 2f * x / this.Width - 1f;
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
                    hit = map.worldMap[currentMapPosition.X][currentMapPosition.Y] > 0;
                }
                if (side == 0) 
                    perpWallDist = (currentMapPosition.X - player.position.X + (1 - stepX) / 2) / rayDir.X;
                else 
                    perpWallDist = (currentMapPosition.Y - player.position.Y + (1 - stepY) / 2) / rayDir.Y;
                ZBuffer.Add(perpWallDist);
                int lineHeight = (int)(this.Height / perpWallDist);

                //calculate lowest and highest pixel to fill in current stripe
                int drawStart = -lineHeight / 2 + this.Height / 2;
                if (drawStart < 0) 
                    drawStart = 0;
                int drawEnd = lineHeight / 2 + this.Height / 2;
                if (drawEnd >= this.Height) 
                    drawEnd = this.Height - 1;

                float drawXScaled = (float)x / this.Width * 2 - 1f;
                float drawYScaled = (float)lineHeight / this.Height / 2;
                addVertice(new Vector2(drawXScaled, drawYScaled), map.worldMap[currentMapPosition.X][currentMapPosition.Y]);
                addVertice(new Vector2(drawXScaled, -drawYScaled), map.worldMap[currentMapPosition.X][currentMapPosition.Y]);
                if (side == 0)
                {
                    addVertice(new Vector2(drawXScaled, drawYScaled), 0);
                    addVertice(new Vector2(drawXScaled, -drawYScaled), 0);
                }
            }

            //Sprite casting
            foreach (Sprite currSpirte in Sprite.sprites)
            {
                currSpirte.updateDistanceToPlayer(player.position);
            }
            Sprite.sprites.Sort();

            foreach (Sprite currSprite in Sprite.sprites)
            {
                var spritePosition = currSprite.position - player.position;
                float invDet = 1.0f / (player.plane.X * player.direction.Y - player.direction.X * player.plane.Y); 
                float transformX = invDet * (player.direction.Y * spritePosition.X - player.direction.X * spritePosition.Y);
                float transformY = invDet * (-player.plane.Y * spritePosition.X + player.plane.X * spritePosition.Y);
                int spriteScreenX = (int)((this.Width / 2f) * (1f + transformX / transformY));  
                
                //calculate height of the sprite on screen
                int spriteHeight = Math.Abs((int)(this.Height / (transformY)))/2; //Die Zwei verkleinert die Barrel VLLT Konstane machen TODO
                int drawStartY = -spriteHeight / 2 + this.Height / 2 + (int)(-20/transformY); //AUS DIESEM TERM VLLT AUCH EINE KOSNTANTE MACHEN TODO
                int drawEndY = spriteHeight / 2 + this.Height / 2 + (int)(-20/transformY); //TODO

                //calculate width of the sprite
                int spriteWidth = Math.Abs((int)(this.Height / (transformY)))/2; //Die Zwei verkleinert die Barrel TODO
                int drawStartX = -spriteWidth / 2 + spriteScreenX;
                int drawEndX = spriteWidth / 2 + spriteScreenX;

                int firstVisibleX = -1;
                int lastVisibleX = -1;
                for(int stripe = drawStartX; stripe < drawEndX; stripe++)
                {
                    if(transformY > 0 && stripe > 0 && stripe < this.Width && transformY < ZBuffer[stripe])
                    {
                        if (firstVisibleX == -1)
                        {
                            firstVisibleX = stripe;
                        } else
                        {
                            lastVisibleX = stripe;
                        }
                    }
                }
                currSprite.visible = lastVisibleX != -1 && firstVisibleX != -1;
                if (!currSprite.visible)
                    continue;
                int spriteWidthPixel = drawEndX - drawStartX;
                currSprite.firstTextureX = (float)(firstVisibleX-drawStartX) / spriteWidthPixel;
                currSprite.lastTextureX = (float)(lastVisibleX-drawStartX) / spriteWidthPixel;

                currSprite.drawStart = new Vector2(firstVisibleX, drawStartY);
                currSprite.drawEnd = new Vector2(lastVisibleX, drawEndY);
                currSprite.transformDrawToScrren(this.Width, this.Height);
            }

            //speed modifiers
            float moveSpeed = 0.08f; //TODO: Konstanten machen????
            float rotSpeed = 0.03f;

            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (input.IsKeyDown(Key.W))
            {
                if (map.worldMap[(int)(player.position.X + player.direction.X * moveSpeed)][(int)player.position.Y] == 0)
                    player.position += new Vector2(player.direction.X * moveSpeed, 0f);
                if (map.worldMap[(int)player.position.X][(int)(player.position.Y + player.direction.Y * moveSpeed)] == 0)
                    player.position += new Vector2(0f, player.direction.Y * moveSpeed);
            }

            if (input.IsKeyDown(Key.S))
            {
                if (map.worldMap[(int)(player.position.X - player.direction.X * moveSpeed)][(int)player.position.Y] == 0)
                    player.position -= new Vector2(player.direction.X * moveSpeed, 0f);
                if (map.worldMap[(int)player.position.X][(int)(player.position.Y - player.direction.Y * moveSpeed)] == 0)
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
            fillVAO(VAORed, VBORed, verticesRed);

            fillVAO(VAOGreen, VBOGreen, verticesGreen);

            fillVAO(VAOBlue, VBOBlue, verticesBlue);

            fillVAO(VAOWhite, VBOWhite, verticesWhite);

            fillVAO(VAOShadow, VBOShadow, verticesShadow);

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            verticesRed = new List<Vector2>();
            verticesGreen = new List<Vector2>();
            verticesBlue = new List<Vector2>();
            verticesWhite = new List<Vector2>();
            verticesShadow = new List<Vector2>();
            verticesGroundPlane = new List<Vector2>();

            GL.ClearColor(0.4f, 0.2f, 0.2f, 1f);

            VAORed = GL.GenVertexArray();
            VBORed = GL.GenBuffer();

            VAOGreen = GL.GenVertexArray();
            VBOGreen = GL.GenBuffer();

            VAOBlue = GL.GenVertexArray();
            VBOBlue = GL.GenBuffer();

            VAOWhite = GL.GenVertexArray();
            VBOWhite = GL.GenBuffer();

            VAOShadow = GL.GenVertexArray();
            VBOShadow = GL.GenBuffer();

            verticesGroundPlane.Add(new Vector2(-1f, 0));
            verticesGroundPlane.Add(new Vector2(1f, 0));
            verticesGroundPlane.Add(new Vector2(1f, -1f));
            verticesGroundPlane.Add(new Vector2(-1f, -1f));


            shader = new Shader("shader.vert", "shader.frag");

            //Textures
            Sprite.sprites.Add(new Sprite(new Vector2(6f, 9f), Sprite.SpriteName.barrel));
            Sprite.sprites.Add(new Sprite(new Vector2(6f, 8f), Sprite.SpriteName.barrel));

            //TEST
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            
            barrelID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, barrelID);

            Bitmap bmp = new Bitmap(Sprite.getSprite(Sprite.SpriteName.barrel));
            bmp.MakeTransparent(Color.FromArgb(255,255,0,220));
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            BitmapData data = bmp.LockBits(new Rectangle(0,0,bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb); 

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VBORed);
            GL.DeleteBuffer(VBOGreen);
            GL.DeleteBuffer(VBOBlue);
            GL.DeleteBuffer(VBOWhite);
            shader.Dispose();
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.2f, 0.2f, 0.2f);
            GL.Vertex2(verticesGroundPlane[0]);
            GL.Vertex2(verticesGroundPlane[1]);
            GL.Color3(0.25f, 0.07f, 0.30f);
            GL.Vertex2(verticesGroundPlane[2]);
            GL.Vertex2(verticesGroundPlane[3]);
            GL.End();

            shader.Use();
            
            GL.BindVertexArray(VAORed);
            GL.VertexAttrib4(1, colors[0]);
            GL.DrawArrays(PrimitiveType.Lines, 0, verticesRed.Count);

            GL.BindVertexArray(VAOGreen);
            GL.VertexAttrib4(1, colors[1]);
            GL.DrawArrays(PrimitiveType.Lines, 0, verticesGreen.Count);

            GL.BindVertexArray(VAOBlue);
            GL.VertexAttrib4(1, colors[2]);
            GL.DrawArrays(PrimitiveType.Lines, 0, verticesBlue.Count);

            GL.BindVertexArray(VAOWhite);
            GL.VertexAttrib4(1, colors[3]);
            GL.DrawArrays(PrimitiveType.Lines, 0, verticesWhite.Count);

            GL.Enable(EnableCap.Blend);

            GL.BindVertexArray(VAOShadow);
            GL.VertexAttrib4(1, new Vector4(0.2f, 0.2f, 0.2f, 0.175f));
            GL.DrawArrays(PrimitiveType.Lines, 0, verticesShadow.Count);

            shader.Remove();

            //TEST
            GL.BindTexture(TextureTarget.Texture2D, barrelID);
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            foreach (Sprite currSprite in Sprite.sprites)
            {
                if (!currSprite.visible)
                    continue;
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.Transparent);

                GL.TexCoord2(currSprite.firstTextureX, 0);
			    GL.Vertex2(currSprite.drawStart);

                GL.TexCoord2(currSprite.firstTextureX, 0.8f);
			    GL.Vertex2(currSprite.drawStart.X, currSprite.drawEnd.Y);

                GL.TexCoord2(currSprite.lastTextureX, 0.8f);
			    GL.Vertex2(currSprite.drawEnd);

                GL.TexCoord2(currSprite.lastTextureX, 0);
			    GL.Vertex2(currSprite.drawEnd.X, currSprite.drawStart.Y);
			    
			    GL.End();
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Blend);
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }


        private void addVertice(Vector2 position, sbyte colorID)
        {
            switch (colorID)
            {
                case 0:
                    verticesShadow.Add(position);
                    break;
                case 1: 
                    verticesRed.Add(position);
                    break;
                case 2:
                    verticesGreen.Add(position);
                    break;
                case 3: 
                    verticesBlue.Add(position);
                    break;
                default: 
                    verticesWhite.Add(position);
                    break;
            }
        }

        private void fillVAO(int VAO, int VBO, List<Vector2> vertices)
        {
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector2.SizeInBytes, vertices.ToArray(), BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);

        }
    }
}
