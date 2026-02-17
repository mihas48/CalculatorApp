namespace CalculatorApp;

public partial class EngineeringCalculator : ContentPage
{
    public EngineeringCalculator()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

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
        power,
        none
    }

    Operation operation = Operation.none;

    double leftOperand = double.MinValue;

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

        leftOperand = double.MinValue;
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
            "x^y" => Operation.power,
            _ => Operation.none
        };

        if (resLabel.Text != "0" && resLabel.Text != "0,")
            double.TryParse(resLabel.Text, out leftOperand);

        OnBtnClearEntryClicked(sender, e);
    }

    private void OnBtnSimpleOperationClicked(object sender, System.EventArgs e)
    {
        static double Factorial(double num)
        {
            if (num == 0 || num == 1) return num;
            return num * Factorial(num - 1);
        }

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
            case "n!":
                if (operand < 0)
                    resLabel.Text = "Ошибка! Невозможно найти факториал от отрицательного числа!";
                else if (operand > 1000)
                    resLabel.Text = "Ошибка! Переполнение";
                else if (operand % 1 != 0)
                    resLabel.Text = "Ошибка! Невозможно найти факториал от дробного числа!";
                else
                    resLabel.Text = Factorial(operand).ToString();
                break;
            case "x³":
                resLabel.Text = Math.Pow(operand, 3).ToString();
                break;
            case "10ⁿ":
                resLabel.Text = Math.Pow(10, operand).ToString();
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
            case Operation.power:
                resLabel.Text = Math.Pow(leftOperand, operand).ToString();
                double.TryParse(resLabel.Text, out leftOperand);
                operation = Operation.none;
                break;
            case Operation.none:
                break;
        }
    }

    private void OnBtnPercentageClicked(object sender, System.EventArgs e)
    {
        if (leftOperand == double.MinValue || leftOperand == 0)
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

    private void OnBtnConstantClicked(object sender, System.EventArgs e)
    {
        Button CurrentBtn = (Button)sender;

        switch (CurrentBtn.Text)
        {
            case "π":
                resLabel.Text = Math.PI.ToString();
                break;
            case "e":
                resLabel.Text = Math.E.ToString();
                break;
        }
    }

    private void OnBtnTrigClicked(object sender, System.EventArgs e)
    {
        double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        double operand;
        double.TryParse(resLabel.Text, out operand);

        Button CurrentBtn = (Button)sender;

        switch (CurrentBtn.Text)
        {
            case "sin":
                resLabel.Text = Math.Sin(DegreesToRadians(operand)).ToString();
                break;
            case "cos":
                resLabel.Text = Math.Cos(DegreesToRadians(operand)).ToString();
                break;
            case "tan":
                if (operand == 90)
                    resLabel.Text = "Ошибка! Невозможно найти тангенс от 90 градусов";
                else
                    resLabel.Text = Math.Tan(DegreesToRadians(operand)).ToString();
                break;
            case "ln":
                if (operand <= 0)
                    resLabel.Text = "Ошибка! Невозможно найти логарифм от этого числа!";
                else
                    resLabel.Text = Math.Log(operand).ToString();  
                break;
            case "log":
                if (operand <= 0)
                    resLabel.Text = "Ошибка! Невозможно найти логарифм от этого числа!";
                else
                    resLabel.Text = Math.Log10(operand).ToString();  
                break;
        }
    }
}