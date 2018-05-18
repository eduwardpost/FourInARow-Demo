namespace FourInARow.Logic.Models
{
    public class Coin
    {
        public Owner Owner { get; }

        public Coin(Owner owner)
        {
            Owner = owner;
        }

    }

    public enum Owner
    {
        PlayerOne,
        PlayerTwo
    }
}
