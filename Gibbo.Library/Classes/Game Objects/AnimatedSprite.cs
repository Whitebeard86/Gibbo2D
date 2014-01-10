﻿#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Animated Sprite Object
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class AnimatedSprite : GameObject
    {
        #region fields
        [DataMember]
        private bool playAllRows = false;
        [DataMember]
        private bool loop = true;
        [DataMember]
        private bool isPlaying = false;
        [DataMember]
        private int totalFramesPerRow = 0;
        [DataMember]
        private int totalRows = 0;
        [DataMember]
        private int delay = 0;
        [DataMember]
        private int currentRow = 0;
        [DataMember]
        private int currentColumn = 0;
        [DataMember]
        private string imageName = string.Empty;
        [DataMember]
        private BlendModes blendMode = BlendModes.NonPremultiplied;
        [DataMember]
        private bool resetOnStart = true;
        [DataMember]
        private Color color = Color.White;
        [DataMember]
        private int totalMillisecondsPassed = 0;

#if WINDOWS
        [NonSerialized]
#endif
        private Texture2D texture;

#if WINDOWS
        [NonSerialized]
#endif
        private BlendState blendState;

        #endregion

        #region properties

        /// <summary>
        /// Determines if the current row/colum should reset on start
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Reset On Start"), Description("Determines if the current row/colum should reset on start")]
#endif
        public bool ResetOnStart
        {
            get { return resetOnStart; }
            set { resetOnStart = value; }
        }

        /// <summary>
        /// The active texture of the animated sprite
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        ///// <summary>
        ///// Automatically calculates the collision boundries based on the texture size
        ///// </summary>
        //[Category("Sprite Properties")]
        //[DisplayName("Auto Boundries"), Description("Automatically calculates the collision boundries based on the step size")]
        //public bool AutoCollisionModel
        //{
        //    get { return autoCollisionModel; }
        //    set { autoCollisionModel = value; }
        //}

        /// <summary>
        /// The blending mode
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Blend Mode"), Description("The blending mode")]
#endif
        public BlendModes BlendMode
        {
            get { return blendMode; }
            set { blendMode = value; this.LoadState(); }
        }

        /// <summary>
        /// Automatically calculates the collision boundries based on the texture size
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Current Row"), Description("The active row of the current animation")]
#endif
        public int CurrentRow
        {
            get { return currentRow; }
            set { currentRow = value; }
        }

        /// <summary>
        /// Automatically calculates the collision boundries based on the texture size
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Current Column"), Description("The active column of the current animation in the current row")]
#endif
        public int CurrentColumn
        {
            get { return currentColumn; }
            set { currentColumn = value; }
        }

        /// <summary>
        /// The delay of each animation step
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Step Delay"), Description("The delay of each animation step, in milliseconds")]
#endif
        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        /// <summary>
        /// The total frames available in each texture
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Frames Per Row"), Description("The total frames available in each texture")]
#endif
        public int TotalFramesPerRow
        {
            get { return totalFramesPerRow; }
            set { totalFramesPerRow = value; }
        }

        /// <summary>
        /// The total frames available in each texture
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Total Row"), Description("The total rows available in each image")]
#endif
        public int TotalRows
        {
            get { return totalRows; }
            set { totalRows = value; }
        }

        /// <summary>
        /// Determines if the current texture is being animated
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Is Playing"), Description("Determines if the current texture is being animated")]
#endif
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; }
        }

        /// <summary>
        /// Determines if animation should proceed to the next row when the current row is finished
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Play All Rows"), Description("Determines if animation should proceed to the next row when the current row is finished")]
#endif
        public bool PlayAllRows
        {
            get { return playAllRows; }
            set { playAllRows = value; }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [EditorAttribute(typeof(ContentBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Sprite Properties")]
        [DisplayName("Image Path"), Description("The relative path to the image")]
#endif
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        /// <summary>
        /// Determines if the current texture is being animated
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [DisplayName("Loop"), Description("Determines if the animation should repeat after all collumns for the current row are played")]
#endif
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        /// <summary>
        /// The fill color
        /// </summary>
#if WINDOWS
        [Category("Sprite Properties")]
        [EditorAttribute(typeof(XNAColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DisplayName("Color"), Description("The fill color")]
#endif
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        #endregion

        #region constructors

        #endregion

        #region methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (resetOnStart)
            {
                currentRow = 0;
                currentColumn = 0;
            }

            LoadState();
        }

        private void LoadState()
        {
            switch (this.blendMode)
            {
                case BlendModes.NonPremultiplied:
                    this.blendState = BlendState.NonPremultiplied;
                    break;
                case BlendModes.AlphaBlend:
                    this.blendState = BlendState.AlphaBlend;
                    break;
                case BlendModes.Additive:
                    this.blendState = BlendState.Additive;
                    break;
            }
        }

        /// <summary>
        /// Updates this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Is the animation playing?
            if (isPlaying)
            {
                totalMillisecondsPassed += gameTime.ElapsedGameTime.Milliseconds;

                // Time to increment a step?
                if (totalMillisecondsPassed >= delay)
                {
                    totalMillisecondsPassed = 0;

                    CurrentColumn++;

                    if (currentColumn >= totalFramesPerRow)
                    {
                        currentColumn = 0;

                        if (playAllRows)
                        {
                            currentRow++;

                            if (currentRow >= totalRows)
                            {
                                if (!loop)
                                    isPlaying = false;
                                else
                                    currentRow = 0;
                            }
                        }
                        else
                        {
                            if (!loop)
                                isPlaying = false;
                        }
                    }
                    else if (currentColumn < 0)
                    {
                        currentColumn = totalFramesPerRow - 1;

                        if (!loop)
                            isPlaying = false;
                    }

                    CurrentColumn = (int)MathHelper.Clamp(CurrentColumn, 0, totalFramesPerRow);
                }
            }

            //if (autoCollisionModel && texture != null && totalFramesPerRow > 0 && totalRows > 0)
            //{
            //    this.CollisionModel.Width = texture.Width / totalFramesPerRow;
            //    this.CollisionModel.Height = texture.Height / totalRows;
            //}
        }

        /// <summary>
        /// Draws this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        /// <param name="spriteBatch">The spriteBatch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (texture == null && imageName != string.Empty)
            {
                texture = TextureLoader.FromContent(imageName);
            }

            if (texture != null && totalFramesPerRow > 0 && totalRows > 0 && Visible) //  && CollisionModel.CollisionBoundry.Intersects(SceneManager.ActiveCamera.BoundingBox)
            {
                int width = texture.Width / totalFramesPerRow;
                int height = texture.Height / totalRows;

                spriteBatch.Begin(SpriteSortMode.Deferred, this.blendState, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                spriteBatch.Draw(texture, Transform.Position, new Rectangle(currentColumn * width, currentRow * height, width, height), Color, Transform.Rotation, new Vector2(width / 2, height / 2), Transform.Scale, SpriteEffects.None, 1);
                spriteBatch.End();
            }
        }

        #endregion
    }
}
