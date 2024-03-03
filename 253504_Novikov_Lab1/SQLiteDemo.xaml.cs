using _253504_Novikov_Lab1.Services;
using _253504_Novikov_Lab1.Entities;

namespace _253504_Novikov_Lab1;

public partial class SQLiteDemo : ContentPage
{
	private readonly IDbService _db;
	private IEnumerable<Ward>? wardsEnumerable;
	private List<Ward> wards;
	private List<string?> wardNames;
	public SQLiteDemo(IDbService db)
	{
		InitializeComponent();
		_db = db;
        Task.Run(async () =>
        {
            wardsEnumerable = await _db.GetAllWards();
            wards = wardsEnumerable.ToList();
            WardsPicker.ItemsSource = wardsEnumerable.Select(w => w.Name).ToList();
        });
	}
    


	private async void WardIdChanged(object sender, EventArgs e)
	{
        SearchResultContainer.Children.Clear();
        string? selectedOption = WardsPicker.SelectedItem.ToString();
        
        Ward? w = wards.Where(x => x.Name == selectedOption).FirstOrDefault();
        IEnumerable<Patient> patients;
        if (w == null)
        {
            patients = new List<Patient>();
        }
        else
        {
            patients = await _db.GetWardsPatients(w.Id);
            patients = patients.ToList();
        }
        
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