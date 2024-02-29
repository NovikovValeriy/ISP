using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;

namespace _253504_Novikov_Lab1;

public partial class ProgressDemo : ContentPage
{
    private CancellationTokenSource? cancellationTokenSource;
    private CancellationToken cancellationToken;
    public ProgressDemo()
	{
		InitializeComponent();
        cancelButton.IsEnabled = false;
    }

    private async void StartClicked(object sender, EventArgs e)
    {
        double result = 0;

        startButton.IsEnabled = false;
        cancelButton.IsEnabled = true;

        cancellationTokenSource = new CancellationTokenSource();
        cancellationToken = cancellationTokenSource.Token;

        Debug.WriteLine($"------------> Enter calc {Thread.CurrentThread.ManagedThreadId}");
        try
        {
            label.Text = "Вычисление";
            result = await Task.Run(() => CalculateIntegral(cancellationToken), cancellationToken);
            label.Text = result.ToString();
        }
        catch (OperationCanceledException)
        {
            label.Text = "Задание отменено";
        }
        finally
        {
            startButton.IsEnabled = true;
            cancelButton.IsEnabled = false;
            cancellationTokenSource.Dispose();
        }
    }

    private async Task<double> CalculateIntegral(CancellationToken token)
    {
        Debug.WriteLine($"------------> Inside calc {Thread.CurrentThread.ManagedThreadId}");
        double result = 0;
        double step = 0.001;
        for(double i = 0; i < 1;i += step)
        {
            if(token.IsCancellationRequested) token.ThrowIfCancellationRequested();
            result += Math.Sin(i) * step;
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Debug.WriteLine($"------------> Changing progressBar {Thread.CurrentThread.ManagedThreadId}");
                progressBar.Progress = i;
                percentage.Text = Convert.ToInt16(i * 100).ToString() + "%";
            });
            Debug.WriteLine($"------------> Outside changing progressBar {Thread.CurrentThread.ManagedThreadId}");
        }
        return result;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        cancellationTokenSource?.Cancel();
    }
}
