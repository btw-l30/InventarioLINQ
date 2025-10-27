public class Estudiante : Persona
{
    public string Carnet { get; set; }
    public string Curso { get; set; }
    public double Nota { get; set; }

    public Estudiante(string nombre, string apellido, int edad, string carnet, string curso, double nota)
        : base(nombre, apellido, edad)
    {
        Carnet = carnet;
        Curso = curso;
        Nota = nota;
    }

    public bool EstaAprobado()
    {
        return Nota >= 70;
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"Carnet: {Carnet}, Nombre: {Nombre} {Apellido}, Curso: {Curso}, Nota: {Nota:F2}, Estado: {(EstaAprobado() ? "APROBADO" : "REPROBADO")}");
    }

    public override string ToString()
    {
        return $"Carnet: {Carnet}, {Nombre} {Apellido}, Curso: {Curso}, Nota: {Nota:F2}";
    }
}