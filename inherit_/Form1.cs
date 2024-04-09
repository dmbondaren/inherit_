using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inherit_
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        private List<Employee> employees = new List<Employee>();

        public Form1()
        {
            InitializeComponent();
            students.Add(new Student { Name = "Іван", Age = 20, StudentId = 1 });
            students.Add(new Student { Name = "Марія", Age = 22, StudentId = 2 });

            employees.Add(new Employee { Name = "Петро", Age = 30, Workplace = "Компанія А", Salary = 400 });
            employees.Add(new Employee { Name = "Оксана", Age = 35, Workplace = "Компанія Б", Salary = 6000 });

            // Додаємо відповідність кнопок з методами обробки подій
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (var student in students)
            {
                listBox1.Items.Add($"Студент: {student.Name}, Вік: {student.Age}");
            }

            foreach (var employee in employees)
            {
                listBox1.Items.Add($"Працівник: {employee.Name}, Вік: {employee.Age}, Дохід: {employee.CalculateIncome()}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            var filteredEmployees = employees.Where(emp => emp.CalculateIncome() < 5000);
            foreach (var employee in filteredEmployees)
            {
                listBox1.Items.Add($"Працівник: {employee.Name}, Вік: {employee.Age}, Дохід: {employee.CalculateIncome()}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear(); // Очищуємо вміст listBox2 перед виведенням

            var filteredPeople = students.Concat<Person>(employees).Where(person => person.Age < 25);
            foreach (var person in filteredPeople)
            {
                if (person is Student)
                {
                    var student = (Student)person;
                    listBox1.Items.Add($"Студент: {student.Name}, Вік: {student.Age}");
                }
                else if (person is Employee)
                {
                    var employee = (Employee)person;
                    listBox1.Items.Add($"Працівник: {employee.Name}, Вік: {employee.Age}, Дохід: {employee.CalculateIncome()}");
                }
            }

            // Отримуємо тільки студентів з третьої колекції
            var filteredStudents = filteredPeople.OfType<Student>();
            foreach (var student in filteredStudents)
            {
                listBox2.Items.Add($"Студент: {student.Name}, Вік: {student.Age}");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Student : Person
    {
        public int StudentId { get; set; }
    }

    public class Employee : Person
    {
        public string Workplace { get; set; }
        public double Salary { get; set; }

        public double CalculateIncome()
        {
            return Salary; // Поки що просто повертаємо зарплату як дохід
        }
    }
}
