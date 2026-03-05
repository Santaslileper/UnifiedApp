using System;
using System.Drawing;
using System.Windows.Forms;
using DesktopWidgets;

public class SimpleCalculatorWidget : WidgetHost
{
    public SimpleCalculatorWidget(string id = null) : base("SimpleCalculator", 200, 200, id)
    {
        // Input for first number
        TextBox num1 = new TextBox
        {
            Text = "0",
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Top,
            TextAlign = HorizontalAlignment.Right
        };

        // Input for second number
        TextBox num2 = new TextBox
        {
            Text = "0",
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Top,
            TextAlign = HorizontalAlignment.Right
        };

        // Operator selector
        ComboBox op = new ComboBox
        {
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI", 10),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        op.Items.AddRange(new string[] { "+", "-", "*", "/" });
        op.SelectedIndex = 0;

        // Button to calculate
        Button calc = new Button
        {
            Text = "Calculate",
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI", 10)
        };

        // Result display
        Label result = new Label
        {
            Text = "Result: ",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Handle calculation
        calc.Click += (s, e) =>
        {
            if (double.TryParse(num1.Text, out double a) && double.TryParse(num2.Text, out double b))
            {
                double res = 0;
                switch (op.SelectedItem.ToString())
                {
                    case "+": res = a + b; break;
                    case "-": res = a - b; break;
                    case "*": res = a * b; break;
                    case "/":
                        if (b == 0) { result.Text = "Result: Cannot divide by 0"; return; }
                        res = a / b; break;
                }
                result.Text = "Result: " + res;
            }
            else
            {
                result.Text = "Result: Invalid input";
            }
        };

        // Add controls in order
        ContentPanel.Controls.Add(result);
        ContentPanel.Controls.Add(calc);
        ContentPanel.Controls.Add(op);
        ContentPanel.Controls.Add(num2);
        ContentPanel.Controls.Add(num1);

        // Finish setup
        Load();
    }
}