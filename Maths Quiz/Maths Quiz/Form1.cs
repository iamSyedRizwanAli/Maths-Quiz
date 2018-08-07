using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maths_Quiz
{
    public partial class Form1 : Form
    {
        private string correct_answer_text = "The correct answer is: ";
        private string solution = "";

        private RadioButton[] radioButtons = new RadioButton[4];
        private bool[] booked_buttons = new bool[4];

        private bool solution_given = false;
        private byte correct_option = 5;

        private int correct_answers = 0;
        private int total_questions = 0;
        private float score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private sbyte isSelectedOptionCorrect()
        {
            int users_choice = radioButton1.Checked ? 0 : radioButton2.Checked ? 1 : radioButton3.Checked ? 2 : radioButton4.Checked ? 3 : -1;

            if (users_choice == -1)
            {
                MessageBox.Show("Please select an option", "Warning");
                return -1;
            }
            else if (users_choice == correct_option)
                return 1;
            else
                return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!solution_given)
            {
                sbyte correctness = isSelectedOptionCorrect();
                
                if (correctness != -1)
                {
                    score = (correct_answers * 100) / total_questions;

                    if (correctness == 1)
                        correct_answers++;

                    label1.Text = correct_answer_text + solution;
                    button1.Text = "Next";
                    solution_given = true;

                    textBox4.Text = score.ToString() + "%";
                    textBox3.Text = correct_answers.ToString();
                    textBox2.Text = total_questions.ToString();
                }
            }
            else
            {
                label1.Text = correct_answer_text;
                button1.Text = "Check";
                solution_given = false;

                setUpQuestion();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButtons[0] = radioButton1;
            radioButtons[1] = radioButton2;
            radioButtons[2] = radioButton3;
            radioButtons[3] = radioButton4;

            setUpQuestion();
            
        }

        private void setUpQuestion()
        {
            total_questions++;

            Equation equation = new Equation();
            textBox1.Text = equation.ThisEquation;
            solution = equation.Solution;

            SetOptions();

            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
            booked_buttons[0] = booked_buttons[1] = booked_buttons[2] = booked_buttons[3] = false;
        }

        private void SetOptions()
        {
            double theSolution = Convert.ToDouble(solution);
            Random random_machine = new Random();

            double[] other_options = new double[3];

            other_options[0] = theSolution + 1;
            other_options[1] = theSolution - 1;
            other_options[2] = theSolution / 10;
            
            //for correct option
            int selected_radiobutton= random_machine.Next(4);
            correct_option = (byte)selected_radiobutton;
            radioButtons[selected_radiobutton].Text = solution;
            booked_buttons[selected_radiobutton] = true;

            int i = 0;

            while(i != 3)
            {
                while (booked_buttons[selected_radiobutton])
                    selected_radiobutton = random_machine.Next(4);

                radioButtons[selected_radiobutton].Text = other_options[i].ToString();
                booked_buttons[selected_radiobutton] = true;

                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setUpQuestion();

            score = (correct_answers * 100) / total_questions;
            textBox4.Text = score.ToString() + "%";
            textBox3.Text = correct_answers.ToString();
            textBox2.Text = total_questions.ToString();
        }
    }
}
