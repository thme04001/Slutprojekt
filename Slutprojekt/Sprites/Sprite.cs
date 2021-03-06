using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Slutprojekt.Managers;
using Slutprojekt.Models;

namespace Slutprojekt.Sprites
{
    public class Sprite
    {
        #region Fields
        KeyboardState currentKey;
        KeyboardState lastKey;
        bool AnimationRuning = false;
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        #endregion

        #region Properties

        public Input Input;

        public Vector2 Position
        {
            get { 
                return _position; 
            }
            set{
                _position = value;

                if (_animationManager != null)
                _animationManager.Position = _position;
            }
        }

        public float Speed = 1.5f;

        public Vector2 Velocity;

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);

            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);

            else throw new Exception("This ain't right..!");
        }

        public virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left) && AnimationRuning != true)
                Velocity.X = -Speed;

            else if (Keyboard.GetState().IsKeyDown(Input.Right) && AnimationRuning != true)
                Velocity.X = Speed;
  
            if(Keyboard.GetState().IsKeyDown(Input.Attack ) && lastKey.IsKeyUp(Keys.Enter) && AnimationRuning != true) 
                AnimationRuning = true;
            

            else if(AnimationRuning && !Keyboard.GetState().IsKeyDown(Input.Attack) && lastKey.IsKeyDown(Keys.Enter))
                AnimationRuning = false;
            
        }
        protected virtual void Keystates()
        {
            currentKey = Keyboard.GetState();
            lastKey = currentKey;
        }

        protected virtual void SetAnimations()
        {   
            if (Velocity.X > 0)
                _animationManager.Play(_animations["PlayerRight"]);
        
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["PlayerLeft"]);
        
            else if(AnimationRuning && Keyboard.GetState().IsKeyDown(Keys.Enter) && !lastKey.IsKeyUp(Keys.Enter)){
            _animationManager.Play(_animations["PlayerAttack"]);
            }
        
            else{
                _animationManager.Play(_animations["PlayerIdle"]);
            } 
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            Keystates();

            SetAnimations();

            _animationManager.Update(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }   

        #endregion
    }
}
