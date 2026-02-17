using System.Security.AccessControl;

namespace CalculatorApp
{
    public partial class MainPage : ContentPage
    {
        enum Operation
        {
            addition,
            subtrarion,
            multiplication,
            division,
            squareRoot,
            percentage,
            inverse,
            squaring,
            none
        }

        Operation operation = Operation.none;

        double leftOperand = 0;


        public MainPage()
        {
            InitializeComponent();

            Button toCommonPageBtn = new Button
            {
                Text = "Common",
                HorizontalOptions = LayoutOptions.Start
            };
            toCommonPageBtn.Clicked += ToEngineeringCalculator;
        }

        // Смена режима на инженерный калькулятор
        private async void ToEngineeringCalculator(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new EngineeringCalculator());
        }


        //===== Обработка нажатия кнопок =====

        private void OnBtnNumClicked(object sender, System.EventArgs e)
        {
            double num;

            Button currentBtn = (Button)sender;

            if (resLabel.Text == "0" || !double.TryParse(resLabel.Text, out num))
                resLabel.Text = "";
            resLabel.Text += currentBtn.Text;
        }

        private void OnBtnDelClicked(object sender, System.EventArgs e)
        {
            if (resLabel.Text != "")
            {
                resLabel.Text = resLabel.Text.Remove(resLabel.Text.Length - 1, 1);
                if (resLabel.Text.Length == 0)
                {
                    resLabel.Text = "0";
                }
            }
        }

        private void OnBtnClearEntryClicked(object sender, System.EventArgs e)
        {
            resLabel.Text = "0";
        }

        private void OnBtnClearClicked(object sender, System.EventArgs e)
        {
            OnBtnClearEntryClicked(sender, e);

            leftOperand = 0;
            operation = Operation.none;
        }

        private void OnBtnPointClicked(object sender, System.EventArgs e)
        {
            if (!resLabel.Text.Contains(','))
            {
                resLabel.Text += ",";
            }
        }

        private void OnBtnComplexOperationClicked(object sender, System.EventArgs e)
        {
            Button currentBtn = (Button)sender;

            operation = currentBtn.Text switch
            {
                "+" => Operation.addition,
                "-" => Operation.subtrarion,
                "×" => Operation.multiplication,
                "÷" => Operation.division,
                _ => Operation.none  
            };

            // Запоминает значение поля ввода, если оно не пустое
            if (resLabel.Text != "0" && resLabel.Text != "0,")
                double.TryParse(resLabel.Text, out leftOperand);

            OnBtnClearEntryClicked(sender, e);
        }

        private void OnBtnSimpleOperationClicked(object sender, System.EventArgs e)
        {
            double operand;
            double.TryParse(resLabel.Text, out operand);

            Button currentBtn = (Button)sender;

            switch (currentBtn.Text)
            {
                case "√x":
                    if (operand < 0)
                        resLabel.Text = "Ошибка! Невозможно извлечь корень из отрицательного числа!";
                    else
                        resLabel.Text = Math.Sqrt(operand).ToString();
                    break;
                case "1/x":
                    if (resLabel.Text == "0" || resLabel.Text == "0,")
                        resLabel.Text = "Ошибка! Деление на ноль невозможно!";
                    else
                        resLabel.Text = (1.0 / operand).ToString();
                    break;
                case "+/-":
                    if (resLabel.Text[0] != '-' && (resLabel.Text != "0" && resLabel.Text != "0,"))
                        resLabel.Text = '-' + resLabel.Text;
                    else if (resLabel.Text != "0" && resLabel.Text != "0,")
                        resLabel.Text = resLabel.Text.Substring(1);
                    break;
                case "x²":
                    resLabel.Text = Math.Pow(operand, 2).ToString();
                    break;
            }
        }

        private void OnBtnEqualsClicked(object sender, System.EventArgs e)
        {
            double operand;
            double.TryParse(resLabel.Text, out operand);

            switch (operation)
            {
                case Operation.addition:
                    resLabel.Text = (leftOperand + operand).ToString();
                    double.TryParse(resLabel.Text, out leftOperand);
                    operation = Operation.none;
                    break;
                case Operation.subtrarion:
                    resLabel.Text = (leftOperand - operand).ToString();
                    double.TryParse(resLabel.Text, out leftOperand);
                    operation = Operation.none;
                    break;
                case Operation.multiplication:
                    resLabel.Text = (leftOperand * operand).ToString();
                    double.TryParse(resLabel.Text, out leftOperand);
                    operation = Operation.none;
                    break;
                case Operation.division:
                    if (operand == 0)
                    {
                        resLabel.Text = "Ошибка! Деление на ноль невозможно!";
                    }
                    else
                    {
                        resLabel.Text = (leftOperand / operand).ToString();
                        double.TryParse(resLabel.Text, out leftOperand);
                        operation = Operation.none;
                    }
                    break;
                case Operation.none:
                    break;
            }
        }

        private void OnBtnPercentageClicked(object sender, System.EventArgs e)
        {
            if (leftOperand == 0)
            {
                resLabel.Text = "0";
            }
            else
            {
                double operand;
                double.TryParse(resLabel.Text, out operand);

                switch (operation)
                {
                    case Operation.addition:
                        resLabel.Text = (leftOperand + (leftOperand * operand / 100)).ToString();
                        break;
                    case Operation.subtrarion:
                        resLabel.Text = (leftOperand - (leftOperand * operand / 100)).ToString();
                        break;
                    case Operation.multiplication:
                        resLabel.Text = (leftOperand * operand / 100).ToString();
                        break;
                    case Operation.division:
                        resLabel.Text = (leftOperand / operand * 100).ToString();
                        break;
                    default:
                        resLabel.Text = "0";
                        break;
                }
            }
        }
    }
}