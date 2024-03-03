using _253504_Novikov_Lab1.Services;
using _253504_Novikov_Lab1.Entities;

namespace _253504_Novikov_Lab1;

public partial class SQLiteDemo : ContentPage
{
	private readonly IDbService _db;
	private List<Ward> wards;
	private List<string?> wardNames;
	public SQLiteDemo(IDbService db)
	{
		InitializeComponent();
		WardsPicker.Items.Clear();
		_db = db;
		wards = _db.GetAllWards().ToList();
		wardNames = wards.Select(w => w.Name).ToList();
		WardsPicker.ItemsSource = wardNames;
	}

	private void WardIdChanged(object sender, EventArgs e)
	{
        SearchResultContainer.Children.Clear();
        string? selectedOption = WardsPicker.SelectedItem.ToString();
        Ward? w = wards.Where(x => x.Name == selectedOption).FirstOrDefault();
        List<Patient> patients = (w != null ? _db.GetWardsPatients(w.Id).ToList() : new List<Patient>());
        foreach (Patient patient in patients)
        {
            Label patientLabel = new Label
            {
                Text = patient.FirstName + " " + patient.LastName + ", " + patient.Sex + ", " + patient.Age.ToString() + ", " + patient.Diagnosis,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 10, 10, 10),
            };
            SearchResultContainer.Children.Add(patientLabel);
        }
    }
}