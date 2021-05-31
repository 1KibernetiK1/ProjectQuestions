using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ProjectQuestions.Abstract
{
    public class ManagerBase<T> : GameObjectBase
    where T : GameObjectBase
    {
        protected List<T> subjects;
        protected List<T> subjectsToRemove;

        public ManagerBase()
        {
            subjects = new List<T>();
            subjectsToRemove = new List<T>();
        }

        public virtual void Add(T subject)
        {
            subjects.Add(subject);
            subject.Finish += SubjectFinish;
        }

        private void SubjectFinish(GameObjectBase obj)
        {
            subjectsToRemove.Add((T)obj);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var b in subjects)
            {
                b.Update(gameTime);
            }
            // удаляем отработанные
            foreach (var b in subjectsToRemove)
            {
                subjects.Remove(b);
            }
            subjectsToRemove.Clear();
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (var b in subjects)
            {
                b.Draw(batch);
            }
        }
    }
}
