namespace _253504_Novikov_Lab1
{   

    enum State
    {
        OperatorPressed,
        OperatorJustPressed,
        Cleared,
        Calculated
    }
    public partial class MainPage : ContentPage
    {
        double operand = 0;
        string lastOperator = string.Empty;
        State state = State.Cleared;

        public MainPage()
        {
            InitializeComponent();
        }

        private void resetData()
        {
            operand = 0;
            lastOperator = string.Empty;
            state = State.Cleared;
        }

        private void calculate(string inputOperator)
        {
            switch(inputOperator)
            {
                case "+":
                    operand += Convert.ToDouble(result.Text);
                    break;
                case "-":
                    operand -= Convert.ToDouble(result.Text);
                    break;
                case "×":
                    operand *= Convert.ToDouble(result.Text);
                    break;
                case "÷":
                    operand /= Convert.ToDouble(result.Text);
                    break;
                case "^":
                    operand = Math.Pow(operand, Convert.ToDouble(result.Text));
                    break;
            }
        }

        private void FactionClicked(object sender, EventArgs e)
        {
            if (!result.Text.Contains("."))
            {
                if(state == State.OperatorJustPressed || state == State.Calculated)
                {
                    result.Text = "0.";
                    if (state == State.OperatorJustPressed) state = State.OperatorPressed;
                    else state = State.Cleared;
                }
                else
                {
                    result.Text += ".";
                }
            }
        }

        private void PowerClicked(object sender, EventArgs e)
        {
            result.Text = (Math.Pow(Convert.ToDouble(result.Text), 2)).ToString();
            if (state == State.OperatorJustPressed) state = State.OperatorPressed;
        }

        private void SquareClicked(object sender, EventArgs e)
        {
            result.Text = (Math.Sqrt(Convert.ToDouble(result.Text))).ToString();
            if (state == State.OperatorJustPressed) state = State.OperatorPressed;
        }

        private void SignClicked(object sender, EventArgs e)
        {
            if(result.Text != "0" && result.Text != "0.")
            {
                if (result.Text.Contains("-"))
                {
                    result.Text = result.Text.Remove(0, 1);
                }
                else result.Text = result.Text.Insert(0, "-");

                if(state == State.Calculated)
                {
                    upperResult.Text = string.Empty;
                    resetData();
                    state = State.Cleared;
                }
                else if (state == State.OperatorJustPressed)
                {
                    state = State.OperatorPressed;
                }
            }
        }

        private void InverseProportionClicked(object sender, EventArgs e)
        {
            result.Text = (1 / Convert.ToDouble(result.Text)).ToString();
            if (state == State.OperatorJustPressed) state = State.OperatorPressed;
        }

        private void EraseClicked(object sender, EventArgs e)
        {
            if (state != State.OperatorJustPressed)
            {
                result.Text = result.Text.Remove(result.Text.Length - 1);
                if (result.Text.Length == 0) result.Text = "0";
            }
        }

        private void ClearEverythingClicked(object sender, EventArgs e)
        {
            resetData();
            state = State.Cleared;
            upperResult.Text = string.Empty;
            result.Text = "0";
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            if(state != State.Calculated)
            {
                result.Text = "0";
            }
            else
            {
                ClearEverythingClicked(sender, e);
            }
        }

        private void EvaluateClicked(object sender, EventArgs e)
        {
            double temp = 0;
            double temp2 = 0;
            if (state == State.Cleared) return;
            if (state == State.OperatorJustPressed)
            {
                temp = operand;
                calculate(lastOperator);
                result.Text = operand.ToString();
                operand = temp;
                upperResult.Text = operand.ToString() + " " + lastOperator + " " + operand.ToString() + " =";
                state = State.Calculated;
            }
            else if (state == State.OperatorPressed)
            {
                temp2 = operand; //47
                calculate(lastOperator); //47 - 2 = 45
                temp = operand; //45
                operand = Convert.ToDouble(result.Text); //2
                result.Text = temp.ToString();
                upperResult.Text = temp2.ToString() + " " + lastOperator + " " + operand.ToString() + " =";
                state = State.Calculated;
            }
            else if (state == State.Calculated)
            {
                temp = operand; // 2
                temp2 = operand = Convert.ToDouble(result.Text); //45
                result.Text = temp.ToString(); //2
                calculate(lastOperator);
                upperResult.Text = temp2.ToString() + " " + lastOperator + " " + temp.ToString() + " =";
                result.Text = operand.ToString();
                operand = temp;
            }
        }

        private void OperationsClicked(object sender, EventArgs e)
        {
            if(state == State.OperatorJustPressed || state == State.Cleared || state == State.Calculated)
            {
                lastOperator = ((Button)sender).Text;
                if (lastOperator == "xʸ") lastOperator = "^";
                operand = Convert.ToDouble(result.Text);
                upperResult.Text = operand.ToString() + " " + lastOperator;
            }
            else
            {
                calculate(lastOperator);
                lastOperator = ((Button)sender).Text;
                if (lastOperator == "xʸ") lastOperator = "^";
                upperResult.Text = operand.ToString() + " " + lastOperator;
                result.Text = operand.ToString();
            }
            state = State.OperatorJustPressed;
        }
        private void OnClicked(object sender, EventArgs e)
        {
            var input = ((Button)sender).Text;
            if (state == State.Cleared || state == State.OperatorJustPressed)
            {
                if (result.Text == "0" || state == State.OperatorJustPressed) {
                    result.Text = input;
                    if (state == State.OperatorJustPressed) state = State.OperatorPressed;
                    return;
                }
            }
            if (state == State.Calculated)
            {
                resetData();
                result.Text = input;
                upperResult.Text = string.Empty;
                state = State.Cleared;
                return;
            }
            if (result.Text.Length < 13)
            {
                result.Text += input;
            }
        }


    }

}
