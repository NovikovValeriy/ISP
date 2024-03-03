using _253504_Novikov_Lab1.Services;

namespace _253504_Novikov_Lab1;

public partial class SQLiteDemo : ContentPage
{
	private readonly IDbService _db;
	public SQLiteDemo(IDbService db)
	{
		InitializeComponent();
		_db = db;
	}

	private void WardIdChanged(object sender, EventArgs e)
	{

	}
}