namespace BasicDataStructures.DataStructures
{
    public class ItemPresenterViewModel
    {
        public long Id { get; private set; }
        public string DisplayString { get; private set; }

        public ItemPresenterViewModel(long id, string displayString)
        {
            Id = id;
            DisplayString = displayString;
        }

        public override string ToString()
        {
            return DisplayString;
        }
    }
}
