using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Quiz
{
    public class Equation
    {
        public String ThisEquation { get; }
        public String Solution { get; }

        private List<String> SplittedEquationForSolution;

        private static char[] operators = { '/', '*', '+', '-'};

        public Equation()
        {
            SplittedEquationForSolution = new List<string>();
            ThisEquation = GenerateAnEquation();
            Solution = SolveTheEquation();
        }
        
        private String GenerateAnEquation()
        {
            Random random_machine = new Random();
            int number_of_coeffs = random_machine.Next(4) + 2;
            string equation = "";

            for (int i = 0; i < number_of_coeffs; i++)
            {
                string temp = (random_machine.Next(15) + 1).ToString();
                equation += temp;
                SplittedEquationForSolution.Add(temp);

                if (i != number_of_coeffs - 1)
                {
                    temp = operators[random_machine.Next(operators.Length)].ToString();
                    equation += temp;
                    SplittedEquationForSolution.Add(temp);
                }
            }

            return equation;
        }

        private string SolveTheEquation()
        {
            bool solution_flag = false;

            int subjected_operator = 0;

            while(!solution_flag)
            {
                int idx = SplittedEquationForSolution.IndexOf(operators[subjected_operator].ToString());

                if (idx == -1)
                {
                    subjected_operator++;
                    if (subjected_operator >= operators.Length)
                        solution_flag = true;
                }
                else
                    PerformOperation(idx);
            }

            return SplittedEquationForSolution[0];
        }

        private void PerformOperation(int index)
        {
            double alpha = Convert.ToDouble(SplittedEquationForSolution[index - 1]);
            double beta = Convert.ToDouble(SplittedEquationForSolution[index + 1]);

            double answer = 0;

            if (SplittedEquationForSolution[index] == "+")
                answer = alpha + beta;
            else if (SplittedEquationForSolution[index] == "-")
                answer = alpha - beta;
            else if (SplittedEquationForSolution[index] == "*")
                answer = alpha * beta;
            else if (SplittedEquationForSolution[index] == "/")
                answer = alpha / beta;

            answer = Math.Round(answer, 2);

            SplittedEquationForSolution[index - 1] = answer.ToString();
            SplittedEquationForSolution.RemoveAt(index + 1);
            SplittedEquationForSolution.RemoveAt(index);
        }

    }
}
