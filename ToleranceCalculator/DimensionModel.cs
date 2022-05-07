using System;

namespace ToleranceCalculator
{
    public class DimensionModel
    {
        private double _lowerLimit;
        private double _nominal;
        private double _upperLimit;
        public StackDirection Direction { get; set; }

        public double LowerLimit
        {
            get
            {
                return _lowerLimit;
            }
            set
            {
                _lowerLimit = value;
                MinDimension = Nominal + LowerLimit;
            }
        }

        public double MaxDimension { get; set; }

        public double MinDimension { get; set; }

        public double Nominal
        {
            get => _nominal;
            set
            {
                if (value.ToString()[0] == '-')
                {
                    Direction = StackDirection.minus;
                    _nominal = Math.Abs(value);
                }
                else
                {
                    Direction = StackDirection.plus;
                    _nominal = Math.Abs(value);
                }
            }
        }

        public double UpperLimit
        {
            get
            {
                return _upperLimit;
            }

            set
            {
                _upperLimit = value;
                MaxDimension = Nominal + UpperLimit;
            }
        }

        public override string ToString()
        {
            return $"{(char)Direction} - {Nominal} - {UpperLimit} - {LowerLimit}";
        }
    }
}
