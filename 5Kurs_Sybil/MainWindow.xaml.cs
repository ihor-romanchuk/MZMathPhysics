using AutoMapper;
using PoohMathParser;
using System;
using System.Linq;
using System.Windows;

namespace _5Kurs_Sybil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StaticResources _staticResources;
        private readonly IntegralEquationSolver _integralEquationSolver;
        private readonly IMappingEngine _mappingEngine;

        public Settings Settings { get; private set; }

        public MainWindow()
        {
            _staticResources = new StaticResources();
            _mappingEngine = Mapper.Engine;
            _integralEquationSolver = new IntegralEquationSolver();

            Settings = _mappingEngine.Mapper.Map<Settings>(_staticResources.DefaultSettings.Value[GetEquationType()]);

            InitializeComponent();
        }
        private EquationsEnum GetEquationType()
        {
            return (EquationsEnum)(tabControl?.SelectedIndex ?? 0);
        }

        private void resultBtn1_Click(object sender, RoutedEventArgs e)
        {
            Settings = _mappingEngine.Mapper.Map<Settings>(_staticResources.DefaultSettings.Value[GetEquationType()]);
            Settings.FunctionF = new MathExpression(FixMinus(functionGTextBox1.Text));
            Settings.AmountOfPartitions = Convert.ToInt32(amountOfPartitionsTextBox1.Text);
            Settings.ColocationPoints = Settings.GetDefaultColocationPoints();

            var result = _integralEquationSolver.Solve(Settings, _staticResources.MatrixAInits.Value[GetEquationType()],
                _staticResources.MatrixBInits.Value[GetEquationType()]);

            ResultWindow resultWindow = new ResultWindow(result);
            resultWindow.Show();
        }

        private void resultBtn2_Click(object sender, RoutedEventArgs e)
        {
            Settings = _mappingEngine.Mapper.Map<Settings>(_staticResources.DefaultSettings.Value[GetEquationType()]);
            Settings.FunctionF = new MathExpression(FixMinus(functionGTextBox2.Text));
            Settings.AmountOfPartitions = Convert.ToInt32(amountOfPartitionsTextBox2.Text);
            Settings.ColocationPoints = Settings.GetDefaultColocationPoints();

            var result = _integralEquationSolver.Solve(Settings, _staticResources.MatrixAInits.Value[GetEquationType()],
                _staticResources.MatrixBInits.Value[GetEquationType()]);

            ResultWindow resultWindow = new ResultWindow(result);
            resultWindow.Show();
        }

        private void resultBtn3_Click(object sender, RoutedEventArgs e)
        {
            Settings = _mappingEngine.Mapper.Map<Settings>(_staticResources.DefaultSettings.Value[GetEquationType()]);
            Settings.FunctionF = new MathExpression(FixMinus(functionGTextBox3.Text));
            Settings.AmountOfPartitions = Convert.ToInt32(amountOfPartitionsTextBox3.Text);
            Settings.ColocationPoints = Settings.PartitionPoints.Skip(1).Take(Settings.PartitionPoints.Count - 2).ToList();

            var result = _integralEquationSolver.Solve(Settings, _staticResources.MatrixAInits.Value[GetEquationType()],
                _staticResources.MatrixBInits.Value[GetEquationType()]);

            ResultWindow resultWindow = new ResultWindow(result);
            resultWindow.Show();
        }

        private void resultBtn4_Click(object sender, RoutedEventArgs e)
        {
            Settings = _mappingEngine.Mapper.Map<Settings>(_staticResources.DefaultSettings.Value[GetEquationType()]);
            Settings.FunctionF = new MathExpression(FixMinus(functionGTextBox4.Text));
            Settings.AmountOfPartitions = Convert.ToInt32(amountOfPartitionsTextBox4.Text);
            Settings.ColocationPoints = Settings.GetDefaultColocationPoints();
            Settings.Radius = Convert.ToDouble(radiusTextBox4.Text);

            var result = _integralEquationSolver.Solve(Settings, _staticResources.MatrixAInits.Value[GetEquationType()],
                _staticResources.MatrixBInits.Value[GetEquationType()]);

            ResultWindow resultWindow = new ResultWindow(result);
            resultWindow.Show();
        }

        private string FixMinus(string input)
        {
            string result = input.ToString();
            if (result[0] == '-')
            {
                result = $"(0{result})";
            }
            return result;
        }
    }
}
