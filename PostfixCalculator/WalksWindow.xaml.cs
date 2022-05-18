using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PostfixCalculator
{
    /// <summary>
    /// Interaction logic for WalksWindow.xaml
    /// </summary>
    public partial class WalksWindow : Window
    {
        public WalksWindow()
        {
            InitializeComponent();
        }

        private void StepBtn_Click(object sender, RoutedEventArgs e)
        {
            int firstNum = MainWindow.numbers.Pop();
            int secondNum = MainWindow.numbers.Pop();
            string op = MainWindow.operators.Dequeue();

            int res = GetOperationResult(firstNum, secondNum, op);

            Proccess.Text = firstNum + " " + op + " " + secondNum + " = " + res;

            MainWindow.numbers.Push(res);
            MainWindow.PasteNumbersInStack(MainWindow.numbers);
            MainWindow.PasteOperatorsInQueue(MainWindow.operators);

            if (MainWindow.numbers.Size == 1)
            {
                StepBtn.IsEnabled = false;
                Proccess.Text = res.ToString();
            }
        }

        private int GetOperationResult(int firstNum, int secondNum, string op)
        {
            switch (op)
            {
                case "+":
                    return firstNum + secondNum;

                case "-":
                    return firstNum - secondNum;

                case "*":
                    return firstNum * secondNum;
                
                case "/":
                    return firstNum / secondNum;

                default:
                    break;
            }

            return 0;
        }
    }
}
