using _253504_Novikov_Lab1.Services;
using _253504_Novikov_Lab1.Entities;
using System.Xml;

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

        collectionView.ItemTemplate = new DataTemplate(() =>
        {
            var firstNameLabel = new Label { FontSize = 20};
            firstNameLabel.SetBinding(Label.TextProperty, "FirstName");
            
            var secondNameLabel = new Label { FontSize = 20 };
            secondNameLabel.SetBinding(Label.TextProperty, "LastName");

            var sexLabel = new Label();
            sexLabel.SetBinding(Label.TextProperty, new Binding { Path = "Sex", StringFormat = "Пол: {0}" });

            var ageLabel = new Label();
            ageLabel.SetBinding(Label.TextProperty, new Binding { Path = "Age", StringFormat = "Возраст: {0}" });

            var diagnosisLabel = new Label();
            diagnosisLabel.SetBinding(Label.TextProperty, new Binding { Path = "Diagnosis", StringFormat = "Диагноз: {0}" });

            return new StackLayout
            {
                Children = { firstNameLabel, secondNameLabel, sexLabel, ageLabel, diagnosisLabel },
                Margin = new Thickness(10, 10, 10, 10)
            };
        });

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

        collectionView.ItemsSource = patients;
    }
}