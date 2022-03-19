namespace Cards
{
    public enum AttributeType
    {
        Health,
        Attack,
        ManaCost
    }

    public class CardData
    {
        public string Name;
        public string Description;
        public UnityEngine.Sprite Icon;
        public int Health;
        public int Attack;
        public int ManaCost;
    
        public CardData(string name, string description, UnityEngine.Sprite icon, int health, int attack, int manaCost)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Health = health;
            Attack = attack;
            ManaCost = manaCost;
        }
    }
}