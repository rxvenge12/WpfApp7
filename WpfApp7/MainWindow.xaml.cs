using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Student> students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            // Загрузка данных студентов
            LoadStudents();
            // Заполнение списка студентов при запуске приложения
            UpdateStudentListView(students);
        }

        private void LoadStudents()
        {
            // Здесь можно загрузить данные студентов из базы данных или другого источника
            // Для примера, создадим несколько студентов вручную
            students.Add(new Student("Иванов Иван Иванович", "Программирование", 2, 75, 3));
            students.Add(new Student("Петров Петр Петрович", "Математика", 3, 80, 2));
            students.Add(new Student("Сидоров Сидор Сидорович", "Физика", 1, 90, 1));
        }

        private void UpdateStudentListView(List<Student> studentList)
        {
            // Очистка предыдущего списка
            StudentListView.Items.Clear();
            // Добавление студентов в список
            foreach (var student in studentList)
            {
                StudentListView.Items.Add(student);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение параметров поиска из текстовых полей и выпадающих списков
            string searchString = SearchTextBox.Text;
            string specialty = SpecialtyComboBox.SelectedItem?.ToString();
            int course = CourseComboBox.SelectedIndex + 1; // Индекс начинается с 0, курс с 1
            int minScore = int.Parse(MinScoreTextBox.Text);
            int maxScore = int.Parse(MaxScoreTextBox.Text);

            // Выполнение поиска с учетом параметров
            var filteredStudents = students.Where(student =>
                student.FullName.Contains(searchString) &&
                (specialty == null || student.Specialty == specialty) &&
                (course == 0 || student.Course == course) &&
                student.AverageScore >= minScore &&
                student.AverageScore <= maxScore
            ).ToList();

            // Сортировка по стажу работы и курсу
            filteredStudents = filteredStudents.OrderBy(student => student.Experience)
                                               .ThenBy(student => student.Course)
                                               .ToList();

            // Обновление списка студентов на экране
            UpdateStudentListView(filteredStudents);
        }
    }

    public class Student
    {
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public int Course { get; set; }
        public int AverageScore { get; set; }
        public int Experience { get; set; }

        public Student(string fullName, string specialty, int course, int averageScore, int experience)
        {
            FullName = fullName;
            Specialty = specialty;
            Course = course;
            AverageScore = averageScore;
            Experience = experience;
        }

        public override string ToString()
        {
            return $"{FullName} - {Specialty}, {Course} курс, Средний балл: {AverageScore}, Стаж работы: {Experience}";
        }
    }
}
