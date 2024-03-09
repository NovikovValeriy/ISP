using _253504_Novikov_Lab1.Services;
using System.Diagnostics;

namespace _253504_Novikov_Lab1;

public partial class CurrencyConverter : ContentPage
{
	private readonly IRateService _rateService;
	private List<string> _currencies = new List<string>() { "BYN", "RUB", "EUR", "USD", "CHF", "CNY", "GBP" };
	private Dictionary<string, double> _ratesdict = new Dictionary<string, double>(){
		{"BYN", 1 },
		{ "RUB", -1 },
		{ "USD", -1 },
		{ "EUR", -1 },
		{ "CNY", -1 },
		{ "GBP", -1 },
		{ "CHF", -1 }
    };
	public CurrencyConverter(IRateService service)
	{
		InitializeComponent();
		
		_rateService = service;
        
		Loaded += RatesUpdate;
        
		datePicker.MaximumDate = DateTime.Today;
		datePicker.Date = DateTime.Today;
		datePicker.MinimumDate = new DateTime(2004, 1, 1);
        datePicker.PropertyChanged += RatesUpdate;
		
		inputPicker.ItemsSource = _ratesdict.Keys.ToList();
		resultPicker.ItemsSource = _ratesdict.Keys.ToList();

		inputPicker.SelectedItem = inputPicker.ItemsSource[0];
		resultPicker.SelectedItem = resultPicker.ItemsSource[1];
	}

	async void RatesUpdate(object sender, EventArgs e)
	{
		var rates = await _rateService.GetRates(datePicker.Date);
		Parallel.ForEach(rates, rate =>
		{
			Label label;
            label = (Label)FindByName(rate.Cur_Abbreviation + "rate");
			if (label == null) label = RUBrate;
            string key = rate.Cur_Abbreviation;
            double value = (double)(rate.Cur_OfficialRate == null ? -1 : rate.Cur_OfficialRate) / rate.Cur_Scale;
			if (sender != datePicker)
			{
				_ratesdict[key] = value;
            }
            MainThread.BeginInvokeOnMainThread(() =>
			{
                label.Text = value.ToString("F3");
            });
		});
	}

    private void Calculate(object sender, EventArgs e)
    {
		resultLabel.Text = (
			Convert.ToDouble(
				inputEditor.Text == string.Empty ? 0 : inputEditor.Text) 
			* _ratesdict[inputPicker.SelectedItem.ToString()]
			/ _ratesdict[resultPicker.SelectedItem.ToString()]).ToString("F3");
    }
}