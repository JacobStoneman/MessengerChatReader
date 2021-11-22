namespace GroupChatAnalyser_Utils
{
	public class PieChartEntry
	{
        private string _name;
        public string Name { get => _name; }

        private int _amount;
        public int Amount { get => _amount; }

		public PieChartEntry(string name, int amount)
		{
            _amount = amount;
            _name = name;
		}
    }
}
