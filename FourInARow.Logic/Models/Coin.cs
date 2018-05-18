namespace FourInARow.Logic.Models
{
    public class Coin
    {
        public Owner Owner { get; }

        public Coin(Owner owner)
        {
            Owner = owner;
        }

        public override string ToString()
        {
            return Owner == Owner.PlayerOne ? "1" : "2";
        }
    }

    public enum Owner
    {
        PlayerOne,
        PlayerTwo
    }
}
