using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using MapLibrary;
using System.Runtime.InteropServices;
using System.IO;
using System.Media;

namespace Raycasting
{
    class Game : GameWindow
    {
        Player player;
        Map map;
        SoundPlayer coinPlayer;

        List<Vector2> verticesGroundPlane;

        public bool UseWallTextures = false;
        private int WallTexture;
        private Shader shader;

        //Fields for Sprites
        List<float> ZBuffer = new List<float>();
        DebugProc debugProc;

        public Game(Player player, Map map) : base(500, 300, GraphicsMode.Default, "Raycasting", GameWindowFlags.Fullscreen)
        {
            this.map = map;
            this.player = player;
            this.CursorVisible = false;
            this.CursorGrabbed = true;
            coinPlayer = new SoundPlayer(Directory.GetCurrentDirectory() + @"/Sound/coin.wav");

            GL.Enable(EnableCap.Texture2D);
            //GL.Enable(EnableCap.DebugOutput);  //Hier kann GLES Shader Debug aktiviert werden
            debugProc = DebugCallback;
            GL.DebugMessageCallback(debugProc, new IntPtr());
        }

        public void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        {
            Console.WriteLine(Marshal.PtrToStringAnsi(message));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GameTextureManager.clearAllVAOs();

            Point lastHit = new Point(-100,0);
            float lastYScaled = 0;
            int lastSide = 0;
            float lastWallX = 1f;
            ZBuffer.Clear();

            #region Raycasting
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

                float wallX; //where exactly the wall was hit
                if (side == 0) 
                    wallX = player.position.Y + perpWallDist * rayDir.Y;
                else 
                    wallX = player.position.X + perpWallDist * rayDir.X;
                wallX -= (float)Math.Floor(wallX);
                float drawXScaled = (float)x / this.Width * 2 - 1f;
                float drawYScaled = (float)lineHeight / this.Height / 2;

                //FIRST HIT
                if (lastHit.X == -100)
                {
                    GameTextureManager.getTextureByGameTexture(map.worldMap[currentMapPosition.X][currentMapPosition.Y]).addOpenVertice(drawXScaled, drawYScaled, wallX);
                    if (side == 0)
                    {
                        GameTextureManager.textureDictionary[GameTexture.Shadow].addOpenVertice(drawXScaled, drawYScaled, wallX);
                    }

                    lastHit = new Point(currentMapPosition.X, currentMapPosition.Y);
                    lastYScaled = drawYScaled;
                    lastSide = side;
                    lastWallX = wallX;
                    continue;
                }
                

                //LAST HIT
                if (x == this.Width - 1)
                {
                    GameTextureManager.getTextureByGameTexture(map.worldMap[currentMapPosition.X][currentMapPosition.Y]).addCloseVertice(drawXScaled, drawYScaled, wallX);
                    if (side == 0)
                    {
                        GameTextureManager.textureDictionary[GameTexture.Shadow].addCloseVertice(drawXScaled, drawYScaled, wallX);
                    }
                    continue;
                }

                //STILL ON SAME BLOCK
                if (currentMapPosition.Equals(lastHit) && lastSide == side)
                {
                    lastYScaled = drawYScaled;
                    lastWallX = wallX;
                    continue;
                }
                //NEW BLOCK
                GameTextureManager.getTextureByGameTexture(map.worldMap[lastHit.X][lastHit.Y]).addCloseVertice(drawXScaled, lastYScaled, lastWallX);
                GameTextureManager.getTextureByGameTexture(map.worldMap[currentMapPosition.X][currentMapPosition.Y]).addOpenVertice(drawXScaled, drawYScaled, wallX);

                if (lastSide == 0)
                {
                    GameTextureManager.textureDictionary[GameTexture.Shadow].addCloseVertice(drawXScaled, lastYScaled, lastWallX);
                }

                if (side == 0)
                {
                   GameTextureManager.textureDictionary[GameTexture.Shadow].addOpenVertice(drawXScaled, drawYScaled, wallX);
                }

                lastHit = new Point(currentMapPosition.X, currentMapPosition.Y);
                lastYScaled = drawYScaled;
                lastSide = side;
            }
            #endregion

            #region Sprite-handling
            //Sprite casting
            foreach (Sprite currSpirte in SpriteManager.sprites)
            {
                currSpirte.updateDistanceToPlayer(player.position);
            }
            SpriteManager.sprites.Sort();

            //Check for player collision with sprites
            foreach (Sprite currSprite in SpriteManager.sprites.Where<Sprite>(x => !x.hidden && x.distanceToPlayer < 0.05f))
            {
                switch (currSprite.name)
                {
                    case SpriteName.Coin:
                        currSprite.hidden = true;
                        player.collectedCoins++;
                        coinPlayer.Play();
                        break;
                    case SpriteName.Portal:
                        Console.WriteLine("Congratulations! You managed to escape.");
                        //TODO Show Time taken???
                        Console.WriteLine("Press any key to continue...");
                        this.WindowState = WindowState.Minimized;
                        Console.ReadKey();
                        Exit();
                        break;
                    default:
                        break;
                }
            }

            foreach (Sprite currSprite in SpriteManager.sprites)
            {
                if (currSprite.hidden)
                    continue;
                var spritePosition = currSprite.position - player.position;
                float invDet = 1.0f / (player.plane.X * player.direction.Y - player.direction.X * player.plane.Y); 
                float transformX = invDet * (player.direction.Y * spritePosition.X - player.direction.X * spritePosition.Y);
                float transformY = invDet * (-player.plane.Y * spritePosition.X + player.plane.X * spritePosition.Y);
                int spriteScreenX = (int)((this.Width / 2f) * (1f + transformX / transformY));  
                
                //calculate height of the sprite on screen
                int spriteHeight = Math.Abs((int)(this.Height / (transformY)))/2;
                int drawStartY = -spriteHeight / 2 + this.Height / 2 + (int)(-20/transformY);
                int drawEndY = spriteHeight / 2 + this.Height / 2 + (int)(-20/transformY);

                //calculate width of the sprite
                int spriteWidth = Math.Abs((int)(this.Height / (transformY)))/2;
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
            #endregion

            //Handle game logic
            if (player.collectedCoins == SpriteManager.totalCoins)
            {
                foreach (Sprite currSprite in SpriteManager.sprites.Where<Sprite>(x => x.name == SpriteName.Portal_Inactive))
                {
                    currSprite.name = SpriteName.Portal;
                }
            }


            //speed modifiers
            float moveSpeed = Player.moveSpeed; 
            float rotSpeed = Player.rotSpeed;

            #region Keyboard-input
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
                player.rotate(-rotSpeed);
            }

            if (input.IsKeyDown(Key.A))
            {
                player.rotate(rotSpeed);
            }
            #endregion
            GameTextureManager.fillAllVAOs();

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            verticesGroundPlane = new List<Vector2>();

            GL.ClearColor(0.4f, 0.2f, 0.2f, 1f);

            verticesGroundPlane.Add(new Vector2(-1f, 0));
            verticesGroundPlane.Add(new Vector2(1f, 0));
            verticesGroundPlane.Add(new Vector2(1f, -1f));
            verticesGroundPlane.Add(new Vector2(-1f, -1f));

            var fragShader = UseWallTextures ? "TexturedShader.frag" : "shader.frag";
            shader = new Shader("shader.vert", fragShader);
            //Textures
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            foreach (SpriteName name in (SpriteName[])Enum.GetValues(typeof(SpriteName)))
            {
                SpriteManager.addSpriteTextureID(name, generateTexture(SpriteManager.getSpritePath(name))); 
            }
            SpriteManager.loadSpritesFromMap(map);
            WallTexture = generateTexture(Directory.GetCurrentDirectory() + @"\Textures\greystone.png");

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            foreach (var item in GameTextureManager.textureDictionary)
            {
                GL.DeleteBuffer(item.Value.VBO);
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
            foreach (SpriteName sprite in (SpriteName[])Enum.GetValues(typeof(SpriteName)))
            {
                GL.DeleteTexture(SpriteManager.getSpriteTextureID(sprite));
            }
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

            foreach (var item in GameTextureManager.textureDictionary)
            {
                if (item.Key == GameTexture.Shadow)
                    continue;
                var currTexture = item.Value;
                GL.BindTexture(TextureTarget.Texture2D, WallTexture);
                GL.BindVertexArray(currTexture.VAO);
                GL.VertexAttrib4(1, currTexture.color);
                GL.DrawArrays(PrimitiveType.Quads, 0, currTexture.vertices.Count);
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Enable(EnableCap.Blend);

            var shadowTexture = GameTextureManager.textureDictionary[GameTexture.Shadow];
            GL.BindVertexArray(shadowTexture.VAO);
            GL.VertexAttrib4(1, new Vector4(0f, 0f, 0f, 0.175f));
            GL.DrawArrays(PrimitiveType.Quads, 0, shadowTexture.vertices.Count);
            GL.BindVertexArray(0);

            shader.Remove();

            foreach (Sprite currSprite in SpriteManager.sprites)
            {
                if (currSprite.hidden || !currSprite.visible)
                    continue;
                GL.BindTexture(TextureTarget.Texture2D, SpriteManager.getSpriteTextureID(currSprite.name));
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.Transparent);

                GL.TexCoord2(currSprite.firstTextureX, 0);
			    GL.Vertex2(currSprite.drawStart);

                GL.TexCoord2(currSprite.firstTextureX, 0.99f);
			    GL.Vertex2(currSprite.drawStart.X, currSprite.drawEnd.Y);

                GL.TexCoord2(currSprite.lastTextureX, 0.99f);
			    GL.Vertex2(currSprite.drawEnd);

                GL.TexCoord2(currSprite.lastTextureX, 0);
			    GL.Vertex2(currSprite.drawEnd.X, currSprite.drawStart.Y);
			    
			    GL.End();
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            GL.Disable(EnableCap.Blend);
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        private int generateTexture(string texturePath)
        {
            int newTextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, newTextureID);

            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            Bitmap bmp = new Bitmap(texturePath);
            bmp.MakeTransparent(Color.FromArgb(255, 255, 0, 220));
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D); 

            bmp.UnlockBits(data);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            return newTextureID;
        }
    }
}
