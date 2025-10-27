public class Profesor : Persona
{
    public string Materia { get; set; }
    public int AnosExperiencia { get; set; }

    public Profesor(string nombre, string apellido, int edad, string materia, int anosExperiencia)
        : base(nombre, apellido, edad)
    {
        Materia = materia;
        AnosExperiencia = anosExperiencia;
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"Profesor: {Nombre} {Apellido}, Materia: {Materia}, Experiencia: {AnosExperiencia} años");
    }
}