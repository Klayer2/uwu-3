namespace ReLost.Inventory.Items
{

    public interface IModifier
    {
        void AddValue(ref int baseValue);
    }
}
