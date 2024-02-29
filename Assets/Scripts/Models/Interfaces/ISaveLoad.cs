namespace Models.Interfaces
{
	public interface ISaveLoad
	{
		public string Save();
		public void Load(string value);
	}
}