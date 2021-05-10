using System;

namespace Task7
{
    [Serializable]
    public class Car
    {
        public string name { get; set; }
        public string model { get; set; }
        public int power { get; set; }
        public int weight { get; set; }
        public int speedMax { get; set; }
        public int speed { get; set; }
        public string color { get; set; }

        public Car() { }

        public Car(string Name, string Model, int Power, int Weight, int SpeedMax, int Speed, string Color)
        {
            name = Name;
            model = Model;
            power = Power;
            weight = Weight;
            speedMax = SpeedMax;
            speed = Speed;
            color = Color;
        }
    }
}
