using System.Collections.Generic;
using Combat;

namespace UI.UI_Scripts.Notebook
{
    public class NotebookEntry
    {
        public List<Attack> seenAttacks;
        public List<Element> attemptedElements;
        public bool unlockedBasicDescription;
        public bool unlockedDetailedDescription;
        
        public NotebookEntry()
        {
            this.seenAttacks = new List<Attack>();
            this.attemptedElements = new List<Element>();
            this.unlockedBasicDescription = false;
            this.unlockedDetailedDescription = false;
        }
        
        public void AddAttack(Attack attack)
        {
            if (!seenAttacks.Contains(attack))
            {
                seenAttacks.Add(attack);
            }
        }
        
        public void AddElement(Element element)
        {
            if (!attemptedElements.Contains(element))
            {
                attemptedElements.Add(element);
            }
        }
        
        public void UnlockBasicDescription()
        {
            unlockedBasicDescription = true;
        }
        
        public void UnlockDetailedDescription()
        {
            unlockedDetailedDescription = true;
        }
    }
}