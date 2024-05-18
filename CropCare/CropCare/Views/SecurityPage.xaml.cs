using CropCare.Models;
using CropCare.Models.Controllers;
using PropertyChanged;
using System.ComponentModel;

namespace CropCare.Views;
public partial class SecurityPage : ContentPage, INotifyPropertyChanged
{
    /// <summary>
    /// Gets the farm associated with the security controls.
    /// </summary>
    public Farm Farm { get; private set; }

    /// <summary>
    /// Gets the security controller associated with the page.
    /// </summary>
    public SecurityController SecurityController { get; private set; }

    public bool MotionReading { get; set; }

    public double LoudnessReading { get; set; }
    public string LoudnessUnit { get; set; }

    public bool VibrationReading { get; set; }

    public double LuminosityReading { get; set; }
    public string LuminosityUnit { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityPage"/> class.
    /// </summary>
    /// <param name="farm">The farm associated with the security controls.</param>
    public SecurityPage(Farm farm)
    {
        InitializeComponent();
        Farm = farm;
        SecurityController = farm.SecurityController;
        App.IOTService.MessageReceived += UpdateCharts;
        BindingContext = SecurityController;
    }

    private void UpdateCharts(string s, string s2)
    {
        //LoudChart.BindingContext = SecurityController.Charts[ReadingType.LOUDNESS];
        LumiChart.BindingContext = SecurityController.Charts[ReadingType.LUMINOSITY];
        //MotionChart.BindingContext = SecurityController.Charts[ReadingType.MOTION];
        //VibrationChart.BindingContext = SecurityController.Charts[ReadingType.VIBRATION];
    }
}