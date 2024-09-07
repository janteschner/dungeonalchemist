namespace Combat
{
    public class DamageNumberWithInfo
    {
        public int damage;
        public Element element;
        public bool isWeak;
        public bool isResistant;
        public bool isImmune;
        
        public DamageNumberWithInfo(int damage, Element element, bool isWeak, bool isResistant, bool isImmune)
        {
            this.damage = damage;
            this.element = element;
            this.isWeak = isWeak;
            this.isResistant = isResistant;
            this.isImmune = isImmune;
        }
    }
}