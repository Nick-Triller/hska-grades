using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;

namespace Hska_Grades_Tool
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ExamGrades> examGrades = new ObservableCollection<ExamGrades>();

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = examGrades;
            groupDataGridBySemester();
        }

        public void groupDataGridBySemester()
        {
            CollectionViewSource.GetDefaultView(examGrades).GroupDescriptions.Add(new PropertyGroupDescription("Semester"));
        }

        public void ungroupDataGrid()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(examGrades);
            view.GroupDescriptions.Clear();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            String username = usernameTextBox.Text;
            String password = passwordTextBox.Password;
            List<ExamGrades> examGradesList;
            if (username == "" && password == "")
            {
                examGradesList = MockGradesService.getGrades(username, password);
            }
            else
            {
                examGradesList = GradesService.getGrades(username, password);
            }
            examGrades.Clear();
            for (int i = 0; i < examGradesList.Count; i++)
            {
                examGrades.Add(examGradesList[i]);
            }
        }
    }

    [ValueConversion(typeof(float), typeof(string))]    
    public class FloatGradeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float grade = -1;
            if (value != null)
            {
                grade = (float)value;
            }
            if (grade == -1)
            {
                return "-";
            }
            return grade.ToString("0.0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

