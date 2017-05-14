using AutoMapper;
using PoohMathParser;
using System;
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
            Settings.FunctionF = new MathExpression(functionGTextBox1.Text);
            Settings.AmountOfPartitions = Convert.ToInt32(amountOfPartitionsTextBox1.Text);

            var result = _integralEquationSolver.Solve(Settings, _staticResources.MatrixAInits.Value[GetEquationType()],
                _staticResources.MatrixBInits.Value[GetEquationType()]);

            ResultWindow resultWindow = new ResultWindow(result);
            resultWindow.Show();
        }

        private void resultBtn2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resultBtn4_Click(object sender, RoutedEventArgs e)
        {
            Settings = _mappingEngine.Mapper.Map<Settings>(_staticResources.DefaultSettings.Value[GetEquationType()]);
            Settings.FunctionF = new MathExpression(functionGTextBox4.Text);
            Settings.AmountOfPartitions = Convert.ToInt32(amountOfPartitionsTextBox4.Text);

            var result = _integralEquationSolver.Solve(Settings, _staticResources.MatrixAInits.Value[GetEquationType()],
                _staticResources.MatrixBInits.Value[GetEquationType()]);

            ResultWindow resultWindow = new ResultWindow(result);
            resultWindow.Show();
        }
    }
}
