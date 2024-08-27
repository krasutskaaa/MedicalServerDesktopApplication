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
namespace CourseworkMedicalServerDesktopApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 

public partial class MainWindow : Window
{
    private Doctor _doctor;
    public MainWindow()
    {
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
}

