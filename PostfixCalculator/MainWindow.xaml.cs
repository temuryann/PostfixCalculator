using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace PostfixCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex regex = new Regex(@"[0-9 + \- / *]");
        private static WalksWindow walksWindow = new WalksWindow();
        private Stopwatch stopwatch = new Stopwatch();
        internal static CustomStack<int> numbers = new CustomStack<int>();
        internal static CustomQueue<string> operators = new CustomQueue<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WalksWindowShowBtn_Click(object sender, RoutedEventArgs e)
        {
            walksWindow.Show();
        }

        private bool IsTextAllowed(string text)
        {
            return !regex.IsMatch(text);
        }

        private void postfixTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsTextAllowed(e.Text))
                e.Handled = true;

            if (stopwatch.ElapsedMilliseconds > 1000)
                PasteSpace(e);

            stopwatch.Reset();
            stopwatch.Start();
        }

        private bool IsValid()
        {
            string txt = string.Empty;
            int number;
            int countForLogic = 0;
            int countForNumber = 0;
            int countForOperators = 0;

            for (int i = 0; i < postfixTxt.Text.Length; i++)
            {
                if (postfixTxt.Text[i] == ' ')
                {
                    countForLogic++;

                    if (countForLogic == 2)
                    {
                        i--;
                        countForLogic = 0;

                        if (int.TryParse(txt, out number))
                        {
                            numbers.Push(number);
                            countForNumber++;
                        }
                        else
                        {
                            operators.Enqueue(txt);
                            countForOperators++;
                        }

                        txt = string.Empty;
                    }
                }

                if (countForLogic == 1 && postfixTxt.Text[i] != ' ')
                    txt += postfixTxt.Text[i];
            }

            if (countForNumber - countForOperators == 1)
                return true;

            numbers.Clear();
            operators.Clear();
            return false;
        }

        private void PasteSpace(TextCompositionEventArgs e)
        {
            if (e.Handled == false && postfixTxt.Text[postfixTxt.Text.Length - 1] != ' ')
            {
                postfixTxt.Text += " ";
                postfixTxt.Select(postfixTxt.Text.Length, 0);
            }
        }

        private void walkBtn_Click(object sender, RoutedEventArgs e)
        {
            postfixTxt.Text += " ";
            AllValidation();
        }

        private void AllValidation()
        {
            if (IsValid())
            {
                walkBtn.IsEnabled = false;
                postfixTxt.Foreground = Brushes.Green;
                IsWalksWindowShow();
                MessageBox.Show("Validation Was Completed Successfully");
                PasteNumbersInStack(numbers);
                PasteOperatorsInQueue(operators);
            }
            else
            {
                postfixTxt.Foreground = Brushes.Red;
                MessageBox.Show("Postfix Syntax Error, Please Change & Try Click Walks Button");
                postfixTxt.Foreground = Brushes.Black;
            }
        }

        private async void IsWalksWindowShow()
        {
            if (!walksWindow.IsActive)
            {
                await Task.Delay(500);
                walksWindow.Show();
                WalksWindowShowBtn.IsEnabled = false;
                postfixTxt.IsReadOnly = true;
            }
        }

        internal static async void PasteNumbersInStack(CustomStack<int> numbers)
        {
            walksWindow.ContainerStack.Children.Clear();
            int count = 0;

            foreach (var item in numbers)
            {
                count++;
                TextBox myTextBox = new TextBox() { Text = $"Number {count}: {item}", Width = 100, Height = 20, FontSize = 10, VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = true };
                await Task.Delay(700);
                walksWindow.ContainerStack.Children.Insert(0, myTextBox);
            }
        }

        internal static async void PasteOperatorsInQueue(CustomQueue<string> operators)
        {
            walksWindow.ContainerQueue.Children.Clear();
            int count = 0;

            foreach (var item in operators)
            {
                count++;
                TextBox myTextBox = new TextBox() { Text = $"Operator {count}: {item}", Width = 100, Height = 20, FontSize = 10, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, IsReadOnly = true };
                await Task.Delay(700);
                walksWindow.ContainerQueue.Children.Insert(0, myTextBox);
            }
        }

        private void resultShow_Click(object sender, RoutedEventArgs e)
        {
            if (walksWindow.StepBtn.IsEnabled == false)
            {
                resultTxt.Text += walksWindow.Proccess.Text;
                resultShow.IsEnabled = false;
                MessageBox.Show("Good Job !");
            }
            else
                MessageBox.Show("Are You Okay ?");
        }
    }
}
