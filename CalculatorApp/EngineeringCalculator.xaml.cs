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
        none
    }

    Operation operation = Operation.none;

    double leftOperand = double.MinValue;

    private void OnBtnNumClicked(object sender, System.EventArgs e)
    {
        Button currentBtn = (Button)sender;

        if (resLabel.Text == "0")
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
            "*" => Operation.multiplication,
            "/" => Operation.division
        };

        if (resLabel.Text != "0")
            leftOperand = double.Parse(resLabel.Text);

        OnBtnClearEntryClicked(sender, e);
    }

    private void OnBtnSimpleOperationClicked(object sender, System.EventArgs e)
    {
        Button currentBtn = (Button)sender;

        switch (currentBtn.Text)
        {
            case "2_/x":
                resLabel.Text = Math.Sqrt(double.Parse(resLabel.Text)).ToString();
                break;
            case "1/x":
                resLabel.Text = (double.Parse(resLabel.Text) / 100).ToString();
                break;
            case "+/-":
                if (resLabel.Text[0] != '-' && resLabel.Text != "0")
                    resLabel.Text = '-' + resLabel.Text;
                else
                    resLabel.Text = resLabel.Text.Substring(1);
                break;
            case "x^2":
                resLabel.Text = Math.Pow(double.Parse(resLabel.Text), 2).ToString();
                break;
        }
    }

    private void OnBtnEqualsClicked(object sender, System.EventArgs e)
    {
        switch (operation)
        {
            case Operation.addition:
                resLabel.Text = (leftOperand + double.Parse(resLabel.Text)).ToString();
                leftOperand = double.Parse(resLabel.Text);
                operation = Operation.none;
                break;
            case Operation.subtrarion:
                resLabel.Text = (leftOperand - double.Parse(resLabel.Text)).ToString();
                leftOperand = double.Parse(resLabel.Text);
                operation = Operation.none;
                break;
            case Operation.multiplication:
                resLabel.Text = (leftOperand * double.Parse(resLabel.Text)).ToString();
                leftOperand = double.Parse(resLabel.Text);
                operation = Operation.none;
                break;
            case Operation.division:
                if (double.Parse(resLabel.Text) == 0)
                {
                    resLabel.Text = "0";
                }

                else
                {
                    resLabel.Text = (leftOperand / double.Parse(resLabel.Text)).ToString();
                    leftOperand = double.Parse(resLabel.Text);
                    operation = Operation.none;
                }

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
            switch (operation)
            {
                case Operation.addition:
                    resLabel.Text = (leftOperand + (leftOperand * double.Parse(resLabel.Text) / 100)).ToString();
                    break;
                case Operation.subtrarion:
                    resLabel.Text = (leftOperand - (leftOperand * double.Parse(resLabel.Text) / 100)).ToString();
                    break;
                case Operation.multiplication:
                    resLabel.Text = (leftOperand * double.Parse(resLabel.Text) / 100).ToString();
                    break;
                case Operation.division:
                    resLabel.Text = (leftOperand / double.Parse(resLabel.Text) * 100).ToString();
                    break;
                default:
                    resLabel.Text = "0";
                    break;
            }
        }
    }
}