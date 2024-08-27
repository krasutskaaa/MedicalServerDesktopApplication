using SharedLibrary.Dtos;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharedLibrary.Abstractions.Entities;
using SharedLibrary.Encryption;
using CourseworkMedicalServerDesktopApplication.DataServices;
using CourseworkMedicalServerDesktopApplication.WindowEntities;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;
namespace CourseworkMedicalServerDesktopApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 

public partial class MainWindow : Window
{
    public bool IsDarkTheme { get; set; }
    private readonly PaletteHelper _palleteHelper;
    private Doctor _doctor;
    private List<Test> _patientsTests;
    public MainWindow()
    {
        _palleteHelper = new PaletteHelper();
        InitializeComponent();
    }
    private async void OnCreateAccountClicked(object sender, RoutedEventArgs e)
    {
        var apiMethod = new DataService("http://localhost:5235/");
        var newDoctor = new CreateDoctorDto
        (
            NameTextBox.Text,
            GenderTextBox.Text,
            UsernameTextBox2.Text,
            PasswordTextBox2.Password.GenerateSHA256(),
            SpecializationTextBox.Text
        );
        await apiMethod.AddNewDoctorAsync(newDoctor);
        _doctor = await apiMethod.GetDoctorByUsernameAsync(UsernameTextBox2.Text);
        if (_doctor != null)
        {
            Storyboard storyboard = (Storyboard)this.Resources["Storyboard7"];
            storyboard.Begin();
            NameTextBox.Clear();
            GenderTextBox.Clear();
            UsernameTextBox2.Clear();
            PasswordTextBox2.Clear();
            SpecializationTextBox.Clear();

            DoctorsNameLabel.Content = new TextBlock
            {
                Inlines =
            {
                    new Run("Doctor's name: "){FontWeight = FontWeights.Bold },
                    new Run(_doctor.Name)
                }
            };
            DoctorsSpecializationLabel.Content = new TextBlock
            {
                Inlines =
            {
                    new Run("Doctor's specialization: "){FontWeight = FontWeights.Bold },
                    new Run(_doctor.Specialization)
                }
            };
        }
    }
    private async void OnLogInClicked(object sender, RoutedEventArgs e)
    {
        var apiMethod = new DataService("http://localhost:5235/");
        _doctor = await apiMethod.GetDoctorByUsernameAsync(UsernameTextBox1.Text);
        if (_doctor != null &&
            _doctor.Password.Equals(PasswordTextBox1
            .Password.GenerateSHA256()))
        {

            Storyboard storyboard = (Storyboard)this.Resources["Storyboard3"];
            storyboard.Begin();
            UsernameTextBox1.Clear();
            PasswordTextBox1.Clear();
            DoctorsNameLabel.Content = new TextBlock
            {
                Inlines =
                    {
                        new Run("Doctor's name: "){FontWeight = FontWeights.Bold },
                        new Run(_doctor.Name)
                    }
            };
            DoctorsSpecializationLabel.Content = new TextBlock
            {
                Inlines =
                    {
                        new Run("Doctor's specialization: "){FontWeight = FontWeights.Bold },
                        new Run(_doctor.Specialization)
                    }
            };
            _patientsTests = await apiMethod.GetTestsByDoctorIdAsync(_doctor.Id);

            if (_patientsTests is not null)
            {
                var converter = new BrushConverter();
                ObservableCollection<PatientsTests> patientsTestsCollection
                    = new ObservableCollection<PatientsTests>();
                var tasks = _patientsTests.Select(async test =>
                {
                    var patient = await apiMethod.GetPatientByIdAsync(test.PatientId);
                    return new
                    {
                        Test = test,
                        PatientName = patient.Name
                    };
                }).ToList();
                var results = await Task.WhenAll(tasks);

                var tests = results.GroupBy(result =>
                new { result.PatientName, result.Test.CreatedAt.Date });

                foreach (var group in tests)
                {

                    int count = 0;
                    ++count;
                    var patientsTests = new PatientsTests
                    {
                        Number = count.ToString(),
                        Name = group.Key.PatientName,
                        Date = group.Key.Date.ToString("yy-MM-dd"),

                    };

                    var groupResult = group.Where(inst => inst.Test.Value < inst.Test.MinValue ||
                    inst.Test.Value > inst.Test.MaxValue);

                    if (groupResult.Count() != 0)
                    {
                        patientsTests.RadioButtonBorderBrush = (Brush)converter.ConvertFromString("#AD1010");
                        patientsTests.RadioButtonBackground = (Brush)converter.ConvertFromString("#AD1010");
                    }
                    else
                    {
                        patientsTests.RadioButtonBorderBrush = (Brush)converter.ConvertFromString("#318B0A");
                        patientsTests.RadioButtonBackground = (Brush)converter.ConvertFromString("#318B0A");
                    }
                    patientsTestsCollection.Add(patientsTests);

                }

                PatientsTestsDataGrid.ItemsSource = patientsTestsCollection;
            }
        }
    }

    private void OnApplicationClosed(object sender, RoutedEventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }

    private void OnThemeToggled(object sender, RoutedEventArgs e)
    {
        ITheme theme = _palleteHelper.GetTheme();
        if (theme.GetBaseTheme() == BaseTheme.Dark)
        {
            IsDarkTheme = false;
            theme.SetBaseTheme(Theme.Light);
        }
        else
        {
            IsDarkTheme = true;
            theme.SetBaseTheme(Theme.Dark);
        }
        _palleteHelper.SetTheme(theme);
    }

    private async void PatientsTestsDataGridOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PatientsTestsDataGrid.SelectedItem != null)
        {

            var apiMethod = new DataService("http://localhost:5235/");
            var converter = new BrushConverter();
            var selectedPatientTest = (PatientsTests)PatientsTestsDataGrid.SelectedItem;
            string patientName = selectedPatientTest.Name;
            string patientTestDate = selectedPatientTest.Date;
            Storyboard storyboard = (Storyboard)this.Resources["Storyboard4"];
            storyboard.Begin();

            DoctorsNameLabel2.Content = new TextBlock
            {
                Inlines =
                    {
                        new Run("Doctor's name: "){FontWeight = FontWeights.Bold },
                        new Run(_doctor.Name)
                    }
            };
            DoctorsSpecializationLabel2.Content = new TextBlock
            {
                Inlines =
                    {
                        new Run("Doctor's specialization: "){FontWeight = FontWeights.Bold },
                        new Run( _doctor.Specialization)
                    }
            };
            PatientsNameLabel2.Content = new TextBlock
            {
                Inlines =
                    {
                        new Run("Patient's name: "){FontWeight = FontWeights.Bold },
                        new Run(patientName)
                    }
            };
            DateLabel.Content = new TextBlock
            {
                Inlines =
                    {
                        new Run("Test date: "){FontWeight = FontWeights.Bold },
                        new Run(patientTestDate)
                    }
            };
            PatientsTestsDataGrid.SelectedItem = null;

            ObservableCollection<PatientsTestData> patientsTestDataCollection =
                new ObservableCollection<PatientsTestData>();
            List<Test> currentPatientTests = new List<Test>();
            foreach (var test in _patientsTests)
            {
                var patient = await apiMethod.GetPatientByIdAsync(test.PatientId);
                if (patient.Name.Equals(patientName) &&
                    test.CreatedAt.Date.ToString("yy-MM-dd").Equals(patientTestDate))
                {
                    currentPatientTests.Add(test);
                }
            }

            foreach (var currentPatientTest in currentPatientTests)
            {
                int count = 0;
                ++count;
                var patientsTestData = new PatientsTestData
                {
                    Number = count.ToString(),
                    Name = currentPatientTest.Name,
                    Value = currentPatientTest.Value,
                    Measurement = currentPatientTest.UMeasurement,
                };
                if (currentPatientTest.Value < currentPatientTest.MinValue ||
                    currentPatientTest.Value > currentPatientTest.MaxValue)
                {
                    patientsTestData.RadioButtonBorderBrush = (Brush)converter.ConvertFromString("#AD1010");
                    patientsTestData.RadioButtonBackground = (Brush)converter.ConvertFromString("#AD1010");
                }
                else
                {
                    patientsTestData.RadioButtonBorderBrush = (Brush)converter.ConvertFromString("#318B0A");
                    patientsTestData.RadioButtonBackground = (Brush)converter.ConvertFromString("#318B0A");
                }
                patientsTestDataCollection.Add(patientsTestData);
            }

            PatientsTestsDataDataGrid2.ItemsSource = patientsTestDataCollection;
        }
    }

    private void OnBackwardsPatientsTestsButtonClicked(object sender, RoutedEventArgs e)
    {
        Storyboard storyboard = (Storyboard)this.Resources["Storyboard5"];
        storyboard.Begin();
    }

    private void OnBackwardsToLogInButtonClicked(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Do you want to log out?",
            "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {

            Storyboard storyboard = (Storyboard)this.Resources["Storyboard6"];
            storyboard.Begin();
        }
    }

}

