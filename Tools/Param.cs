namespace Entregable2_PD.Tools
{
    public class Param
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool Output { get; set; }
        public Param(string name, object value)
        {
            Name = name;
            Value = value;
            Output = false;
        }
        public Param(string name)
        {
            Name = name;
            Output = true;
        }
    }
}